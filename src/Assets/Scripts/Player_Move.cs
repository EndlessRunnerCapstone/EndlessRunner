using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : Photon.MonoBehaviour {

     //movement variables
	public float runSpeed;
     public float sprintSpeed;
     private float moveX;
     bool isRunning;
     public float fallMultiplier = 2;

     //jumping variables
     public float jumpForce;
     public float jumpTime;
     public float jumpTimeCounter;
     public bool stoppedJumping;
     public bool noMoreJumping;

     //game components
     private Rigidbody2D rb;
     private Animator myAnimator;

     //ground check variables
     public bool isGrounded;
     public LayerMask groundLayer;
     public GameObject leftCast;
     public GameObject rightCast;
     public GameObject upCast;
     

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
          if (!photonView.isMine && PhotonNetwork.connected)
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
          GroundCheck();
          CollisionCheckAbove();
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

          // PHYSICS
          if (Input.GetKey(KeyCode.LeftShift))
          {
               rb.velocity = new Vector2(moveX * sprintSpeed, rb.velocity.y);
          }
          else
          {
               rb.velocity = new Vector2(moveX * runSpeed, rb.velocity.y);
          }
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

     void Animate()
     {
          bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
          bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > 0;

          if (playerHasHorizontalSpeed)
          {
               myAnimator.SetBool("Running", true);
          }
          else
          {
               myAnimator.SetBool("Running", false);
          }

          if (playerHasVerticalSpeed)
          {
               myAnimator.SetBool("Jumping", true);
          }
          else
          {
               myAnimator.SetBool("Jumping", false);
          }
     }

     void GroundCheck()
     {
          bool groundedLeft = Physics2D.Raycast(leftCast.transform.position, Vector2.down, 0.03f, groundLayer);
          bool groundedRight = Physics2D.Raycast(rightCast.transform.position, Vector2.down, 0.03f, groundLayer);
          isGrounded = groundedLeft || groundedRight;

          if (isGrounded)
          {
               jumpTimeCounter = jumpTime;
               noMoreJumping = false;
          }
     }

     void CollisionCheckAbove()
     {
          bool hitAbove = Physics2D.Raycast(upCast.transform.position, Vector2.up, 0.03f, groundLayer);
          if (hitAbove)
          {
               jumpTimeCounter = 0;
          }
     }
}
