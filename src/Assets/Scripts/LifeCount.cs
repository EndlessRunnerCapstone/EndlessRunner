using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{     
     Text lifeCount;
     // Use this for initialization
     void Start()
     {
          lifeCount = GetComponent<Text>();
          lifeCount.text = "" + Globals.PlayerOne.NumberOfLives;
     }

     // Update is called once per frame
     void Update()
     {

     }
}

