using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public static void Launch (Rigidbody item, Vector3 linearVelocity, float speedFactor, int targetsLayer) {

        GameObject launcher = new GameObject();
        Launcher c = launcher.AddComponent<Launcher>();
        c.LaunchItem( item, linearVelocity, speedFactor, targetsLayer );
    }

    void LaunchItem (Rigidbody item, Vector3 linearVelocity, float speedFactor, int targetsLayer) {

        item.isKinematic = false;
        item.useGravity = true;
        item.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        
        RaycastHit hit;
        Debug.DrawRay( item.transform.position, linearVelocity.normalized * 20f, Color.red, 10f );
        if (Physics.Raycast( item.transform.position, linearVelocity, out hit, 20f )) {

            item.useGravity = false;
            
            speedFactor *= 4f;
            
            //StartCoroutine( DoLaunch( item.gameObject, hit.collider.transform.position, hit.distance * speedFactor ) );

            
            Vector3 target = hit.collider.transform.position;
            float time = hit.distance / speedFactor;

            Hashtable ht = new Hashtable();
            ht.Add( "x", target.x );
            ht.Add( "y", target.y );
            ht.Add( "z", target.z );
            ht.Add( "time", time );
            ht.Add( "delay", 0f );
            ht.Add( "onupdate", "" );
            ht.Add( "oncompletetarget", this.gameObject );
            ht.Add( "easetype", iTween.EaseType.easeInOutQuad );

            StartCoroutine( AddForceAfter( time, item ) );

            iTween.MoveTo( item.gameObject, ht );
            

        } else {
            item.AddForce( linearVelocity * speedFactor, ForceMode.Impulse );
            var rotation = Quaternion.LookRotation(linearVelocity.normalized);
            item.transform.rotation = rotation;
        }
    }

    public void oncomplete () {
        Debug.Log("ghgh");
    }

    IEnumerator AddForceAfter (float time, Rigidbody item) {

        /*float t = time * .8f;
        yield return new WaitForSeconds( time );

        Vector3 pos = item.transform.position;

        yield return new WaitForSeconds( (time-t) * .9f );*/
        yield return new WaitForSeconds( time * .9f );

        Destroy( item.gameObject );
        Destroy( this.gameObject );

        //iTween.Stop( item.gameObject );
        //item.useGravity = true;
        //item.AddForce( item.transform.position - pos, ForceMode.Impulse );
        //Debug.Log("fu");
        yield return null;
    }

    private static float LauchMovementInOutQuad (float currentTime, float startValue, float changeInValue, float duration) {
        currentTime /= duration / 2;
        if (currentTime < 1) return changeInValue / 2 * currentTime * currentTime + startValue;
        currentTime--;
        return -changeInValue / 2 * (currentTime * (currentTime - 2) - 1) + startValue;
    }

    private static IEnumerator DoLaunch (GameObject item, Vector3 targetPos, float duration) {

        float t = 0f;
        Vector3 startPos = item.transform.position;
        Debug.Log( "--duration: " + duration );

        while (t <= duration) { // if t is less than the duration

            // then set the block's x position using the math equation
            // (start off at 20 and increase that by 310)

            item.transform.position = new Vector3(
                LauchMovementInOutQuad( t, startPos.x, targetPos.x , duration ),
                LauchMovementInOutQuad( t, startPos.y, targetPos.y , duration ),
                LauchMovementInOutQuad( t, startPos.z, targetPos.z , duration )
            );

            Debug.Log("--time: " + t);
            

            // and then increase t by 1
            t += Time.deltaTime;

            yield return null;
        }
        Debug.Log( "--exit");
    }
}
