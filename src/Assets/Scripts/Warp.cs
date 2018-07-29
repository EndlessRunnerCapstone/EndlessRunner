using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

    public Transform warpTarget;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CameraControl cameraControl = player.gameObject.GetComponent<CameraControl>();

        other.gameObject.transform.position = warpTarget.position;
        cameraControl.setYLock();
        //Camera.main.transform.position = warpTarget.position;

        Debug.Log("move");

    }

}
