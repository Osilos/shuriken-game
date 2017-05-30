using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shuriken : OVRGrabbable {
    public Action<Shuriken> OnGrabStart;
    public Action<Shuriken> OnGrabEnd;

    [SerializeField]
    private float m_TimeBeforeDestroy = 10f;

	// Use this for initialization
	void Start () {
		
	}

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if(OnGrabStart != null) OnGrabStart(this);
        base.GrabBegin(hand, grabPoint);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        if (OnGrabEnd != null) OnGrabEnd(this);
        base.GrabEnd(linearVelocity, angularVelocity);
        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(m_TimeBeforeDestroy);
    }
}
