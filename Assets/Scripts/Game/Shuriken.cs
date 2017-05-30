using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shuriken : OVRGrabbable {
    public Action<Shuriken> OnGrabStart;
    public Action<Shuriken> OnGrabEnd;

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
    }
}
