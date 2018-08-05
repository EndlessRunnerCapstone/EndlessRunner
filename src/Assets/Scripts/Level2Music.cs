using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to start the level 2 music
/// </summary>
public class Level2Music : MonoBehaviour {

    [SerializeField] SoundEffectsManager MusicManager;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            MusicManager.PlayUGMusic();
        }

    }
}
