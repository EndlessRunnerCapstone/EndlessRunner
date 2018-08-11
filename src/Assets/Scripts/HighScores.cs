using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ScoreEntry
{
    public string user;

    public int Score;
}

// The unity json parser is dumb
// https://forum.unity.com/threads/how-to-load-an-array-with-jsonutility.375735/
public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

public class HighScores : MonoBehaviour
{
    public Text text;

    IEnumerable<ScoreEntry> Scores;

    ScoreService scoreService = new ScoreService();

	// Use this for initialization
	void Start () {
        StartCoroutine(scoreService.FetchScores());
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(scoreService.Scores);
		
        if(Scores == null && scoreService.Scores != null)
        {
            Scores = JsonHelper.getJsonArray<ScoreEntry>(scoreService.Scores);
            StringBuilder builder = new StringBuilder();

            foreach(var score in Scores)
            {
                builder.AppendLine(score.user + ": " + score.Score);
            }

            text.text = builder.ToString();
        }

	}
}
