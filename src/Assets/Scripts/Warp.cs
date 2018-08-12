using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : Photon.MonoBehaviour
{
    [SerializeField] SoundEffectsManager sfx;
    [SerializeField] AudioClip pipeSound;
    public Transform warpTarget;
    public GameObject player;
    private bool colliding = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (colliding)
        {
            if (this.gameObject.tag == "DownPipe")
            {
                if ((Input.GetKeyDown(KeyCode.S) || (Input.GetKeyDown(KeyCode.DownArrow))))
                {
                    sfx.PlaySoundEffect(pipeSound);
                    CameraControl cameraControl = player.gameObject.GetComponent<CameraControl>();

                    var players = GameObject.FindGameObjectsWithTag("Player");

                    Debug.Log(players.Length);

                    if (players.Length == 2)
                    {
                        Vector3 offsetPos = new Vector3 { x = warpTarget.position.x, y = warpTarget.position.y + .50f, z = warpTarget.position.z };
                        players[1].gameObject.transform.position = offsetPos;
                    }

                    players[0].gameObject.transform.position = warpTarget.position;
                }
            }
            else
            {
                sfx.PlaySoundEffect(pipeSound);
                CameraControl cameraControl = player.gameObject.GetComponent<CameraControl>();

                var players = GameObject.FindGameObjectsWithTag("Player");

                Debug.Log(players.Length);

                if (players.Length == 2)
                {
                    Vector3 offsetPos = new Vector3 { x = warpTarget.position.x, y = warpTarget.position.y + .50f, z = warpTarget.position.z };

                    players[1].gameObject.transform.position = offsetPos;
                }

                players[0].gameObject.transform.position = warpTarget.position;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        player = GameObject.FindWithTag("Player");

        if (coll.gameObject.tag == "Player")
        {
            colliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        colliding = false;
    }

}
