using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoombaController : MonoBehaviour {

     public float gravity;
     public Vector2 velocity;
     public bool isWalkingLeft = true;
     public LayerMask groundLayer;
     private Rigidbody2D rb;
     public LayerMask playerLayer;
     private bool grounded = false;
     private bool shouldDie;
     private float deathTimer = 0;
     public float timeBeforeDestroy = 1.0f;

     private enum EnemyState
     {
          walking,           
          dead
     }

     private EnemyState state = EnemyState.walking;

	// Use this for initialization
	void Start () {
          rb = GetComponent<Rigidbody2D>();
          rb.freezeRotation = true;
          enabled = false;          
          gravity = 60f;
          velocity.x = 0.5f;
	}
	
	// Update is called once per frame
	void Update ()
     { 
          UpdateEnemyPosition();
          checkDeath();
     }


     private void OnBecameVisible()
     {
          enabled = true;
     }


     void Fall()
     {          
          grounded = false;
     }

     void UpdateEnemyPosition()
     {
          if (state != EnemyState.dead)
          {
               Vector3 pos = transform.localPosition;
               Vector3 scale = transform.localScale;

               if (state == EnemyState.walking)
               {
                    CheckWallCollision();
                    if (isWalkingLeft)
                    {
                         pos.x -= velocity.x * Time.deltaTime;                         
                    }
                    else
                    {
                         pos.x += velocity.x * Time.deltaTime;
                    }                    
               }

               transform.localPosition = pos;
               transform.localScale = scale;               
          }
     }


     void CheckWallCollision()
     {
          bool leftCollision = Physics2D.Raycast(transform.position, Vector2.left, 0.09f, groundLayer);
          bool RightCollision = Physics2D.Raycast(transform.position, Vector2.right, 0.09f, groundLayer);

          if (leftCollision)
          {
               isWalkingLeft = false;
          }
          else if (RightCollision)
          {
               isWalkingLeft = true;
          }
     }  

     public void Death()
     {
          state = EnemyState.dead;
          GetComponent<Rigidbody2D>().gravityScale = 0;
          GetComponent<Animator>().SetBool("isCrushed", true);
          GetComponent<Collider2D>().enabled = false;          
          shouldDie = true;
     }

     public void StarDeath()
     {
          state = EnemyState.dead;
          GetComponent<Collider2D>().enabled = false;
          transform.rotation = new Quaternion(180, 0, 0, 0);
          rb.velocity = new Vector2(0.5f, 2f);
          shouldDie = true;
     }

     void checkDeath()
     {
          if (shouldDie)
          {
               if (deathTimer <= timeBeforeDestroy)
               {
                    deathTimer += Time.deltaTime;
               }
               else
               {
                    shouldDie = false;
                    Destroy(this.gameObject);
               }
          }
     }
}
