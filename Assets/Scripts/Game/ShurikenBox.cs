using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShurikenColor
{
    None,
    Green,
    Red
}

public class ShurikenBox : MonoBehaviour {
    [SerializeField]
    private ShurikenColor m_ShurikenColor;
    private GameObject m_ShurikenTemplate;

    static readonly string SHURIKEN_PATH = "Prefabs/Shuriken";

    // Use this for initialization
    void Start()
    {
        m_ShurikenTemplate = Resources.Load<GameObject>(SHURIKEN_PATH);
        //m_ShurikenTemplate = Resources.Load<GameObject>(SHURIKEN_PATH + m_ShurikenColor.ToString());
        if (m_ShurikenTemplate)
        {
            SpawnShuriken();
        }
        else
        {
            Debug.LogError("404 - Shuriken template not found");
        }
    }

    void SpawnShuriken()
    {
        GameObject l_ShurikenGO = Instantiate(m_ShurikenTemplate);
        Shuriken l_ShurikenScript = l_ShurikenGO.GetComponent<Shuriken>();
        if(l_ShurikenScript)
        {
            l_ShurikenScript.OnGrabStart += OnShurikenGrabStart;
            l_ShurikenScript.OnGrabEnd += OnShurikenGrabEnd;
        }
        l_ShurikenScript.transform.position = transform.position;
        l_ShurikenGO.transform.parent = transform;
        
    }

    void OnShurikenGrabStart(Shuriken p_Shuriken)
    {
        p_Shuriken.OnGrabStart -= OnShurikenGrabStart;
        SpawnShuriken();
    }

    void OnShurikenGrabEnd(Shuriken p_Shuriken)
    {
        p_Shuriken.OnGrabEnd -= OnShurikenGrabEnd;
    }
}
