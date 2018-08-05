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

    void OnTriggerEnter2D(Collider2D coll)
    {
        //  This function calls the "FinalScore function every 0.1 seconds
        InvokeRepeating("FinalScore", 0.01f, 0.1f);
    }

    IEnumerator ChangeScene()
    {
        Debug.Log("Pause");


        yield return new WaitForSeconds(2f);

        // Call the correct loading screen based on what level we are on
        // Also rest time value for next level
        if (SceneManager.GetActiveScene().name == "Level01")
        {
            SceneManager.LoadScene("LoadingScreen1");
            TimeKeeping.timeValue = 400;
        }
        else if (SceneManager.GetActiveScene().name == "Level02")
        {
            SceneManager.LoadScene("LoadingScreen2");
            TimeKeeping.timeValue = 400;
        }
        else if (SceneManager.GetActiveScene().name == "Level03")
        {
            SceneManager.LoadScene("LoadingScreen3");
            TimeKeeping.timeValue = 400;
        }
        else if (SceneManager.GetActiveScene().name == "Level04")
        {
            SceneManager.LoadScene("LoadingScreen4");
            TimeKeeping.timeValue = 400;
        }
        else
            Debug.Log("Level Load script on wrong scene.");
    }

    void FinalScore()
    {
        int timeRemaining = (int)TimeKeeping.timeValue; // get timer value
        Debug.Log(timeRemaining);
        // for the amount of time remaining, add score value and take from time value
        // these are the bonus coins for time remaining on the clock
        if (timeRemaining > 0)
        {
            Debug.Log(timeRemaining);
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
