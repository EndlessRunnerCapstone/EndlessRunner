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
               CheckPlayerCollision(pos);
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

     void CheckPlayerCollision(Vector3 pos)
     {
          
          RaycastHit2D leftSide = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.07f, transform.localPosition.y), Vector2.down, 0.11f, playerLayer);
          RaycastHit2D middle = Physics2D.Raycast(transform.localPosition, Vector2.down, 0.11f, playerLayer);
          RaycastHit2D rightSide = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.07f, transform.localPosition.y), Vector2.down, 0.11f, playerLayer);

          RaycastHit2D topLeftCast = Physics2D.Raycast(new Vector2(pos.x, pos.y + 0.065f), Vector2.left, 0.11f, playerLayer);
          RaycastHit2D topRightCast = Physics2D.Raycast(new Vector2(pos.x, pos.y + 0.065f), Vector2.right, 0.11f, playerLayer);
          RaycastHit2D midLeftCast = Physics2D.Raycast(new Vector2(pos.x, pos.y), Vector2.left, 0.11f, playerLayer);
          RaycastHit2D midRightCast = Physics2D.Raycast(new Vector2(pos.x, pos.y), Vector2.right, 0.11f, playerLayer);
          RaycastHit2D bottomLeftCast = Physics2D.Raycast(new Vector2(pos.x, pos.y - 0.065f), Vector2.left, 0.11f, playerLayer);
          RaycastHit2D bottomRightCast = Physics2D.Raycast(new Vector2(pos.x, pos.y - 0.065f), Vector2.right, 0.11f, playerLayer);


          if (leftSide.collider != null || middle.collider != null || rightSide.collider != null || topLeftCast.collider != null || topRightCast.collider != null || midLeftCast.collider != null || midRightCast.collider != null || bottomLeftCast.collider != null || bottomRightCast.collider != null)
          {
               RaycastHit2D hitRay;
               if (leftSide)
               {
                    hitRay = leftSide;
               }
               else if (middle)
               {
                    hitRay = middle;
               }
               else if (rightSide)
               {
                    hitRay = rightSide;
               }
               else if (topLeftCast)
               {
                    hitRay = topLeftCast;
               }
               else if (topRightCast)
               {
                    hitRay = topRightCast;
               }
               else if (midLeftCast)
               {
                    hitRay = midLeftCast;
               }
               else if (midRightCast)
               {
                    hitRay = midRightCast;
               }
               else if (bottomLeftCast)
               {
                    hitRay = bottomLeftCast;
               }
               else
               {
                    hitRay = bottomRightCast;
               }

               Debug.Log(hitRay.collider);
               if(hitRay.collider.tag == "Player")
               {
                    SceneManager.LoadScene("Level01");
               }
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
