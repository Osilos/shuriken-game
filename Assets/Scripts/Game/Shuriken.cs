using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
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

    [SerializeField]
    private float m_handSpeedMultiplier;

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
        transform.SetParent(null);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        if (OnGrabEnd != null) OnGrabEnd(this);
        base.GrabEnd(linearVelocity, angularVelocity);
        Invoke("Destroy", m_TimeBeforeDestroy);

        if (m_TargetCollider)
            m_TargetCollider.SetActive(true);
        if (m_GrabCollider)
            m_GrabCollider.SetActive(false);
        if (m_TrailGO)
            m_TrailGO.SetActive(true);

        Rigidbody l_ShurikenRB = GetComponent<Rigidbody>();
        if (l_ShurikenRB)
        {
            l_ShurikenRB.isKinematic = false;
            l_ShurikenRB.useGravity = true;
            l_ShurikenRB.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            l_ShurikenRB.AddForce(linearVelocity * m_handSpeedMultiplier, ForceMode.Impulse);
        }
    }


    private void Destroy ()
    {
        Destroy(gameObject);
    }
    
}
