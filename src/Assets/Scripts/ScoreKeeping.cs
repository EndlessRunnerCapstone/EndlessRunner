using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to keep score in the UI and track through the game.
/// </summary>
public class ScoreKeeping : MonoBehaviour {

    public static int scoreValue = 0; // global variable
    Text score; //linked to UI text box

	// Use this for initialization
	void Start () {
        score = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateScore();
    }

    public void Reset()
    {
        scoreValue -= scoreValue;
    }

    public void UpdateScore()
    {
        if (scoreValue < 10)
        {
            score.text = "00000" + scoreValue;
        }
        else if (scoreValue < 100 && scoreValue >= 10)
        {
            score.text = "0000" + scoreValue;
        }
        else if (scoreValue <= 1000 && scoreValue >= 100)
        {
            score.text = "000" + scoreValue;
        }
        else if (scoreValue < 10000 && scoreValue >= 1000)
        {
            score.text = "00" + scoreValue;
        }
        else if (scoreValue < 100000 && scoreValue >= 10000)
        {
            score.text = "0" + scoreValue;
        }
        else
        {
            score.text = "" + scoreValue;
        }
    }
}
