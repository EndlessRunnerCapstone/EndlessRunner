using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.GetComponent<BoxCollider2D>().gameObject.layer == LayerMask.NameToLayer("Player"))
          {
               collision.gameObject.GetComponent<Player_Move>().isBig = true;
               collision.gameObject.GetComponent<Player_Move>().fireFlower = true;
               Destroy(this.gameObject);
          }
     }
}
