﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for bricks that can be destroyed. They disappear when hit by player
/// </summary>
public class BrickDestroy : Photon.MonoBehaviour
{

    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip breakSound;
    public Sprite destroyedSprite;

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            return;
        }

        if (Globals.TwoPlayer)
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
        sfx.PlaySoundEffect(breakSound);
        Destroy(gameObject);
        ScoreKeeping.scoreValue += 50;
    }

}
