using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeWarp : MonoBehaviour
{

    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip pipeSound;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            sfx.PlaySoundEffect(pipeSound);
        }

    }
}
