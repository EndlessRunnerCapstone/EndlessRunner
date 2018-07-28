using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurtleController : MonoBehaviour
{

     public float gravity;
     public Vector2 velocity;
     public bool isMovingLeft = true;
     public LayerMask groundEnemyMask;
     private Rigidbody2D rb;
     public LayerMask playerLayer;
     private bool grounded = false;
     private bool shouldDie;
     private float deathTimer = 0;
     public float timeBeforeDestroy = 1.0f;
     private Animator myAnimator;
     private bool neverHit = true;

     public enum EnemyState
     {
          walking,
          shellIdle,
          movingShell
     }

     public EnemyState state = EnemyState.walking;

     // Use this for initialization
     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          rb.freezeRotation = true;
          enabled = false;
          gravity = 60f;
          velocity.x = 0.5f;
          myAnimator = GetComponent<Animator>();
     }

     // Update is called once per frame
     void Update()
     {
          UpdateEnemyPosition();
          CheckAbove();

          if (state == EnemyState.walking)
          {
               CheckWallCollisionWalking();
          }
          else if (state == EnemyState.shellIdle)
          {
               CheckWallCollisionIdleShell();
          }
          else
          {
               CheckWallCollisionMovingShell();
          }
          //checkDeath();
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
          if (state != EnemyState.shellIdle)
          {
               Vector3 pos = transform.localPosition;
               Vector3 scale = transform.localScale;

               if (state == EnemyState.walking)
               {
                    CheckWallCollisionWalking();
               }

               if (isMovingLeft)
               {
                    pos.x -= velocity.x * Time.deltaTime;
               }
               else
               {
                    pos.x += velocity.x * Time.deltaTime;
               }

               transform.localPosition = pos;
               transform.localScale = scale;
          }          
     }


     void CheckWallCollisionWalking()
     {
          bool leftCollision, rightCollision;        
          leftCollision = Physics2D.Raycast(transform.position, Vector2.left, 0.1f, groundEnemyMask);
          rightCollision = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, groundEnemyMask);

          if (leftCollision)
          {
               isMovingLeft = false;
          }
          else if (rightCollision)
          {
               isMovingLeft = true;
          }
     }

     void CheckWallCollisionIdleShell()
     {
          RaycastHit2D leftCollision, rightCollision;
          leftCollision = Physics2D.Raycast(transform.position, Vector2.left, 0.1f, playerLayer);
          rightCollision = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, playerLayer);
          Debug.DrawRay(transform.position, Vector2.left * 0.1f, Color.red);
          if (leftCollision.collider != null || rightCollision.collider != null)
          {
               if (leftCollision.collider != null)
               {                    
                    state = EnemyState.movingShell;
                    isMovingLeft = false;
                    velocity.x = 2.5f;
               }
               else
               {
                    state = EnemyState.movingShell;
                    isMovingLeft = true;
                    velocity.x = 2.5f;
               }
          }
     }
     void CheckWallCollisionMovingShell()
     {
          RaycastHit2D groundEnemyCollision, playerCollision;

          if (isMovingLeft)
          {
               groundEnemyCollision = Physics2D.Raycast(transform.position, Vector2.left, 0.1f, groundEnemyMask);
               playerCollision = Physics2D.Raycast(transform.position, Vector2.left, 0.1f, playerLayer);
          }
          else
          {
               groundEnemyCollision = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, groundEnemyMask);
               playerCollision = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, playerLayer);
          }         
          
          if (groundEnemyCollision.collider != null || playerCollision.collider != null)
          {
               if (groundEnemyCollision.collider != null)
               {
                    if (groundEnemyCollision.collider.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
                    {
                         if (groundEnemyCollision.collider.tag == "Goomba")
                         {
                              groundEnemyCollision.collider.gameObject.GetComponent<GoombaController>().StarDeath();
                         }
                    }
                    else
                    {
                         isMovingLeft = !isMovingLeft;
                    }
               }
               else
               {
                    isMovingLeft = !isMovingLeft;
               }
          }          
     }

     void CheckAbove()
     {
          RaycastHit2D aboveLeft, aboveMiddle, aboveRight, hitRay;
          if (state == EnemyState.walking && isMovingLeft)
          {
               aboveLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.04f, transform.position.y + 0.09f), Vector2.up, 0.04f, playerLayer);
               aboveMiddle = Physics2D.Raycast(new Vector2(transform.position.x + 0.02f, transform.position.y + 0.02f), Vector2.up, 0.04f, playerLayer);
               aboveRight = Physics2D.Raycast(new Vector2(transform.position.x + 0.06f, transform.position.y + 0.01f), Vector2.up, 0.04f, playerLayer);
          }

          else if (state == EnemyState.walking && !isMovingLeft)
          {
               aboveRight = Physics2D.Raycast(new Vector2(transform.position.x - 0.04f, transform.position.y + 0.09f), Vector2.up, 0.04f, playerLayer);
               aboveMiddle = Physics2D.Raycast(new Vector2(transform.position.x + 0.02f, transform.position.y + 0.02f), Vector2.up, 0.04f, playerLayer);
               aboveLeft = Physics2D.Raycast(new Vector2(transform.position.x + 0.06f, transform.position.y + 0.01f), Vector2.up, 0.04f, playerLayer);
          }
          else
          {
               return;
          }

          if (aboveLeft.collider != null || aboveMiddle.collider != null || aboveRight.collider != null)
          {
               if (aboveLeft.collider != null)
               {
                    hitRay = aboveLeft;
               }
               else if (aboveRight.collider != null)
               {
                    hitRay = aboveRight;
               }
               else
               {
                    hitRay = aboveMiddle;
               }

               if (hitRay.collider.tag == "Player")
               {
                    OnHitFromMario();
               }
          }
     }

     void OnHitFromMario()
     {
          if (state == EnemyState.walking)
          {
               state = EnemyState.shellIdle;
               velocity.x = 0;
               myAnimator.SetBool("idleShell", true);
          }
     }

     private void OnDrawGizmos()
     {
     }

     /*public void Death()
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
     }*/
}
