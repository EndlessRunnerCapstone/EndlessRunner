using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is used to control the very ending animations of the level
/// </summary>
public class Fireworks : MonoBehaviour {

	[SerializeField] SoundEffectsManager sfx;
	[SerializeField] AudioClip fireworksSound;
	public Animator anim;
	public Animator fireworks;
	public Animator fireworks2;
	public Animator fireworks3;
	public Animator fireworks4;
	public Animator fireworks5;
    private bool canPlay = true; // ensure only works once

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	IEnumerator OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
            if(canPlay)
            {
                // raise the flag from the castle
                anim.Play("FlagRaise");
                anim.Play("FlagRaise_Level2");
                canPlay = false;

                // animate the individual fireworks and play their sound effects
                sfx.PlaySoundEffect(fireworksSound);
                fireworks.Play("Fireworks");
                yield return new WaitForSeconds(0.9f); // wait a small bit to show animation
                fireworks.gameObject.SetActive(false); // destroy object once done

                sfx.PlaySoundEffect(fireworksSound);
                fireworks2.Play("Fireworks");
                yield return new WaitForSeconds(0.9f);
                fireworks2.gameObject.SetActive(false);

                sfx.PlaySoundEffect(fireworksSound);
                fireworks3.Play("Fireworks");
                yield return new WaitForSeconds(0.9f);
                fireworks3.gameObject.SetActive(false);

                sfx.PlaySoundEffect(fireworksSound);
                fireworks4.Play("Fireworks");
                yield return new WaitForSeconds(0.9f);
                fireworks4.gameObject.SetActive(false);

                sfx.PlaySoundEffect(fireworksSound);
                fireworks5.Play("Fireworks");
                yield return new WaitForSeconds(0.9f);
                fireworks5.gameObject.SetActive(false);

           //     CameraControl.lockX = false;
            }

		}
		yield return null;
	}
}
