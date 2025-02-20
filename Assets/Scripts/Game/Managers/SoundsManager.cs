using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class SoundsManager: MonoBehaviour {
    #region Variables
    private const string PATH_SOUNDS    = "Sounds/";
    private const float GENERAL_VOLUME  = 1.0f;

    #region SFX
    private const string PATH_SFX       = "SFX/";
    private const int NB_SOURCES_SFX    = 4;
    private const float SFX_VOLUME      = 1f;

    private List<AudioSource> m_SFXSources;
    private Dictionary<SM_SFX, AudioClip> m_SFXDictionary;
    #endregion

    #region Musics
    private const string PATH_MUSICS    = "Musics/";
    private const int NB_SOURCES_MUSICS = 2;
    private const float MUSICS_VOLUME   = 0.25f;
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
    private void Awake() {
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
            AudioClip l_SFX = Resources.Load<AudioClip>(PATH_SOUNDS + PATH_SFX + l_Name.ToString());
            if (l_SFX == null) Debug.LogError("SoundsManager: " + PATH_SOUNDS + PATH_SFX + l_Name.ToString() + " not found.");
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
            AudioClip l_Music = Resources.Load<AudioClip>(PATH_SOUNDS + PATH_MUSICS + l_Name.ToString());

            if (l_Music == null) Debug.LogError("SoundsManager: " + PATH_SOUNDS + PATH_MUSICS + l_Name.ToString() + " not found.");
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

    private void OnDestroy() {
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
    
    #region Sounds Managment
    public void PlaySfx(SM_SFX p_Sfx) {
        if (!m_SFXDictionary.ContainsKey(p_Sfx)) return;

        AudioSource l_AudioSource = m_SFXSources.Find(item => !item.isPlaying);
        l_AudioSource.spatialBlend = 1.0f;
        if (l_AudioSource) l_AudioSource.PlayOneShot(m_SFXDictionary[p_Sfx], SFX_VOLUME * GENERAL_VOLUME);
	}

    public void PlaySFXOnTarget(SM_SFX p_Sfx, Vector3 p_Target) {
        if (!m_SFXDictionary.ContainsKey(p_Sfx)) return;

        GameObject l_Obj            = new GameObject();
        l_Obj.transform.position    = p_Target;

        AudioSource l_AudioSource   = l_Obj.AddComponent<AudioSource>();
        l_AudioSource.spatialBlend  = 1.0f;
        l_AudioSource.loop          = false;
        l_AudioSource.playOnAwake   = false;
        l_AudioSource.PlayOneShot(m_SFXDictionary[p_Sfx], SFX_VOLUME * GENERAL_VOLUME);
    }

    public void PlayMusic(SM_Musics p_Music) {
        if (!m_MusicsDictionary.ContainsKey(p_Music)) return;

        m_LastMusicID       = m_CurrentMusicID;
        m_CurrentMusicID    = (m_CurrentMusicID + 1) % NB_SOURCES_MUSICS;
        m_CurrentMusic      = p_Music;
        
        m_MusicsSources[m_CurrentMusicID].clip = m_MusicsDictionary[p_Music];
        StartCoroutine(FadeCoroutine());
        m_MusicsSources[m_CurrentMusicID].Play();
    }

    #region Utils
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
    #endregion
}

public enum SM_SFX {
    shuriken_1,
    shuriken_2,
    shuriken_3,
    shuriken_4,
    enemy_pop_1,
    enemy_pop_2,
    enemy_die_1,
    //enemy_hurt_1,
    //ambiance_1,
    //ambiance_2,
    //ambiance_3,
    //ambiance_4,
    //ambiance_5,
    //ambiance_6,
    //ambiance_7,
    //ambiance_8,
    //ambiance_9,
    //ambiance_10,
    //ambiance_11,
    //ambiance_12,
    //ambiance_13,
    bullet_hit_wood,
    bullet_hit_rock,
    bullet_hit_dirt,
    bullet_hit_water
}

public enum SM_Musics {
    short_level_1,
    short_level_2,
    short_level_3,
    short_level_4,
    short_level_5,
    short_level_6,
    short_level_7,
    short_level_8,
    short_level_9,
}