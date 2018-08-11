using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for bricks that can be destroyed. They disappear when hit by player
/// </summary>
public class BrickDestroy : Photon.MonoBehaviour
{

    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip breakSound;
    public GameObject particles;
    SpriteRenderer spriteRenderer;
    ParticleSystem particleSystem;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        particleSystem = particles.GetComponent<ParticleSystem>();

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            return;
        }

          coll.gameObject.GetComponent<Player_Move>().jumpTimeCounter = 0;

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
        spriteRenderer.enabled = false;
        particleSystem.Play();
      //  Destroy(gameObject);
        ScoreKeeping.scoreValue += 50;

    }

}
