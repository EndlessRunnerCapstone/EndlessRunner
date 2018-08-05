using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class creates a flower out of a block at the spawn point
 */

public class FlowerBlock : MonoBehaviour
{

    Vector3 originalPos;
    public GameObject Flower;     // object to be instantiated
    public GameObject Spawn;        // spawn point of our "fake mushroom"
    public Sprite afterHitSprite;

    // sound effects variabless
    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip powerUpSound;
    private Animator anim;
    private bool canHit = true;

    // Use this for initialization
    void Start()
    {
        originalPos = transform.position;
    }

    void ChangeSprite()
    {
        // turn off the question block animation
        GetComponent<Animator>().enabled = false;

        // change sprite renderer to "after hit" sprite
        GetComponent<SpriteRenderer>().sprite = afterHitSprite;
    }

    IEnumerator OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (canHit)
            {
                // Bounce the block up slightly
                transform.position += Vector3.up * Time.deltaTime;
                yield return new WaitForSeconds(0.1f);
                transform.position = originalPos;

                // Play sound effect and change sprite to "after hit" sprite
                sfx.PlaySoundEffect(powerUpSound);
                ChangeSprite();
                canHit = false; // can't hit again

                // Instantiate Flower at spawn point
                Instantiate(Flower, Spawn.transform.position, Quaternion.identity);
            }

        }

    }
}
