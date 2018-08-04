using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeping : MonoBehaviour {

    public static float timeValue = 400;
    Text time;

	// Use this for initialization
	void Start () {
        time = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timeValue -= Time.deltaTime;

        if(timeValue < 100 && timeValue >= 10)
        {
            time.text = "0" + (int)timeValue;
        }
        else if(timeValue < 10 && timeValue > 0)
        {
            time.text = "00" + (int)timeValue;
        }
        else if(timeValue <= 0)
        {
            time.text = "000";
        }
        else
        {
            time.text = "" + (int)timeValue;
        }

    }

    public void Reset()
    {
        timeValue += timeValue;
    }
}
