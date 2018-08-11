using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class animates a star out of a block once hit and then
 * instantiates a prefab star from the top of the block
 * */

public class StarBlock : MonoBehaviour
{

    Vector3 originalPos;
    public GameObject Star;     // object to be instantiated
    public GameObject Spawn;        // spawn point of our "fake mushroom"
    public Sprite afterHitSprite;
    public GameObject FakeStar;

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

                // Animate our "fake star" to appear to come out of the block
                FakeStar.GetComponent<Animator>().Play("FakeStar");
                yield return new WaitForSeconds(1.0f);

                // Destroy the "fake star" object and instantiate the real star
                Destroy(FakeStar);
                Instantiate(Star, Spawn.transform.position, Quaternion.identity);
            }

        }

    }
}
