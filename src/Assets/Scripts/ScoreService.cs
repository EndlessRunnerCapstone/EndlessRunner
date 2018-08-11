using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService
{
    public string Scores { get; set; }

    public bool isUploaded { get; set; }

    public IEnumerator FetchScores()
    {
        using (WWW www = new WWW("https://cs-467-scores.azurewebsites.net/api/scores"))
        {
            while(!www.isDone)
            {
                yield return null;
            }

            Scores = www.text;
        }
    }

    public IEnumerator SubmitScore(ScoreEntry score)
    {
        var json = JsonUtility.ToJson(score);
        isUploaded = false;

        using (WWW www = new WWW("https://cs-467-scores.azurewebsites.net/api/scores", System.Text.Encoding.UTF8.GetBytes(json), new Dictionary<string, string> { { "Content-Type", "application/json" } }))
        {
            while (!www.isDone)
            {
                yield return null;
            }
        }

        isUploaded = true;
    }
}
