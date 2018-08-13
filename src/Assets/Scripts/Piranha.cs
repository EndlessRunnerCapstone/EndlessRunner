using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : Photon.MonoBehaviour {

     private Rigidbody2D rb;
     private bool shouldDie = false;
     private float deathTimer = 0;
     public float timeBeforeDestroy = 1.0f;

     // Use this for initialization
     void Start () {
          rb = GetComponent<Rigidbody2D>();          
	}
	
	// Update is called once per frame
	void Update () {
          checkDeath();
	}

     private void OnBecameVisible()
     {
          if (PhotonNetwork.connected)
          {
               photonView.RPC("SetEnabled", PhotonTargets.All);
          }
          else
          {
               SetEnabled();
          }
     }

     [PunRPC]
     private void SetEnabled()
     {
          enabled = true;
     }

     public void StarDeath()
     {
          GetComponent<Collider2D>().enabled = false;
          shouldDie = true;
     }

     void checkDeath()
     {
          if (shouldDie)
          {
               if (!PhotonNetwork.connected)
               {
                    Destroy(this.gameObject);
               }
               else if (PhotonNetwork.isMasterClient)
               {
                    PhotonNetwork.Destroy(this.gameObject);
               }

               shouldDie = false;
          }          
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.GetComponent<Collider2D>().tag == "Fireball" || (collision.GetComponent<Collider2D>().tag == "Player" && collision.GetComponent<Collider2D>().GetComponent<Player_Move>().starPower == true))
          {
               StarDeath();
          }    
     }
}
