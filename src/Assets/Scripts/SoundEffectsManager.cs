using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundEffectsManager : MonoBehaviour
{

    AudioSource sfxPlayer;
    public AudioSource levelMusic;
    [SerializeField]
    AudioClip stageClear;

    public void Start()
    {
        sfxPlayer = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(AudioClip sfx)
    {
        sfxPlayer.PlayOneShot(sfx);
    }

    public void PlayEndMusic()
    {
        levelMusic.Stop();
        PlaySoundEffect(stageClear);
    }

}
