using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserFireController : MonoBehaviour {

     private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
          rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
          rb.velocity = new Vector2(-1.5f, 0);
	}

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.GetComponent<Collider2D>().gameObject.tag == "Player")
          {
               collision.GetComponent<Player_Move>().HitByBowser();
          }
     }

     private void OnBecameInvisible()
     {
          Destroy(this.gameObject);
     }
}
