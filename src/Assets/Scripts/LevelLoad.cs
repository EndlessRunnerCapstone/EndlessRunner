using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (SceneManager.GetActiveScene().name == "Level01")
        {
            SceneManager.LoadScene("LoadingScreen1");
        }
        else if (SceneManager.GetActiveScene().name == "Level02")
        {
            SceneManager.LoadScene("LoadingScreen2");
        }
        else if (SceneManager.GetActiveScene().name == "Level03")
        {
            SceneManager.LoadScene("LoadingScreen3");
        }
        else if (SceneManager.GetActiveScene().name == "Level04")
        {
            SceneManager.LoadScene("LoadingScreen4");
        }
        else
            Debug.Log("Level Load script on wrong scene.");
    }
}
