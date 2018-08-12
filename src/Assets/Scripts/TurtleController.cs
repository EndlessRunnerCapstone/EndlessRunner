using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurtleController : Photon.MonoBehaviour
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
     private bool invincible = false;
     Coroutine invincibleCoroutine = null;
     Coroutine recovering = null;
     private float recoveryWaitTime = 4f;
     private float recoveryTime = 2f;

    private AudioSource sfxPlayer;
    public AudioClip kickSound;
    public AudioClip stompSound;

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
          sfxPlayer = GetComponent<AudioSource>();
     }

     // Update is called once per frame
     void Update()
     {          
          UpdateEnemyPosition();
          
          //checkDeath();
     }

     private void LateUpdate()
     {
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
     }


     private void OnBecameVisible()
     {
          enabled = true;
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
               transform.localScale = new Vector2(-1f, 1f);
          }
          else if (rightCollision)
          {
               isMovingLeft = true;
               transform.localScale = new Vector2(1f, 1f);
          }
     }

     void CheckWallCollisionIdleShell()
     {
          RaycastHit2D leftCollision, rightCollision;
          leftCollision = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.02f), Vector2.left, 0.097f, playerLayer);
          rightCollision = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.02f), Vector2.right, 0.097f, playerLayer);          
          if (leftCollision.collider != null || rightCollision.collider != null)
          {
               if (leftCollision.collider != null)
               {                    
                    state = EnemyState.movingShell;
                    isMovingLeft = false;
                    velocity.x = 2f;
               }
               else
               {
                    state = EnemyState.movingShell;
                    isMovingLeft = true;
                    velocity.x = 2f;
               }
               StartCoroutine(Invincible());
               neverHit = false;
          }
     }
     void CheckWallCollisionMovingShell()
     {
          RaycastHit2D groundEnemyCollision;

          if (isMovingLeft)
          {
               groundEnemyCollision = Physics2D.Raycast(transform.position, Vector2.left, 0.1f, groundEnemyMask);               
          }
          else
          {
               groundEnemyCollision = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, groundEnemyMask);               
          }         
          
          if (groundEnemyCollision.collider != null)
          {
               if (groundEnemyCollision.collider != null)
               {
                    if (groundEnemyCollision.collider.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
                    {
                         if (groundEnemyCollision.collider.tag == "Goomba")
                         {
                                sfxPlayer.PlayOneShot(kickSound);
                                groundEnemyCollision.collider.gameObject.GetComponent<GoombaController>().StarDeath();
                         }
                         else if (groundEnemyCollision.collider.tag == "Turtle")
                         {
                                sfxPlayer.PlayOneShot(kickSound);
                                groundEnemyCollision.collider.gameObject.GetComponent<TurtleController>().StarDeath();
                         }
                         else if (groundEnemyCollision.collider.tag == "FlyingTurtle")
                         {
                                sfxPlayer.PlayOneShot(kickSound);
                                 groundEnemyCollision.collider.gameObject.GetComponent<FlyingTurtleController>().StarDeath();
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
          if (state == EnemyState.walking)
          {
               aboveLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.06f, transform.position.y + 0.02f), Vector2.up, 0.05f, playerLayer);
               aboveMiddle = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.02f), Vector2.up, 0.05f, playerLayer);
               aboveRight = Physics2D.Raycast(new Vector2(transform.position.x + 0.06f, transform.position.y + 0.02f), Vector2.up, 0.05f, playerLayer);
          }
          else
          {
               aboveRight = Physics2D.Raycast(new Vector2(transform.position.x - 0.08f, transform.position.y + 0.05f), Vector2.up, 0.04f, playerLayer);
               aboveMiddle = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.05f), Vector2.up, 0.04f, playerLayer);
               aboveLeft = Physics2D.Raycast(new Vector2(transform.position.x + 0.08f, transform.position.y + 0.05f), Vector2.up, 0.04f, playerLayer);
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
                    Debug.Log(hitRay);
                    OnHitAboveFromMario();
               }
          }
     }

     void OnHitAboveFromMario()
     {
        if(!Globals.TwoPlayer)
        {
            HitAboveInternal();
        }
        else
        {
            photonView.RPC("HitAboveInternal", PhotonTargets.All);
        }
     }

    [PunRPC]
    void HitAboveInternal()
    {
        if (!invincible)
        {
            if (state == EnemyState.walking)
            {
                if (neverHit)
                {
                    StartCoroutine(Recovering());
                }

                sfxPlayer.PlayOneShot(stompSound);
                state = EnemyState.shellIdle;
                velocity.x = 0;
                myAnimator.SetBool("idleShell", true);
            }
            else if (state == EnemyState.shellIdle)
            {
                sfxPlayer.PlayOneShot(kickSound);
                neverHit = false;
                state = EnemyState.movingShell;
                velocity.x = 2f;
                isMovingLeft = false;
            }
            else
            {
                state = EnemyState.shellIdle;
                velocity.x = 0;
            }
            StartCoroutine(Invincible());
        }
    }

     public IEnumerator Invincible()
     {
          if (invincibleCoroutine != null)
          {
               StopCoroutine(invincibleCoroutine);
          }

          invincible = true;          
          yield return new WaitForSeconds(0.3f);
          invincible = false;
     }

     public IEnumerator Recovering()
     {
          float currentTime = 0f;

          while (currentTime < recoveryWaitTime)
          {
               currentTime += 0.1f;
               if (!neverHit)
               {
                    yield break;
               }
               yield return new WaitForSeconds(0.1f);
          }

          currentTime = 0;
          myAnimator.SetBool("recovering", true);
          while (currentTime < recoveryTime)
          {
               currentTime += 0.1f;
               if (!neverHit)
               {
                    myAnimator.SetBool("recovering", false);
                    yield break;
               }
               yield return new WaitForSeconds(0.1f);
          }
          state = EnemyState.walking;
          velocity.x = 0.5f;
          myAnimator.SetBool("recovering", false);
          myAnimator.SetBool("idleShell", false);
          yield return null;
     }

     public void StarDeath()
     {
          myAnimator.SetBool("idleShell", true);
          GetComponent<Collider2D>().enabled = false;
          transform.rotation = new Quaternion(180, 0, 0, 0);
          rb.velocity = new Vector2(0.5f, 2f);
          shouldDie = true;
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
          if (state == EnemyState.walking)
          {
               if (collision.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
               {
                    isMovingLeft = !isMovingLeft;
               }
          }
     }
}
