using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Destroy(gameObject);
        ScoreKeeping.scoreValue += 50;

    }

}
