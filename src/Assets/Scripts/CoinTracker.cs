using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This class is used to track the coins through the game and add them to the UI.
/// </summary>
public class CoinTracker : MonoBehaviour
{
    public static int coinValue = 0; // global variable
    Text coins; // links to the UI text box

    // Use this for initialization
    void Start()
    {
        coins = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (coinValue < 1)
        {
            coins.text = "00" + coinValue;
        }
        else if (coinValue < 10)
        {
            coins.text = "0" + coinValue;
        }
        else
        {
            coins.text = "" + coinValue;
        }
    }

    public void Reset()
    {
        coinValue -= coinValue;
    }
}
