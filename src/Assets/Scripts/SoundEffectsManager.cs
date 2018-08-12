using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the sound for the game
/// </summary>

public class SoundEffectsManager : MonoBehaviour
{
    AudioSource sfxPlayer;
    public AudioSource levelMusic;
    public AudioSource undergroundMusic;
    public AudioSource worldDone;
    public AudioSource starMan;
    [SerializeField] AudioClip stageClear;

    public void Start()
    {
        sfxPlayer = gameObject.GetComponent<AudioSource>();
    }

    // Play one go of a sound effect
    public void PlaySoundEffect(AudioClip sfx)
    {
        sfxPlayer.PlayOneShot(sfx);
    }

    // stop the level music and play the stage clear music
    public void PlayEndMusic()
    {
        levelMusic.Stop();
        undergroundMusic.Stop();
        PlaySoundEffect(stageClear);
    }

    // stop the overworld level music and play the underground music (used in Level 2)
    public void PlayUGMusic()
    {
        levelMusic.Stop();
        undergroundMusic.Play();
    }

    // stop the underground music and play the overworld level music (used in Level 2)
    public void PlayOWMusic()
    {
        undergroundMusic.Stop();
        levelMusic.Play();
    }

    public void PlayWorldDoneMusic()
    {
        levelMusic.Stop();
        worldDone.Play();
    }

    public void PlayStarMan()
    {
        levelMusic.Stop();
        starMan.Play();
    }

    public void StopStarMan()
    {
        starMan.Stop();
        levelMusic.Play();
    }
}
