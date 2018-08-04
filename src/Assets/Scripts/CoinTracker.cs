using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTracker : MonoBehaviour
{

    public static int coinValue = 0;
    Text coins;

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
