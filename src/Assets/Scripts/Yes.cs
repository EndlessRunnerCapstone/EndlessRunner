using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Yes : MonoBehaviour
{
    public InputField Input;

    ScoreService ScoreService = new ScoreService();

    public void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(Handle);
    }

    public void Handle()
    {
        if(string.IsNullOrEmpty(Input.text))
        {
            StartCoroutine(FlashRed());
        }
        else
        {
            StartCoroutine(ScoreService.SubmitScore(new ScoreEntry { user = Input.text, Score = ScoreKeeping.scoreValue }));
        }

        StartCoroutine(WaitAndTransition());
    }

    IEnumerator WaitAndTransition()
    {
        while (!ScoreService.isUploaded)
        {
            yield return null;
        }

        TimeKeeping.timeValue = 400;
        ScoreKeeping.scoreValue = 0;
        CoinTracker.coinValue = 0;
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FlashRed()
    {
        Color original = Input.placeholder.color;
        for (int i = 0; i < 3; i++)
        {
            Input.placeholder.color = new Color(255, 0, 0, 1);
            yield return new WaitForSeconds(.1f);
            Input.placeholder.color = original;
            yield return new WaitForSeconds(.1f);
        }
    }
}
