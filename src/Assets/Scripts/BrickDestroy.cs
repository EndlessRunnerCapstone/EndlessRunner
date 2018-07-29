using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDestroy : Photon.MonoBehaviour
{

    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip breakSound;
    public Sprite destroyedSprite;
   // Animator anim;


    // Use this for initialization
    void Start()
    {
        //anim = GetComponent<Animator>();
        //anim.enabled = false;
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
    }

}
