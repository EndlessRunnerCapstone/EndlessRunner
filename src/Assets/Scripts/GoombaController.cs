using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour {

     public float gravity;
     public Vector2 velocity;
     public bool isWalkingLeft = true;
     public LayerMask groundLayer;
     private Rigidbody2D rb;

     private bool grounded = false;

     private enum EnemyState
     {
          walking,           
          dead
     }

     private EnemyState state = EnemyState.walking;

	// Use this for initialization
	void Start () {
          rb = GetComponent<Rigidbody2D>();
          rb.freezeRotation = true;
          enabled = false;          
          gravity = 60f;
          velocity.x = 0.5f;
	}
	
	// Update is called once per frame
	void Update ()
     { 
          UpdateEnemyPosition();
	}

     private void OnBecameVisible()
     {
          enabled = true;
     }


     void Fall()
     {          
          grounded = false;
     }

     void UpdateEnemyPosition()
     {
          if (state != EnemyState.dead)
          {
               Vector3 pos = transform.localPosition;
               Vector3 scale = transform.localScale;

               if (state == EnemyState.walking)
               {
                    CheckWallCollision();
                    if (isWalkingLeft)
                    {
                         pos.x -= velocity.x * Time.deltaTime;                         
                    }
                    else
                    {
                         pos.x += velocity.x * Time.deltaTime;
                    }                    
               }

               if (velocity.y <= 0)
               {
                    pos = CheckGround(pos);
               }

               transform.localPosition = pos;
               transform.localScale = scale;
          }
     }


     Vector3 CheckGround(Vector3 pos)
     {
          Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - 0.5f);
          Vector2 originMiddle = new Vector2(pos.x, pos.y - 0.5f);
          Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y - 0.5f);

          RaycastHit2D groundLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, groundLayer);
          RaycastHit2D groundMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, groundLayer);
          RaycastHit2D groundRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, groundLayer);

          if(groundLeft.collider != null || groundMiddle.collider != null || groundRight.collider != null)
          {    grounded = true;
               velocity.y = 0;
               state = EnemyState.walking;
          }
          return pos;
     }

     void CheckWallCollision()
     {
          bool leftCollision = Physics2D.Raycast(transform.position, Vector2.left, 0.09f, groundLayer);
          bool RightCollision = Physics2D.Raycast(transform.position, Vector2.right, 0.09f, groundLayer);

          if (leftCollision)
          {
               isWalkingLeft = false;
          }
          else if (RightCollision)
          {
               isWalkingLeft = true;
          }
     }     
}
