using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  This class controls what scene is loaded based on what loading screen we are on
/// </summary>

public class LoadingScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadTime());
    }
	
    IEnumerator LoadTime()
    {
        yield return new WaitForSecondsRealtime(3); // wait a few seconds to show screen before loading

        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            SceneManager.LoadScene("MainMenu");
            TimeKeeping.timeValue = 400;
            ScoreKeeping.scoreValue = 0;
            CoinTracker.coinValue = 0;
            yield break;
        }
        else if (WorldTracker.worldNumber == "1-1")
        {
                SceneManager.LoadScene("Level01");
            
        }
        else if (WorldTracker.worldNumber == "1-2")
        {
                SceneManager.LoadScene("Level02");
        }
        else if (WorldTracker.worldNumber == "1-3")
        {
                SceneManager.LoadScene("Level03");
        }
        else if (WorldTracker.worldNumber == "1-4")
        {
                SceneManager.LoadScene("Level04"); 
        }
        else
               Debug.Log("Loading Screen script on wrong scene.");   
    }
}
