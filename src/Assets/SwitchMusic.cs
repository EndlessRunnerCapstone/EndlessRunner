using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class is used to switch the music from the UW theme to the OW theme in Level 2 at the end.
/// </summary>

public class SwitchMusic : MonoBehaviour
{

    [SerializeField]
    SoundEffectsManager sfx;

    IEnumerator OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            sfx.PlayOWMusic();
        }

        yield return null;
    }
}