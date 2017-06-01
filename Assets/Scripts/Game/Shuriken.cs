using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Shuriken: OVRGrabbable {
    #region Variables
    private Action doAction;
    private Transform m_SnapTransform;

    [SerializeField]
    private float m_SoundLimit = 2f;
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

    public Action<Shuriken> OnGrabStart;
    public Action<Shuriken> OnGrabEnd;
    #endregion
    
    void Start () {
        SetModeVoid();
        if (m_TargetCollider)
            m_TargetCollider.SetActive(false);
        if (m_TrailGO)
            m_TrailGO.SetActive(false);
        if (m_GrabCollider)
            m_GrabCollider.SetActive(true);
    }

    private void Update()
    {
        doAction();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        if(OnGrabStart != null) OnGrabStart(this);
        SetModeGrabbed(hand.transform.Find("Snap"));
        base.GrabBegin(hand, grabPoint);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity) {
        SetModeVoid();

        if (OnGrabEnd != null) OnGrabEnd(this);
        base.GrabEnd(linearVelocity, angularVelocity);
        Invoke("Destroy", m_TimeBeforeDestroy);

        if (m_TargetCollider) m_TargetCollider.SetActive(true);
        if (m_GrabCollider) m_GrabCollider.SetActive(false);
        if (m_TrailGO) m_TrailGO.SetActive(true);

        Rigidbody l_ShurikenRB = GetComponent<Rigidbody>();
        if (l_ShurikenRB) {
            Launcher.Launch(l_ShurikenRB, linearVelocity, m_handSpeedMultiplier, LayerMask.NameToLayer("Enemy") );
        }
        
        if (linearVelocity.magnitude > m_SoundLimit) GameManager.instance.soundManager.PlaySfx((SM_SFX)UnityEngine.Random.Range(0, 4));
    }

    private void Destroy() {
        Destroy(gameObject);
    }

    #region StateMachine
    private void SetModeVoid() {
        doAction = DoActionVoid;
    }

    private void SetModeGrabbed(Transform p_SnapTransform) {
        doAction = DoActionGrabbed;
        m_SnapTransform = p_SnapTransform;
    }

    private void DoActionVoid() {

    }

    private void SetModeLaunched()
    {
        doAction = DoActionLaunched;
        m_TargetCollider.SetActive(true);
        m_GrabCollider.SetActive(false);

        Rigidbody l_ShurikenRB = GetComponent<Rigidbody>();
        if (l_ShurikenRB)
        {
            l_ShurikenRB.isKinematic = false;
        }
        
    }

    private void DoActionLaunched()
    {
        Rigidbody l_ShurikenRB = GetComponent<Rigidbody>();
        if (l_ShurikenRB)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(l_ShurikenRB.velocity), Vector3.Magnitude(l_ShurikenRB.velocity) / 10);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SetModeVoid();
        Rigidbody l_ShurikenRB = GetComponent<Rigidbody>();
        if (l_ShurikenRB)
        {
            l_ShurikenRB.isKinematic = true;
        }
    }

    private void DoActionGrabbed() {
        transform.position = m_SnapTransform.position;
        transform.rotation = m_SnapTransform.rotation;
    }
    #endregion
}