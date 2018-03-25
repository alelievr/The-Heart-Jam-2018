using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingDetector : MonoBehaviour {

     [HideInInspector]
     public bool collided = false;
     public GameObject grabbed;
 
     void OnCollisionStay2D(Collision2D other)
     {
       if (!other.gameObject.CompareTag("Player")) {
         collided = true;
         grabbed = other.gameObject;
       }
     }
     private void OnCollisionExit2D(Collision2D other)
     {
         collided = false;
     }
}
