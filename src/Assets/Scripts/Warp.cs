using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

    public Transform warpTarget;
    public GameObject player;
    private bool colliding;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if(colliding)
        {
            if(this.gameObject.tag == "DownPipe")
            {
                if(Input.GetKeyDown("down"))
                {
                    CameraControl cameraControl = player.gameObject.GetComponent<CameraControl>();
                    player.gameObject.transform.position = warpTarget.position;
                }
            }
            else
            {
                CameraControl cameraControl = player.gameObject.GetComponent<CameraControl>();
                player.gameObject.transform.position = warpTarget.position;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            colliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        colliding = false;
    }

}
