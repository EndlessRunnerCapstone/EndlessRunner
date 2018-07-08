using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

	public float playerSpeed = 10;
	private bool facingRight = false;
	public float jumpForce;
     public float jumpTime;
     public float jumpTimeCounter;
	private float moveX;
     private Rigidbody2D rb;
     public bool stoppedJumping;
     public bool isGrounded;
     public bool noMoreJumping;
     public float fallMultiplier = 2;

     private void Start()
     {
          playerSpeed = 2.1f;
          rb = GetComponent<Rigidbody2D>();
          rb.gravityScale = 2;
          jumpForce = 4;
          stoppedJumping = true;
          jumpTime = .2f;
          jumpTimeCounter = jumpTime;
          isGrounded = true;
          noMoreJumping = false;
     }

     // Update is called once per frame
     void Update () {
		PlayerMove ();
          if (Input.GetButtonDown("Jump"))
          {
               if (isGrounded)
               {
                    Jump();
               }               
          }


          if (Input.GetButtonUp("Jump"))
          {
               jumpTimeCounter = 0;
               stoppedJumping = true;
               noMoreJumping = true;
          }

          if (rb.velocity.y < 0)
          {
               rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
          }
     }

     private void FixedUpdate()
     {
          if (Input.GetButton("Jump") && !stoppedJumping && !noMoreJumping)
          {
               if (jumpTimeCounter > 0) 
               {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
               }
          }
     }

     void PlayerMove ()
	{
		// CONTROLS
		moveX = Input.GetAxis ("Horizontal");
          // ANIMATIONS

          // PLAYER DIRECTION
          if (moveX < 0.0f && facingRight == false) {
			FlipPlayer();
		} else if (moveX > 0.0f && facingRight == true) {
			FlipPlayer();
		}

		// PHYSICS
		rb.velocity = new Vector2 (moveX * playerSpeed, rb.velocity.y);
	}

	void Jump(){
          // JUMP CODE
          rb.velocity = new Vector2(rb.velocity.x, jumpForce);
          stoppedJumping = false;
	}

	void FlipPlayer(){
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

     private void OnCollisionStay2D(Collision2D collision)
     {
          if (collision.collider.gameObject.layer == 9)
          {
               isGrounded = true;
               jumpTimeCounter = jumpTime;
               noMoreJumping = false;
          }
     }

     private void OnCollisionExit2D(Collision2D collision)
     {
          if (collision.collider.gameObject.layer == 9)
          {
               isGrounded = false;
          }
     }
}
