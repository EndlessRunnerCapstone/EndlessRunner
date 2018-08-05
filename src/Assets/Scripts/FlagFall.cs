using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This function is used for the flag that falls down the flagpole at the end of the scene.
/// It plays a sound effect, intiates the animation, and sets bool canPlay to false so it only does so once.
/// </summary>

public class FlagFall : MonoBehaviour {

	[SerializeField] SoundEffectsManager sfx;
	[SerializeField] AudioClip flagSound;
    private bool canPlay = true;
	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	IEnumerator OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
            if (canPlay)
            {
                sfx.PlayEndMusic();
                sfx.PlaySoundEffect(flagSound);
                anim.Play("Flag_Fall");
                anim.Play("FlagFall_Level3");
                anim.Play("FlagFall_Level1");
                canPlay = false;
            }
			
		}

		yield return null;
	}

}

