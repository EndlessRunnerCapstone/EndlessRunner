using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
 
        StartCoroutine(LoadTime());

    }
	
    IEnumerator LoadTime()
    {
        yield return new WaitForSecondsRealtime(3);

        if (SceneManager.GetActiveScene().name == "LoadingScreen1")
        {
            SceneManager.LoadScene("Level02");
        }
        else if (SceneManager.GetActiveScene().name == "LoadingScreen2")
        {
            SceneManager.LoadScene("Level03");
        }
        else if (SceneManager.GetActiveScene().name == "LoadingScreen3")
        {
            SceneManager.LoadScene("Level04");
        }
        else if (SceneManager.GetActiveScene().name == "LoadingScreen4")
        {
            SceneManager.LoadScene("Level01");
        }
        else
            Debug.Log("Loading Screen script on wrong scene.");
    }
}
