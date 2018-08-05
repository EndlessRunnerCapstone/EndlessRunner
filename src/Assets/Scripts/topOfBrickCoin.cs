using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class controls the coins that sit on top of blocks
/// </summary>
public class topOfBrickCoin : MonoBehaviour {

    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip coinSfx;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            sfx.PlaySoundEffect(coinSfx);
            Destroy(gameObject);
            ScoreKeeping.scoreValue += 200;
            CoinTracker.coinValue += 1;
        }
    }
    
}
