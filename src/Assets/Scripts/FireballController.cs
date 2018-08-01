using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {
     private Rigidbody2D rb;
     public Vector2 velocity;
     public LayerMask groundEnemyMask;
     public bool isGrounded = false;
     private Animator myAnimator;

	// Use this for initialization
	void Start () {          
          rb = GetComponent<Rigidbody2D>();
          velocity = rb.velocity;
          myAnimator = GetComponent<Animator>();          
     }
	
	// Update is called once per frame
	void Update () {
          if (rb.velocity.y < velocity.y)
          {
               rb.velocity = velocity;
          }         
	}

     private void OnBecameInvisible()
     {
          Destroy(this.gameObject);
     }

     private void OnHitEnemy(GameObject enemy)
     {
          if (enemy.tag == "Goomba")
          {
               enemy.GetComponent<GoombaController>().StarDeath();
               StartCoroutine(Die());
          }          
          else if (enemy.tag == "Turtle")
          {
               enemy.GetComponent<TurtleController>().StarDeath();
               StartCoroutine(Die());
          }
          else if (enemy.tag == "FlyingTurtle")
          {
               enemy.GetComponent<FlyingTurtleController>().StarDeath();
               StartCoroutine(Die());
          }
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
          if (collision.collider.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
          {
               OnHitEnemy(collision.collider.gameObject);
          }
          else
          {
               rb.velocity = new Vector2(velocity.x, -velocity.y);

               if (collision.contacts[0].normal.x != 0)
               {
                    StartCoroutine(Die());
               }
          }

     }

     private IEnumerator Die()
     {
          rb.velocity = new Vector2(0, 0);
          rb.gravityScale = 0;
          GetComponent<CircleCollider2D>().enabled = false;
          myAnimator.SetBool("explosion", true);
          yield return new WaitForSeconds(0.1f);
          Destroy(this.gameObject);
     }
}
