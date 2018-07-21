using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour {

    public GameObject PlayerPrefab;

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (PhotonNetwork.isMasterClient && PhotonNetwork.room.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel("Level01");
        }
    }

    // Use this for initialization
    void Start ()
    {

        var test = new Vector3 { x = -7.85f, y = -4.719f, z = 0 };


        if (Player_Move.LocalPlayerInstance == null)
        {
            if(!PhotonNetwork.connected && Globals.TwoPlayer)
            {
                return;
            }

            if (PhotonNetwork.connected)
            {
                PhotonNetwork.Instantiate(this.PlayerPrefab.name, test, Quaternion.identity, 0);
            }
            else
            {
                
                Instantiate(PlayerPrefab, test, Quaternion.identity);
            }
        }
        else
        {
            Player_Move.LocalPlayerInstance.transform.position = test;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
