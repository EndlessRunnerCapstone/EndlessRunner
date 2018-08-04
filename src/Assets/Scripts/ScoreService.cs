using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService
{
    public string Scores { get; set; }

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
}
