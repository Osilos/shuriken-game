using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public short health;
    public byte attackTypes;

    void Awake () {
        health = 0;
        attackTypes = (byte)AttackType.Main;
    }

    void Start () {
        AnimPop();
    }

    void AnimPop () {

    }

    void AnimDie () {

    }

    void AnimHit () {

    }

    void AnimMiss () {

    }

    void LooseHealth () {
        if (--health <= 0) {
            AnimDie();
        } else {
            AnimHit();
        }
    }

    public void OnHit (AttackType type = AttackType.Main) {
        if ((byte)type == attackTypes) {
            LooseHealth();
        } else {
            AnimMiss();
        }
    }

}
