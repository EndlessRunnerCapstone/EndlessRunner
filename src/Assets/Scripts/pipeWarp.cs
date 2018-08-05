using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class just controls the pipe sound effect
/// </summary>
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
