﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCamera : MonoBehaviour {

    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        player = GameObject.FindWithTag("Player");
        CameraControl.lockX = false;

    }
}
