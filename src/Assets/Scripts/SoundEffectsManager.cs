using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundEffectsManager : MonoBehaviour
{

    AudioSource sfxPlayer;
    public AudioSource levelMusic;
    public AudioSource undergroundMusic;
    [SerializeField] AudioClip stageClear;

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
        undergroundMusic.Stop();
        PlaySoundEffect(stageClear);
    }

    public void PlayUGMusic()
    {
        levelMusic.Stop();
        undergroundMusic.Play();
    }

    public void PlayOWMusic()
    {
        undergroundMusic.Stop();
        levelMusic.Play();
    }
}
