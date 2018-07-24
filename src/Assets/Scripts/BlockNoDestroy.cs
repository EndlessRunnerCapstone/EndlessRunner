using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// This class is for Brick blocks that give 8 coins and do not break. At the end they turn into a block that cannot be hit again /// </summary>

public class BlockNoDestroy : MonoBehaviour
{

    [SerializeField]
    SoundEffectsManager sfx;
    [SerializeField]
    AudioClip coinSfx;
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
        GameObject spinningCoin = (GameObject)Instantiate(Resources.Load("Prefabs/SpinningCoin", typeof(GameObject)));
        spinningCoin.transform.SetParent(this.transform.parent);
        spinningCoin.transform.position = new Vector3(originalPos.x, originalPos.y + 0.5f, originalPos.z);
        StartCoroutine(MoveCoin(spinningCoin));
    }


    IEnumerator OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {

            if (hitCount < 8)
            {
                transform.position += Vector3.up * Time.deltaTime; //possibly change this to make it more dramatic
                yield return new WaitForSeconds(0.1f);
                transform.position = originalPos;
                CreateCoin();
                sfx.PlaySoundEffect(coinSfx);
                hitCount++;
            }
            else if (hitCount >= 8)
            {
                ChangeSprite();
            }
        }

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

