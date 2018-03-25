using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingDetector : MonoBehaviour {

     [HideInInspector]
     public bool        collided = false;
     public GameObject  grabbed;
     public bool        touch;
 
    //  void OnCollisionStay2D(Collision2D other)
    //  {
    //    if (!other.gameObject.CompareTag("Player")) {
    //      collided = true;
    //      grabbed = other.gameObject;
    //    }
    //  }

    void OnCollisionStay2D(Collision2D collisionInfo) {
      touch = false;
        foreach (ContactPoint2D contact in collisionInfo.contacts) {
            // Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
            if (contact.rigidbody.gameObject.tag != "Player") {
              grabbed = contact.rigidbody.gameObject;
              touch = true;
            }
            // Debug.Log(contact.rigidbody.gameObject.name);
        }
    }

     private void OnCollisionExit2D(Collision2D other)
     {
         collided = false;
     }

     private void Update()
     {
     }
}
