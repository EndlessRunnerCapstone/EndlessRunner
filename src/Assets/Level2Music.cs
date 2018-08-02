using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
