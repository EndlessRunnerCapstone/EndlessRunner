using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour {

	[SerializeField] SoundEffectsManager sfx;
	[SerializeField] AudioClip fireworksSound;
	public Animator anim;
	public Animator fireworks;
	public Animator fireworks2;
	public Animator fireworks3;
	public Animator fireworks4;
	public Animator fireworks5;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	IEnumerator OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			anim.Play("FlagRaise");
			sfx.PlaySoundEffect(fireworksSound);
			fireworks.Play("Fireworks");
			yield return new WaitForSeconds(0.07f);
			fireworks.gameObject.SetActive(false);
			sfx.PlaySoundEffect(fireworksSound);
			fireworks2.Play("Fireworks");
			yield return new WaitForSeconds(0.07f);
			fireworks2.gameObject.SetActive(false);
			sfx.PlaySoundEffect(fireworksSound);
			fireworks3.Play("Fireworks");
			yield return new WaitForSeconds(0.07f);
			fireworks3.gameObject.SetActive(false);
			sfx.PlaySoundEffect(fireworksSound);
			fireworks4.Play("Fireworks");
			yield return new WaitForSeconds(0.07f);
			fireworks4.gameObject.SetActive(false);
			sfx.PlaySoundEffect(fireworksSound);
			fireworks5.Play("Fireworks");
			yield return new WaitForSeconds(0.07f);
			fireworks5.gameObject.SetActive(false);
		}
		yield return null;
	}
}
