using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

	public float runSpeed;
     public float sprintSpeed;	
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
     public GameObject leftCast;
     public GameObject rightCast;
     public LayerMask groundLayer;
     

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
          leftCast = GameObject.Find("LeftRaycast");
          rightCast = GameObject.Find("RightRaycast");
     }

     // Update is called once per frame
     void Update () {
		PlayerMove ();
          Animate();
          FlipPlayer();          
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


          groundCheck();
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
          myAnimator.SetBool("Running", playerHasHorizontalSpeed);
          myAnimator.SetBool("Jumping", playerHasVerticalSpeed);
     }

     void groundCheck()
     {
          bool leftGrounded = Physics2D.Raycast(leftCast.transform.position, Vector2.down, 0.03f, groundLayer);
          bool rightGrounded = Physics2D.Raycast(rightCast.transform.position, Vector2.down, 0.03f, groundLayer);
          Debug.Log(leftGrounded);
          Debug.Log(rightGrounded);
          if (leftGrounded || rightGrounded)
          {
               isGrounded = true;
               jumpTimeCounter = jumpTime;
               noMoreJumping = false;
          }
          else
          {
               isGrounded = false;
          }
     }
}
