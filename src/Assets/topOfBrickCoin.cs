﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topOfBrickCoin : MonoBehaviour {

    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip coinSfx;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            sfx.PlaySoundEffect(coinSfx);
            Destroy(gameObject);
        }
    }
    
}
