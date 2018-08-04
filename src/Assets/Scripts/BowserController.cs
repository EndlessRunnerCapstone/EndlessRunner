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
     [SerializeField] private GameObject fireball;

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
	}
	
	// Update is called once per frame
	void Update () {
          if (!shouldDie)
          {
               DeathCheck();
          }

          if (jumpingCoroutine == null)
          {
               jumpingCoroutine = StartCoroutine(JumpTimer());
          }

          if (shootingFire == null)
          {
               shootingFire = StartCoroutine(Fireballs());
          }
     }

     private void FixedUpdate()
     {
          if (isJumping && Time.fixedTime - jumpStart < jumpTime && !shouldDie)
          {
               rb.AddForce(new Vector2(0, 1100f));
          }
     }

     private IEnumerator JumpTimer()
     {
          float minWaitTime = 3.5f;
          float maxWaitTime = 6.5f;

          while (true)
          {
               yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
               if (!shouldDie)
               {
                    Jump();
               }             
          }
     }

     private void Jump()
     {
          jumpStart = Time.time;
          rb.AddForce(new Vector2(0, 1100f));
          isJumping = true;
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

     private IEnumerator Fireballs()
     {
          float minWaitTime = 2f;
          float maxWaitTime = 4f;

          while (true)
          {
               yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
               ShootFireball();
          }
     }

     private void ShootFireball()
     {
          GameObject bullet = Instantiate(fireball, new Vector2(transform.localPosition.x - 0.15f, transform.localPosition.y), transform.rotation);
          bullet.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1.5f, 0));
     }
}
