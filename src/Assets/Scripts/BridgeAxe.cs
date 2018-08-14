using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeAxe : MonoBehaviour {

     [SerializeField] private GameObject[] bridgeBlocks = new GameObject[17];
     public GameObject bowser;
    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip bowserDiesSound;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.gameObject.tag == "Player")
          { 
                
               StartCoroutine(DestroyBridge());
            spriteRenderer.enabled = false;
          }
     }

     private IEnumerator DestroyBridge()
     {
        CameraControl.lockX = true;
          bowser.GetComponent<BowserController>().stoppedCoroutines = true;
          StopCoroutine(bowser.GetComponent<BowserController>().Fireballs());
          StopCoroutine(bowser.GetComponent<BowserController>().RandomMovement());
          StopCoroutine(bowser.GetComponent<BowserController>().JumpTimer());
          sfx.PlaySoundEffect(bowserDiesSound);

        for (int i = 0; i < bridgeBlocks.Length; i++)
          {
               ScoreKeeping.scoreValue += 2000;
               Destroy(bridgeBlocks[i]);
               yield return new WaitForSeconds(0.05f);
          }

        CameraControl.lockX = false;
     }
}
