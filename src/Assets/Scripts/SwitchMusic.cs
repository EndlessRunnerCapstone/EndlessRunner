using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusic : MonoBehaviour {

    [SerializeField] SoundEffectsManager sfx;

    IEnumerator OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            sfx.PlayOWMusic();
        }

        yield return null;
    }
}
