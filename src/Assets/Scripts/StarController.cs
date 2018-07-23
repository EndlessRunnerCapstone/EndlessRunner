using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour {

     private bool isMovingLeft = false;
     public LayerMask groundLayer;
     private Rigidbody2D rb;
     public bool isGrounded = true;
     public Vector2 velocity;
     public float jumpPower;

	// Use this for initialization
	void Start () {
          rb = GetComponent<Rigidbody2D>();
          rb.freezeRotation = true;
          velocity.x = 0.8f;
          jumpPower = 3f;
	}
	
	// Update is called once per frame
	void Update () {
          UpdatePosition();
          GroundCheck();
          Bounce();
	}

     private void GroundCheck()
     {
          RaycastHit2D left, middle, right;
          left = Physics2D.Raycast(new Vector2(transform.localPosition.x - 0.05f, transform.localPosition.y - .06f), Vector2.down, 0.04f, groundLayer);
          middle = Physics2D.Raycast(new Vector2(transform.localPosition.x, transform.localPosition.y - .06f), Vector2.down, 0.04f, groundLayer);
          right = Physics2D.Raycast(new Vector2(transform.localPosition.x + 0.05f, transform.localPosition.y - .06f), Vector2.down, 0.04f, groundLayer);

          if (left.collider != null || middle.collider != null || right.collider != null)
          {
               isGrounded = true;
          }
          else
          {
               isGrounded = false;
          }
     }

     private void UpdatePosition()
     {
          Vector3 pos = transform.localPosition;
          Vector3 scale = transform.localScale;

               CheckWallCollision();
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

     private void CheckWallCollision()
     {
          bool leftCollision = Physics2D.Raycast(transform.position, Vector2.left, 0.09f, groundLayer);
          bool RightCollision = Physics2D.Raycast(transform.position, Vector2.right, 0.09f, groundLayer);

          if (leftCollision)
          {
               isMovingLeft = false;
          }
          else if (RightCollision)
          {
               isMovingLeft = true;
          }
     }

     private void Bounce()
     {
          if (isGrounded)
          {
               rb.velocity = new Vector2(rb.velocity.x, jumpPower);
          }
     }
          
}
