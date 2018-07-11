using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : Photon.MonoBehaviour {

	public float runSpeed;
     public float sprintSpeed;
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
     private Animator myAnimator;
     bool isRunning;

     private void Start()
     {
          runSpeed = 1.6f;
          sprintSpeed = 3f;
          rb = GetComponent<Rigidbody2D>();
          rb.gravityScale = 2;
          jumpForce = 4;
          stoppedJumping = true;
          jumpTime = .2f;
          jumpTimeCounter = jumpTime;
          isGrounded = true;
          noMoreJumping = false;
          myAnimator = GetComponent<Animator>();
     }

     // Update is called once per frame
     void Update () {
        if(!photonView.isMine && PhotonNetwork.connected)
        {
            return;
        }

		PlayerMove ();
          FlipPlayer();
          Animate();
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

          // PHYSICS
          if (Input.GetKey(KeyCode.LeftShift))
          {
               rb.velocity = new Vector2(moveX * sprintSpeed, rb.velocity.y);
          }
          else
          {
               rb.velocity = new Vector2(moveX * runSpeed, rb.velocity.y);
          }
          bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
          bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > 0;
          myAnimator.SetBool("Running", playerHasHorizontalSpeed);
          myAnimator.SetBool("Jumping", playerHasVerticalSpeed);

     }

	void Jump(){
          // JUMP CODE
          rb.velocity = new Vector2(rb.velocity.x, jumpForce);
          stoppedJumping = false;
	}

	void FlipPlayer(){
          bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
          if (playerHasHorizontalSpeed)
          {
               transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
          }
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

     void Animate()
     {
          if (rb.velocity.x != 0)
          {
               myAnimator.SetBool("Running", true);
          }
          else
          {
               myAnimator.SetBool("Running", false);
          }
     }
}
