using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

