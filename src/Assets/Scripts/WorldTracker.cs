using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTracker : MonoBehaviour {

    public static string worldNumber = "";
    Text world;

	// Use this for initialization
	void Start () {
        world = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        updateWorld();
	}

    public void updateWorld()
    {
        world.text = worldNumber;
    }
}
