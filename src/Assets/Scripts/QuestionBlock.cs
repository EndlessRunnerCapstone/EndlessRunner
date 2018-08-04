using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls bounce of question mark block
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

    IEnumerator QuestionBlockBounce()
    {

        if (canBounce)
        {
            canBounce = false;
            transform.position += Vector3.up * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
            transform.position = originalPos;
        }
    }

    void ChangeSprite()
    {
        GetComponent<Animator>().enabled = false;

        if (spriteRenderer.enabled == false)
        {
            spriteRenderer.enabled = true;
        }

        GetComponent<SpriteRenderer>().sprite = afterHitSprite;
    }

    void CreateCoin()
    {
        GameObject spinningCoin = (GameObject)Instantiate(Resources.Load("Prefabs/SpinningCoin", typeof(GameObject)) as GameObject);
        spinningCoin.transform.SetParent(this.transform.parent);
        spinningCoin.transform.position = new Vector3(originalPos.x, originalPos.y + 0.5f, originalPos.z);
        StartCoroutine(MoveCoin(spinningCoin));
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(Globals.TwoPlayer)
        {
            photonView.RPC("HitInternal", PhotonTargets.All);   
        }
        else
        {
            HitInternal();
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

    IEnumerator MoveBlock()
    {
        transform.position += Vector3.up * Time.deltaTime; //possibly change this to make it more dramatic
        yield return new WaitForSeconds(0.1f);
        transform.position = originalPos;
    }

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


