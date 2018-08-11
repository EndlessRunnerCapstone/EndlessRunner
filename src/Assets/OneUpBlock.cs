using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class animates a mushroom out of a block once hit and then
 * instantiates a prefab mushroom from the top of the block
 * */

public class OneUpBlock : MonoBehaviour
{

    Vector3 originalPos;
    public GameObject Mushroom;
    public GameObject Spawn;        // spawn point of our "fake mushroom"
    public Sprite afterHitSprite;
    public GameObject FakeMushroom;
    SpriteRenderer spriteRenderer;

    // sound effects variabless
    [SerializeField]
    SoundEffectsManager sfx;
    [SerializeField]
    AudioClip powerUpSound;
    private Animator anim;
    private bool canHit = true;


    void Start()
    {
        originalPos = transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void ChangeSprite()
    {
        // turn off the question blcok animation
        GetComponent<Animator>().enabled = false;

        // if the block was invisible, enable the sprite renderer again
        if (spriteRenderer.enabled == false)
        {
            spriteRenderer.enabled = true;
        }

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
                    // Animate our "fake mushroom" to appear to come out of the block
                    FakeMushroom.GetComponent<Animator>().Play("FakeMushroom");
                    yield return new WaitForSeconds(1.0f);

                    // Destroy the "fake mushroom" object and instantiate the real mushroom
                    Destroy(FakeMushroom);
                    Instantiate(Mushroom, Spawn.transform.position, Quaternion.identity);

            }

        }

    }
}
