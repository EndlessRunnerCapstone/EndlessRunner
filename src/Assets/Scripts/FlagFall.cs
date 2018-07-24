using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagFall : MonoBehaviour {

	[SerializeField] SoundEffectsManager sfx;
	[SerializeField] AudioClip flagSound;

	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	IEnumerator OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			sfx.PlayEndMusic();
			sfx.PlaySoundEffect(flagSound);
			anim.Play("FlagFall");
		}

		yield return null;
	}

}

