using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  This class controls what loading screens are loaded based on what level we are on.
///  It also gives the bonus coins for time remaining on the clock
/// </summary>

public class LevelLoad : MonoBehaviour {

    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip coinSfx;
    private bool canPlay = true;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(WorldTracker.worldNumber == "1-4")
        {
            if(canPlay)
            {
                sfx.PlayWorldDoneMusic();
                canPlay = false;
            }
      
        }
        //  This function calls the "FinalScore function every 0.1 seconds
        InvokeRepeating("FinalScore", 0.01f, 0.1f);
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2f);
    

        // Call the loading screen
        // Also rest time value for next level  

        if (WorldTracker.worldNumber == "1-4")
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            if (WorldTracker.worldNumber == "1-1")
            {
                WorldTracker.worldNumber = "1-2";
            }
            else if (WorldTracker.worldNumber == "1-2")
            {
                WorldTracker.worldNumber = "1-3";
            }
            else if (WorldTracker.worldNumber == "1-3")
            {
                WorldTracker.worldNumber = "1-4";
            }
            else if (WorldTracker.worldNumber == "1-4")
            {
                SceneManager.LoadScene("MainMenu");
            }

            SceneManager.LoadScene("LoadingScreen1");
            CameraControl.lockX = false;
            TimeKeeping.timeValue = 400;
        }      
    }

    void FinalScore()
    {
        int timeRemaining = (int)TimeKeeping.timeValue; // get timer value

        // for the amount of time remaining, add score value and take from time value
        // these are the bonus coins for time remaining on the clock
        if (timeRemaining > 0)
        {
            ScoreKeeping.scoreValue += 10;
            sfx.PlaySoundEffect(coinSfx);
            TimeKeeping.timeValue -= 10;
        }
        else
        {
            CancelInvoke(); // once 0 is hit, stop the Invoke function
            StartCoroutine(ChangeScene());
        }
    }
}
