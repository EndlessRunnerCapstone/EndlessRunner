using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class Player_Move : Photon.MonoBehaviour, IPunObservable {

    public static GameObject LocalPlayerInstance;

    // sound variables
    private AudioSource sfxPlayer;
    public AudioClip smallJumpSound;
    public AudioClip bigJumpSound;
    public AudioClip gameOverSound;
    public AudioClip powerUpSound;
    public AudioClip loseBigSound;
    private AudioSource[] allAudioSources;

    //movement variables
    private float runSpeed;
    private float sprintSpeed;
    private float moveX;
    private bool isRunning;
    private float fallMultiplier;
    public static int PlayerState;

     //jumping variables
     private float jumpForce;
     private float jumpTime;
     public float jumpTimeCounter;
     private bool stoppedJumping;
     private bool noMoreJumping;

     //game components
     private Rigidbody2D rb;
     private Animator myAnimator;

     //ground check variables
     public bool isGrounded;
     public LayerMask floorMask;

     //Animation bools
     public bool isBig = false;
     public bool fireFlower = false;

     //Invincibility
     [SerializeField] private bool invincible;
     float invincibilityTime = 2f;
     float flickerTime = 0.1f;
     private bool starPower;
     float currentTime;
     private bool turtleInvincible;

     //Death
     private bool isDead;
     Coroutine hasStarPower = null;
     Coroutine turtleInvincibility = null;

     //Fireball
     public GameObject fireball;
     private float nextFire = -1f;


    //Network
    private Vector2 networkPosition;
    private float networkRotation;

    private bool networkSet = false;
    private bool peerReadyToReload = false;
    private bool readyToReload = false;

    public void PlaySoundEffect(AudioClip sfx)
    {
        sfxPlayer.PlayOneShot(sfx);
    }

    private void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    private void Awake()
    {
        PhotonNetwork.sendRateOnSerialize = 20;

        sfxPlayer = GetComponent<AudioSource>();

        if (!PhotonNetwork.connected || photonView.isMine)
        {
            LocalPlayerInstance = this.gameObject;
        }
    }

    public void ResetMario()
    {
          isDead = false;
          isBig = false;
          GroundCheck();
          invincible = false;
          starPower = false;
          if(hasStarPower != null)
          {
               StopCoroutine(hasStarPower);
          }
          myAnimator.SetBool("starPower", false);
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
          PlayerState = 0; // indicates base state of player
          currentTime = -1;
     }

     // Update is called once per frame
     void Update () {
          if (!photonView.isMine && PhotonNetwork.connected)
          {
            return;
          }

          if (!isDead)
          {
               PlayerMove();
               FlipPlayer();
               GroundCheck();
               CollisionCheckAbove();
               CheckSideCollision();
          }

          Animate();

          if (!isDead)
          {
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

          if (Input.GetKeyDown(KeyCode.W) && Time.time > nextFire && fireFlower)
          {
               ShootFireball();
          }

          if (currentTime + 0.3f < Time.time)
          {
               turtleInvincible = false;
          }

    }

    private void FixedUpdate()
     {
        if (Globals.TwoPlayer && !photonView.isMine && networkSet)
        {
            rb.position = Vector2.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime);
        }
        else
        {
            if (Input.GetButton("Jump") && !stoppedJumping && !noMoreJumping && !isDead)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
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

        if (isBig)
        {
            PlaySoundEffect(bigJumpSound);
        }
        else
        {
            PlaySoundEffect(smallJumpSound);
        }

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


          if (fireFlower)
          {
               myAnimator.SetBool("fireFlower", true);
        }
          else
          {
               myAnimator.SetBool("fireFlower", false);
          }

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
               myAnimator.SetBool("isBig", false);
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
               else if (hitRay.collider.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
               {
                    if (starPower)
                    {
                         if (hitRay.collider.tag == "Goomba")
                         {
                              hitRay.collider.GetComponent<GoombaController>().StarDeath();
                         }
                    }
                    else
                    {
                         if (hitRay.collider.tag == "Goomba")
                         {
                              Debug.Log(hitRay);
                              hitRay.collider.GetComponent<GoombaController>().Death();
                         }

                         if (Input.GetButton("Jump"))
                         {
                              rb.velocity = new Vector2(rb.velocity.x, 7);
                         }
                         else
                         {
                              rb.velocity = new Vector2(rb.velocity.x, 2);
                         }
                    }
               }
          }
          else
          {
               if (transform.parent != null)
               {
                    jumpTimeCounter = jumpTime;
                    noMoreJumping = false;
                    isGrounded = true;
               }
               else
               {
                    isGrounded = false;
               }
          }
     }

     void CollisionCheckAbove()
     {
          RaycastHit2D hitAbove;
          if (!isBig)
          {
               hitAbove = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.07f), Vector2.up, 0.04f, floorMask);
          }
          else
          {
               hitAbove = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.145f), Vector2.up, 0.03f, floorMask);
          }

          if (hitAbove)
          {
               Debug.Log(hitAbove);
               jumpTimeCounter = 0;
               stoppedJumping = true;
               noMoreJumping = true;
               rb.velocity = new Vector2(rb.velocity.x, 0);


               if (hitAbove.collider.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
               {
                    OnEnemyHit(hitAbove);
               }
          }
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
          if (!isDead)
          {
               if (collision.gameObject.tag == "RedMushroom")
               {
                   ScoreKeeping.scoreValue += 1000;
                   PlaySoundEffect(powerUpSound);
                   isBig = true;
                   Destroy(collision.gameObject);
                   PlayerState = 1;
               }
               else if (collision.gameObject.tag == "Star")
               {
                    Destroy(collision.gameObject);
                    if (!starPower)
                    {
                         hasStarPower = StartCoroutine(StarPower());
                         PlaySoundEffect(powerUpSound);
                    }
               }
               else if (collision.gameObject.tag == "LifeMushroom") {
                    Globals.PlayerOne.NumberOfLives++;
                    Globals.PlayerTwo.NumberOfLives++;
                    PlaySoundEffect(powerUpSound);
                    Destroy(collision.gameObject);
               }
          }

          if (collision.gameObject.tag == "MovingPlatform")
          {
               {
                    transform.parent = collision.transform;
               }
          }
          else if (collision.gameObject.tag == "Bowser")
          {
               HitByBowser();
          }
     }

     private void CheckSideCollision()
     {
          RaycastHit2D hitRay, upLeft, midLeft, botLeft, upRight, midRight, botRight;
          if (!isBig)
          {
               upLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.035f, transform.localPosition.y + 0.06f), Vector2.left, 0.055f, floorMask);
               midLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.035f, transform.localPosition.y), Vector2.left, 0.055f, floorMask);
               botLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.035f, transform.localPosition.y - 0.06f), Vector2.left, 0.055f, floorMask);
               upRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.035f, transform.localPosition.y + 0.06f), Vector2.right, 0.055f, floorMask);
               midRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.035f, transform.localPosition.y), Vector2.right, 0.055f, floorMask);
               botRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.035f, transform.localPosition.y - 0.06f), Vector2.right, 0.055f, floorMask);
          }
          else
          {
               upLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.04f, transform.localPosition.y + 0.12f), Vector2.left, 0.055f, floorMask);
               midLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.04f, transform.localPosition.y), Vector2.left, 0.055f, floorMask);
               botLeft = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.04f, transform.localPosition.y - 0.12f), Vector2.left, 0.05f, floorMask);
               upRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.04f, transform.localPosition.y + 0.12f), Vector2.right, 0.055f, floorMask);
               midRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.04f, transform.localPosition.y), Vector2.right, 0.055f, floorMask);
               botRight = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.04f, transform.localPosition.y - 0.12f), Vector2.right, 0.055f, floorMask);
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

               if (hitRay.collider.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
               {
                    OnEnemyHit(hitRay);
               }
          }
     }

     public IEnumerator Invincible()
     {
          isBig = false;
          fireFlower = false;
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
     }

     void OnEnemyHit(RaycastHit2D hitRay)
     {
          if(Globals.TwoPlayer && !photonView.isMine)
          {
              return;
          }

          if (starPower)
          {
               if (hitRay.collider.tag == "Goomba")
               {
                    hitRay.collider.gameObject.GetComponent<GoombaController>().StarDeath();
               }
               else if (hitRay.collider.tag == "Turtle")
               {
                    hitRay.collider.gameObject.GetComponent<TurtleController>().StarDeath();
               }
          }
          else
          {
               if((hitRay.collider.tag == "Turtle" && hitRay.collider.gameObject.GetComponent<TurtleController>().state == TurtleController.EnemyState.shellIdle) ||
                    (hitRay.collider.tag == "FlyingTurtle" && hitRay.collider.gameObject.GetComponent<FlyingTurtleController>().state == FlyingTurtleController.EnemyState.shellIdle))
               {
                    turtleInvincible = true;
                    currentTime = Time.time;

               }
               else if (!invincible)
               {
                    if ((hitRay.collider.tag == "Turtle" || hitRay.collider.tag == "FlyingTurtle") && turtleInvincible)
                    {
                         return;
                    }

                    if (isBig)
                    {

                         PlaySoundEffect(loseBigSound);
                         StartCoroutine(Invincible());
                         PlayerState = 0;
                    }
                    else
                    {
                         if (!isDead)
                         {
                              StartCoroutine(Die());
                         }
                    }
               }
          }
     }

     public IEnumerator Die()
     {
          StopAllAudio();
          PlaySoundEffect(gameOverSound);
          rb.gravityScale = 0.9f;
          myAnimator.SetBool("isDead", true);
          isDead = true;
          GetComponent<BoxCollider2D>().enabled = false;
          rb.velocity = new Vector2(0, 5f);
          yield return new WaitForSeconds(3);
          GetComponent<BoxCollider2D>().enabled = true;
          myAnimator.SetBool("isDead", false);
          myAnimator.SetBool("starPower", false);
          rb.gravityScale = 2;
          Globals.PlayerTwo.NumberOfLives--;
          Globals.PlayerOne.NumberOfLives--;


          // Reset Timer
          TimeKeeping.timeValue = 400;

        if (!Globals.TwoPlayer)
        {
               if (Globals.PlayerOne.NumberOfLives > 0)
               {
                
                  SceneManager.LoadScene("LoadingScreen1");
     
               }
               else
               {
                    Globals.PlayerOne.NumberOfLives = 3;
                    Globals.PlayerTwo.NumberOfLives = 3;

                    SceneManager.LoadScene("GameOver");
            }
        }
        else
        {
            if (Globals.PlayerOne.NumberOfLives > 0)
               {
                    GameObject.FindWithTag("GameManager").GetComponent<GameManager>().ReloadScene(SceneManager.GetActiveScene().name);
               }
            else
               {
                    GameObject.FindWithTag("GameManager").GetComponent<GameManager>().ReloadScene("GameOver");
               }            
        }
    }

    private IEnumerator StarPower()
     {
          starPower = true;
          myAnimator.SetBool("starPower", true);
          yield return new WaitForSeconds(10);
          starPower = false;
          myAnimator.SetBool("starPower", false);
     }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Just in case we are called before we are setup
        if(rb == null)
        {
            return;
        }

        if(stream.isWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
            stream.SendNext(rb.velocity);
            stream.SendNext(readyToReload);
        }
        else
        {
            networkPosition = (Vector2)stream.ReceiveNext();
            networkRotation = (float)stream.ReceiveNext();
            rb.velocity = (Vector2)stream.ReceiveNext();
            peerReadyToReload = (bool)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.time - info.timestamp));
            networkPosition += (rb.velocity * lag);
            networkSet = true;
        }
    }

     public void ShootFireball()
     {
          nextFire = Time.time + 0.8f;
          if (transform.localScale.x == -1f)
          {
               GameObject bullet = Instantiate(fireball, new Vector2(transform.localPosition.x - 0.15f, transform.localPosition.y), transform.rotation);
               bullet.GetComponent<Rigidbody2D>().velocity = (new Vector2(-3f, -2.5f));

          }
          else
          {
               GameObject bullet = Instantiate(fireball, new Vector2(transform.localPosition.x + 0.15f, transform.localPosition.y), transform.rotation);
               bullet.GetComponent<Rigidbody2D>().velocity = (new Vector2(3f, -2.5f));
          }
     }

     private void OnCollisionExit2D(Collision2D collision)
     {
          if (collision.gameObject.tag == "MovingPlatform")
          {
               transform.parent = null;
          }
     }

     public void HitByBowser()
     {
          if (!invincible)
          {
               if (isBig)
               {
                    PlaySoundEffect(loseBigSound);
                    StartCoroutine(Invincible());
               }
               else
               {
                    if (!isDead)
                    {
                         StartCoroutine(Die());
                    }
               }
          }
     }
}
