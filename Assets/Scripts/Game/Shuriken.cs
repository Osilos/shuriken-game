using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shuriken : OVRGrabbable {
    public Action<Shuriken> OnGrabStart;
    public Action<Shuriken> OnGrabEnd;

    [SerializeField]
    private float m_TimeBeforeDestroy = 10f;

    [SerializeField]
    private GameObject m_TargetCollider;

    [SerializeField]
    private GameObject m_GrabCollider;

    [SerializeField]
    private GameObject m_TrailGO;

    // Use this for initialization
    void Start () {
        if (m_TargetCollider)
            m_TargetCollider.SetActive(false);
        if (m_TrailGO)
            m_TrailGO.SetActive(false);
        if (m_GrabCollider)
            m_GrabCollider.SetActive(true);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if(OnGrabStart != null) OnGrabStart(this);
        Transform l_SnapTransform = hand.transform.Find("Snap");
        if(l_SnapTransform)
            snapOffset = l_SnapTransform;
        base.GrabBegin(hand, grabPoint);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        if (OnGrabEnd != null) OnGrabEnd(this);
        base.GrabEnd(linearVelocity, angularVelocity);
        StartCoroutine(DelayDestroy());

        if (m_TargetCollider)
            m_TargetCollider.SetActive(true);
        if (m_GrabCollider)
            m_GrabCollider.SetActive(false);
        if (m_TrailGO)
            m_TrailGO.SetActive(true);
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(m_TimeBeforeDestroy);
    }
}
