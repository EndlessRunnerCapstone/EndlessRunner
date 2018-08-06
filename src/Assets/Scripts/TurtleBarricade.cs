using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBarricade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.tag == "Turtle" && collision.gameObject.GetComponent<TurtleController>().state == TurtleController.EnemyState.walking)
          {
               collision.gameObject.GetComponent<TurtleController>().isMovingLeft = !collision.gameObject.GetComponent<TurtleController>().isMovingLeft;
               if (collision.gameObject.GetComponent<TurtleController>().isMovingLeft)
               {
                    collision.transform.localScale = new Vector2(1f, 1f);
               }
               else
               {
                    collision.transform.localScale = new Vector2(-1f, 1f);
               }
          }
     }
}
