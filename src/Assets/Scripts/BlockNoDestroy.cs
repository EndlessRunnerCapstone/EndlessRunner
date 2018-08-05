using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// This class is for Brick blocks that give 8 coins and do not break. At the end they turn into a block that cannot be hit again /// </summary>

public class BlockNoDestroy : MonoBehaviour
{

    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip coinSfx;
    private int hitCount = 0; //can be used to limit hits
    Vector3 originalPos;
    public Sprite afterHitSprite;
    public float coinMoveSpeed = 8;
    public float coinMoveHeight = 3;
    public float coinFallDistance = 2;

    // Use this for initialization
    void Start()
    {
        originalPos = transform.position;

    }

    void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = afterHitSprite;
    }

    void CreateCoin()
    {
        // Instantiate a prefab object with the spinning coin animation on it
        GameObject spinningCoin = (GameObject)Instantiate(Resources.Load("Prefabs/SpinningCoin", typeof(GameObject)));

        //set as a child to this object
        spinningCoin.transform.SetParent(this.transform.parent); 

        // set the spinningCoin object to just above the parent object
        spinningCoin.transform.position = new Vector3(originalPos.x, originalPos.y + 0.5f, originalPos.z);

        // move the coin!
        StartCoroutine(MoveCoin(spinningCoin)); 
    }


    IEnumerator OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (hitCount < 8)
            {
                // bounce block up and down
                transform.position += Vector3.up * Time.deltaTime; 
                yield return new WaitForSeconds(0.1f);
                transform.position = originalPos;

                // create coin, play sound effect, increment hit counter
                CreateCoin();
                sfx.PlaySoundEffect(coinSfx);
                hitCount++;

                // add scores
                ScoreKeeping.scoreValue += 200;
                CoinTracker.coinValue += 1;
            }

            // if hit count has maxed out, change sprite to "after hit sprite"
            else if (hitCount >= 8)
            {
                ChangeSprite();
            }
        }

    }

    // This function moves the coin up to a specified height and back down to original location
    IEnumerator MoveCoin(GameObject coin)
    {
        while (true)
        {
            coin.transform.position = new Vector3(coin.transform.position.x, coin.transform.position.y + coinMoveSpeed * Time.deltaTime);
            if (coin.transform.position.y >= originalPos.y + coinMoveHeight - 2f)
            {
                break;
            }
            yield return null;
        }

        while (true)
        {
            coin.transform.position = new Vector3(coin.transform.position.x, coin.transform.position.y - coinMoveSpeed * Time.deltaTime);
            if (coin.transform.position.y <= originalPos.y + coinFallDistance - 2f)
            {
                Destroy(coin.gameObject); // destroy object once it's in the original position
                break;
            }

            yield return null;
        }
    }

}

