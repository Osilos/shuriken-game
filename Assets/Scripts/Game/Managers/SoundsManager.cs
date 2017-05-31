using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class SoundsManager: MonoBehaviour {
    #region Variables
    private const string PATH_SOUNDS    = "";
    private const float GENERAL_VOLUME  = 1.0f;

    #region SFX
    private const string PATH_SFX       = "";
    private const int NB_SOURCES_SFX    = 10;
    private const float SFX_VOLUME      = 0.5f;

    private List<AudioSource> m_SFXSources;
    private Dictionary<SM_SFX, AudioClip> m_SFXDictionary;
    #endregion

    #region Musics
    private const string PATH_MUSICS    = "";
    private const int NB_SOURCES_MUSICS = 2;
    private const float MUSICS_VOLUME   = 0.5f;
    private const float m_FadeDuration  = 2.0f;

    private List<AudioSource> m_MusicsSources;
    private Dictionary<SM_Musics, AudioClip> m_MusicsDictionary;
    private int m_CurrentMusicID;
    private int m_LastMusicID;
    private SM_Musics m_CurrentMusic;
    private SM_Musics m_LastMusic;
    #endregion
    #endregion

    #region Initialisation & Destroy
    public void Awake() {
        InitSFX();
        InitMusics();
    }

    private void InitSFX() {
        #region Sources
        m_SFXSources = new List<AudioSource>();

        for (int cptSource = 0; cptSource < NB_SOURCES_SFX; cptSource++) {
            AddSource(m_SFXSources, false);
        }
        #endregion

        #region AudioClip
        m_SFXDictionary = new Dictionary<SM_SFX, AudioClip>();
        Array l_Names   = Enum.GetValues(typeof(SM_SFX));
        
        foreach (SM_SFX l_Name in l_Names) {
            AudioClip l_SFX = Resources.Load(PATH_SFX + l_Name.ToString()) as AudioClip;
            if (l_SFX == null) Debug.LogError("SoundsManager: " + PATH_SFX + l_Name.ToString() + " not found.");
            m_SFXDictionary.Add(l_Name, l_SFX);
        }
        #endregion
    }

    private void InitMusics() {
        #region Sources
        m_MusicsSources     = new List<AudioSource>();
        m_CurrentMusicID    = 1;
        m_LastMusicID       = 0;

        for (int cptSource = 0; cptSource < NB_SOURCES_MUSICS; cptSource++) {
            AddSource(m_MusicsSources, true);
            m_MusicsSources[cptSource].volume = 0;
        }
        #endregion

        #region AudioClip
        m_MusicsDictionary  = new Dictionary<SM_Musics, AudioClip>();
        Array l_Names       = Enum.GetValues(typeof(SM_Musics));

        foreach (SM_Musics l_Name in l_Names) {
            AudioClip l_Music = Resources.Load(PATH_MUSICS + l_Name.ToString()) as AudioClip;
            if (l_Music == null) Debug.LogError("SoundsManager: " + PATH_MUSICS + l_Name.ToString() + " not found.");
            m_MusicsDictionary.Add(l_Name, l_Music);
        }
        #endregion
    }

    private void AddSource(List<AudioSource> p_Sources, bool p_IsLooping) {
        AudioSource l_AudioSource   = gameObject.AddComponent<AudioSource>();
        l_AudioSource.loop          = p_IsLooping;
        l_AudioSource.playOnAwake   = false;
        p_Sources.Add(l_AudioSource);
    }

    public void OnDestroy() {
        m_SFXSources.Clear();
        m_SFXSources = null;
        
        m_SFXDictionary.Clear();
        m_SFXDictionary = null;

        m_MusicsSources.Clear();
        m_MusicsSources = null;
        
        m_MusicsDictionary.Clear();
        m_MusicsDictionary = null;
    }
    #endregion
    
    #region SFX Managment
    public void PlaySfx(SM_SFX p_Sfx) {
        if (!m_SFXDictionary.ContainsKey(p_Sfx)) return;

        AudioSource l_AudioSource = m_SFXSources.Find(item => !item.isPlaying);
		if (l_AudioSource) l_AudioSource.PlayOneShot(m_SFXDictionary[p_Sfx], SFX_VOLUME * GENERAL_VOLUME);
	}
    #endregion

    #region Musics Managment
    public void PlayMusic(SM_Musics p_Music) {
        if (!m_MusicsDictionary.ContainsKey(p_Music)) return;

        m_LastMusicID       = m_CurrentMusicID;
        m_CurrentMusicID    = (m_CurrentMusicID + 1) % NB_SOURCES_MUSICS;
        m_CurrentMusic      = p_Music;
        
        m_MusicsSources[m_CurrentMusicID].clip = m_MusicsDictionary[p_Music];
        StartCoroutine(FadeCoroutine());
        m_MusicsSources[m_CurrentMusicID].Play();
    }

    private IEnumerator FadeCoroutine() {
        float l_ElapsedTime         = 0;
        float l_LastMusicVolume     = m_MusicsSources[m_LastMusicID].volume;

        while (l_ElapsedTime < m_FadeDuration) {
            float l_FadeTimeRatio = l_ElapsedTime / m_FadeDuration;

            m_MusicsSources[m_CurrentMusicID].volume    = Mathf.Lerp(0, MUSICS_VOLUME * GENERAL_VOLUME, l_FadeTimeRatio);
            m_MusicsSources[m_LastMusicID].volume       = Mathf.Lerp(l_LastMusicVolume, 0, l_FadeTimeRatio);
            l_ElapsedTime                               += Time.deltaTime;

            yield return null;
        }

        m_MusicsSources[m_LastMusicID].Stop();
    }
    #endregion
}

public enum SM_SFX {

}

public enum SM_Musics {

}