using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeping : MonoBehaviour {

    public static int scoreValue = 0;
    Text score;

	// Use this for initialization
	void Start () {
        score = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        
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

    public void Reset()
    {
        scoreValue -= scoreValue;
    }
}
