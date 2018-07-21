using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Move : Photon.MonoBehaviour {

    public static GameObject LocalPlayerInstance;

     //movement variables
	public float runSpeed;
     public float sprintSpeed;
     private float moveX;
     bool isRunning;
     public float fallMultiplier;     

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
     public LayerMask floorMask;    

     //Animation bools
     public bool isBig = false;

     //Invincibility
     private bool invincible;
     float invincibilityTime = 3f;
     float flickerTime = 0.1f;


    private void Awake()
    {
        if(!PhotonNetwork.connected || photonView.isMine)
        {
            LocalPlayerInstance = this.gameObject;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
     {
        CameraControl cameraControl = this.gameObject.GetComponent<CameraControl>();

        if(cameraControl != null)
        {
            if(!PhotonNetwork.connected || photonView.isMine)
            {
                cameraControl.OnStartFollowing();
            }
        }

          runSpeed = 1.3f;
          sprintSpeed = 2.5f;
          rb = GetComponent<Rigidbody2D>();
          rb.gravityScale = 2;
          jumpForce = 4;
          stoppedJumping = true;
          jumpTime = .2f;
          jumpTimeCounter = jumpTime;
          isGrounded = true;
          noMoreJumping = false;
          myAnimator = GetComponent<Animator>();
          fallMultiplier = 1.5f;
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
          GroundCheck();
          CollisionCheckAbove();
          CheckSideCollision();


          if (Input.GetButtonDown("Jump"))
          {               
               if (isGrounded)
               {
                    Jump();
                    isGrounded = false;
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

          if (isBig)
          {
               myAnimator.SetBool("isBig", true);
               if (playerHasHorizontalSpeed == true)
               {
                    myAnimator.SetBool("Running", true);
               }
               else
               {
                    myAnimator.SetBool("Running", false);
               }

               if (playerHasVerticalSpeed == true)
               {
                    myAnimator.SetBool("Jumping", true);
               }
               else
               {
                    myAnimator.SetBool("Jumping", false);
               }
          }
          else
          {
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
     }

     void GroundCheck()
     {
          RaycastHit2D left, middle, right;
          if (isBig)
          {
               left = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.05f, transform.localPosition.y - 0.145f), Vector2.down, 0.04f, floorMask);
               middle = Physics2D.Raycast(new Vector2(transform.localPosition.x, transform.localPosition.y - 0.145f), Vector2.down, 0.04f, floorMask);
               right = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.05f, transform.localPosition.y - 0.145f), Vector2.down, 0.04f, floorMask);
          }
          else
          {
               left = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.05f, transform.localPosition.y - 0.06f), Vector2.down, 0.04f, floorMask);
               middle = Physics2D.Raycast(new Vector2(transform.localPosition.x, transform.localPosition.y - 0.06f), Vector2.down, 0.04f, floorMask);
               right = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.05f, transform.localPosition.y - 0.06f), Vector2.down, 0.04f, floorMask);
          }



          if (left.collider != null || middle.collider != null || right.collider != null)
          {
               RaycastHit2D hitRay = right;

               if (left)
               {
                    hitRay = left;
               }
               else if (middle)
               {
                    hitRay = middle;
               }
               else if (right)
               {
                    hitRay = right;
               }

               if (hitRay.collider.gameObject.layer == 9)
               {
                    jumpTimeCounter = jumpTime;
                    noMoreJumping = false;
                    isGrounded = true;
               }

               if (hitRay.collider.tag == "Enemy")
               {
                    hitRay.collider.GetComponent<GoombaController>().Death();
                    if (Input.GetButton("Jump"))
                    {
                         rb.velocity = new Vector2(rb.velocity.x, 7);
                    }
                    else
                    {
                         rb.velocity = new Vector2(rb.velocity.x, 1);
                    }
               }
          }
          else
          {
               isGrounded = false;
          }
     }

     void CollisionCheckAbove()
     {
          RaycastHit2D hitAbove;
          if (!isBig)
          {
               hitAbove = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.06f), Vector2.up, 0.03f, floorMask);
          }
          else
          {
              hitAbove = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.145f), Vector2.up, 0.03f, floorMask);
          }

          if (hitAbove)
          {
               jumpTimeCounter = 0;

               if (hitAbove.collider.tag == "Enemy")
               {
                    OnEnemyHit(hitAbove);
               }
          }
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
          if (collision.gameObject.tag == "RedMushroom")
          {
               isBig = true;
               Destroy(collision.gameObject);
          }
     }

     private void CheckSideCollision()
     {
          RaycastHit2D hitRay, upLeft, midLeft, botLeft, upRight, midRight, botRight;
          if (!isBig)
          {
               upLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.035f, transform.localPosition.y + 0.06f), Vector2.left, 0.04f, floorMask);
               midLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.035f, transform.localPosition.y), Vector2.left, 0.04f, floorMask);
               botLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.035f, transform.localPosition.y - 0.06f), Vector2.left, 0.04f, floorMask);
               upRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.035f, transform.localPosition.y + 0.06f), Vector2.right, 0.04f, floorMask);
               midRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.035f, transform.localPosition.y), Vector2.right, 0.04f, floorMask);
               botRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.035f, transform.localPosition.y - 0.06f), Vector2.right, 0.04f, floorMask);
          }
          else
          {
               upLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.04f, transform.localPosition.y + 0.12f), Vector2.left, 0.04f, floorMask);
               midLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.04f, transform.localPosition.y), Vector2.left, 0.04f, floorMask);
               botLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.04f, transform.localPosition.y - 0.12f), Vector2.left, 0.04f, floorMask);
               upRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.04f, transform.localPosition.y + 0.12f), Vector2.right, 0.04f, floorMask);
               midRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.04f, transform.localPosition.y), Vector2.right, 0.04f, floorMask);
               botRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.04f, transform.localPosition.y - 0.12f), Vector2.right, 0.04f, floorMask);
          }

          hitRay = upLeft;
          if (upLeft.collider != null || midLeft.collider != null || botLeft.collider != null || upRight.collider != null || midRight.collider != null || botRight.collider != null)
          {
               if (upLeft.collider != null)
               {
                    hitRay = upLeft;
               }
               else if (midLeft.collider != null)
               {
                    hitRay = midLeft;
               }
               else if (botLeft.collider != null)
               {
                    hitRay = botLeft;
               }
               else if (upRight.collider != null)
               {
                    hitRay = upRight;
               }
               else if (midRight.collider != null)
               {
                    hitRay = midRight;
               }
               else if (botRight.collider != null)
               {
                    hitRay = botRight;
               }
          }

          if (hitRay && hitRay.collider.tag == "Enemy")
          {
               OnEnemyHit(hitRay);
          }

     }

     public IEnumerator Invincible()
     {
          isBig = false;
          float time = 0f;
          bool showSprite = false;
          invincible = true;
          

          while (time < invincibilityTime)
          {
               GetComponent<SpriteRenderer>().enabled = showSprite;
               yield return new WaitForSeconds(flickerTime);
               showSprite = !showSprite;
               time = time + flickerTime;
          }

          GetComponent<SpriteRenderer>().enabled = true;
          invincible = false;
          GroundCheck();
     }

     void OnEnemyHit(RaycastHit2D hitRay)
     {
          //TODO: Multiplayer
          if(Globals.TwoPlayer)
          {
              return;
          }

          if(!invincible)
          {
               if (isBig)
               {
                    StartCoroutine(Invincible());
               }
               else
               {
                    SceneManager.LoadScene("Level01");
               }
          }
     }
}
