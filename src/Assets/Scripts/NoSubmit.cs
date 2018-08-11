using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoSubmit : MonoBehaviour
{

    public void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(Handle);
    }

    public void Handle()
    {
        TimeKeeping.timeValue = 400;
        ScoreKeeping.scoreValue = 0;
        CoinTracker.coinValue = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
