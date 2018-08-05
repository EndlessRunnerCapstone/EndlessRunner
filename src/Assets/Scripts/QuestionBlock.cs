using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class controls the bounce and animation of the question block.
///  Once hit, it produces an animated coin that jumps out of the block and then is destroyed.
///  It increments score appropriately for player.
/// </summary>

public class QuestionBlock : Photon.MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip coinSfx;
    private bool canBounce = true;
    private Vector2 originalPosition;
    private int hitCount = 0;
    Vector3 originalPos;
    public Sprite afterHitSprite;
    public float coinMoveSpeed = 0.00001f;
    public float coinMoveHeight = -2f;
    public float coinFallDistance = 1f;

    // Use this for initialization
    void Start()
    {
        originalPos = transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void ChangeSprite()
    {
        GetComponent<Animator>().enabled = false; // turn off the idle animation

        // if the block was invisible, make it visible
        if (spriteRenderer.enabled == false)
        {
            spriteRenderer.enabled = true;
        }

        // change to after hit sprite
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (Globals.TwoPlayer)
            {
                photonView.RPC("HitInternal", PhotonTargets.All);
            }
            else
            {
                HitInternal();
            }
        }       
    }

    [PunRPC]
    void HitInternal()
    {
        if (hitCount == 0)
        {
            MoveBlock();
            ChangeSprite();
            CreateCoin();
            sfx.PlaySoundEffect(coinSfx);
            ScoreKeeping.scoreValue += 200;
            CoinTracker.coinValue += 1;
            hitCount++;
        }
    }

    // bounce the block up and back to original position
    IEnumerator MoveBlock()
    {
        transform.position += Vector3.up * Time.deltaTime; 
        yield return new WaitForSeconds(0.1f);
        transform.position = originalPos;
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
                Destroy(coin.gameObject);
                break;
            }

            yield return null;
        }
    }

}


