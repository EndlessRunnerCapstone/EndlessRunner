using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerController : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    private AudioSource sfxPlayer;
    public AudioClip powerUpSound;

    // Use this for initialization
    private void Awake () {
        sfxPlayer = GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySoundEffect(AudioClip sfx)
    {
        sfxPlayer.PlayOneShot(sfx);
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.GetComponent<BoxCollider2D>().gameObject.layer == LayerMask.NameToLayer("Player"))
          {
               collision.gameObject.GetComponent<Player_Move>().isBig = true;
               collision.gameObject.GetComponent<Player_Move>().fireFlower = true;

            // briefly disable sprite renderer so it appears as though it disappeared
               spriteRenderer.enabled = false;

               // Play sound effect and wait long enough for it to play
               PlaySoundEffect(powerUpSound);
               yield return new WaitForSeconds(2f);

               // Destroy object
               Destroy(this.gameObject);
          }
     }
}
