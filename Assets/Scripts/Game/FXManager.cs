using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager: MonoBehaviour {
    #region Variables
    private const string PATH_FX = "Prefabs/FX/";

    private Dictionary<FM_FX, GameObject> m_Particles;
    private List<ParticleSystem> m_PlayingParticles;
    #endregion

    #region Initialisation & Destroy
    private void Awake() {
        InitFX();
    }

    private void InitFX() {
        m_Particles         = new Dictionary<FM_FX, GameObject>();
        m_PlayingParticles  = new List<ParticleSystem>();
        Array l_Names       = Enum.GetValues(typeof(FM_FX));

        foreach (FM_FX l_Name in l_Names) {
            GameObject l_FX = Resources.Load<GameObject>(PATH_FX + l_Name.ToString());
            if (l_FX == null) Debug.LogError("FXManager: " + PATH_FX + l_Name.ToString() + " not found.");
            m_Particles.Add(l_Name, l_FX);
        }
    }

    private void OnDestroy() {
        m_Particles.Clear();
        m_Particles = null;

        m_PlayingParticles.Clear();
        m_PlayingParticles = null;
    }
    #endregion

    #region FX Managment
    public void PlayOnce(FM_FX p_FX, Transform p_Parent, Vector3 p_Position) {
        if (!m_Particles.ContainsKey(p_FX)) return;

        GameObject l_Object         = Instantiate(m_Particles[p_FX]);
        l_Object.transform.parent   = p_Parent;
        l_Object.transform.position = p_Position;

        StartCoroutine(PlayOnceCoroutine(l_Object.GetComponent<ParticleSystem>()));
    }

    public int PlayRepeatedly(FM_FX p_FX, Transform p_Parent, Vector3 p_Position) {
        if (!m_Particles.ContainsKey(p_FX)) return -1;

        GameObject l_Object         = Instantiate(m_Particles[p_FX]);
        l_Object.transform.parent   = p_Parent;
        l_Object.transform.position = p_Position;

        int l_ID = m_PlayingParticles.FindIndex(null);
        Debug.Log(l_ID);
        if (l_ID < 0) {
            m_PlayingParticles.Add(l_Object.GetComponent<ParticleSystem>());
            return m_PlayingParticles.Count - 1;
        }
        else {
            m_PlayingParticles[l_ID] = l_Object.GetComponent<ParticleSystem>();
            return l_ID;
        }
    }

    public void StopFX(int p_FXID) {
        if (p_FXID < 0) return;

        //TODO
    }

    #region Utils
    private IEnumerator PlayOnceCoroutine(ParticleSystem p_FX) {
        while (p_FX.isEmitting) {
            yield return null;
        }

        Destroy(p_FX.gameObject);
    }
    #endregion
    #endregion
}

public enum FM_FX {
    FX_Fireworks_Blue_Small,
    FX_Fireworks_Green_Small,
    FX_Fireworks_Yellow_Small
}