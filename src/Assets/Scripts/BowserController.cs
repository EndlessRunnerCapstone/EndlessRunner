using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserController : MonoBehaviour {
     private bool isDead;
     private Rigidbody2D rb;
     private float jumpForce = 500;
     [SerializeField] private bool isJumping;
     private float jumpStart;
     private float jumpTime;
     public LayerMask groundLayer;
     public int health;
     private bool shouldDie;
     private Coroutine jumpingCoroutine;
     private Coroutine shootingFire;
     private Coroutine movingCoroutine;
     [SerializeField] private GameObject fireball;
     private bool movingLeft, movingRight;
     private Animator myAnimator;
     public bool stoppedCoroutines = false;
    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip fireBallSound;

    // Use this for initialization
    void Start () {
          enabled = false;
          isDead = false;
          rb = GetComponent<Rigidbody2D>();          
          rb.freezeRotation = true;
          jumpTime = .5f;
          isJumping = false;
          health = 8;
          shouldDie = false;
          myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
          if (!shouldDie)
          {
               DeathCheck();
               if (movingLeft == true)
               {
                    rb.velocity = new Vector2(-0.4f, rb.velocity.y);

               }
               else if (movingRight == true)
               {
                    rb.velocity = new Vector2(0.4f, rb.velocity.y);
               }
               Vector2 clampedPosition = transform.position;
               clampedPosition.x = Mathf.Clamp(transform.position.x, 15, 17.7f);
               transform.position = clampedPosition;
          }

          if (jumpingCoroutine == null && !stoppedCoroutines)
          {
               jumpingCoroutine = StartCoroutine(JumpTimer());
          }

          if (shootingFire == null && !stoppedCoroutines)
          {
               shootingFire = StartCoroutine(Fireballs());
          }

          if (movingCoroutine == null && !stoppedCoroutines)
          {
               movingCoroutine = StartCoroutine(RandomMovement());
          }
     }

     private void FixedUpdate()
     {
          if (isJumping && Time.fixedTime - jumpStart < jumpTime && !shouldDie)
          {
               rb.AddForce(new Vector2(0, 1000f));
          }
     }

     public IEnumerator JumpTimer()
     {
          float minWaitTime = 2.5f;
          float maxWaitTime = 4.5f;

          while (true)
          {
               yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
               if (!shouldDie)
               {
                    Jump();
               }             
          }
     }

     public IEnumerator RandomMovement()
     {
          float minWaitTime = 2.0f;
          float maxWaitTime = 4.0f;
          int moveDirection;

          while (true)
          {
               moveDirection = Random.Range(0, 2);
               myAnimator.SetBool("isMoving", true);
               if (moveDirection == 0)
               {
                    movingLeft = true;                    
                    yield return new WaitForSeconds(1.2f);
                    movingLeft = false;                    
               }
               else
               {
                    movingRight = true;
                    yield return new WaitForSeconds(1.5f);
                    movingRight = false;
               }
               myAnimator.SetBool("isMoving", false);

               yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
          }
     }

     private void Jump()
     {
          if (!stoppedCoroutines)
          {
               jumpStart = Time.time;
               rb.AddForce(new Vector2(0, 1000f));
               isJumping = true;
          }
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
          if (collision.collider.gameObject.layer == LayerMask.NameToLayer("groundLayer") && collision.contacts[0].normal.x == 0)
          {
               isJumping = false;
          }
     }

     public void Die()
     {
          GetComponent<Collider2D>().enabled = false;
          transform.rotation = new Quaternion(180, 0, 0, 0);
          rb.velocity = new Vector2(0.25f, 0.5f);
     }

     public void DeathCheck()
     {
          if (health <= 0)
          {
               shouldDie = true;
               StopCoroutine(jumpingCoroutine);
               StopCoroutine(shootingFire);
               Die();
          }
     }

     public IEnumerator Fireballs()
     {
          float minWaitTime = 2f;
          float maxWaitTime = 4f;

          while (true)
          {
               yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            sfx.PlaySoundEffect(fireBallSound);
               ShootFireball();
          }
     }

     private void ShootFireball()
     {
          if (!stoppedCoroutines)
          {
               GameObject bullet = Instantiate(fireball, new Vector2(transform.localPosition.x - 0.15f, transform.localPosition.y), transform.rotation);
               bullet.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, 0));
          }
     }
}
