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

        var masterPosition = new Vector3 { x = -7.75f, y = -4.719f, z = 0 };

        var secondPosition = new Vector3 { x = -8.15f, y = -4.719f, z = 0 };

        if (Player_Move.LocalPlayerInstance == null)
        {
            if(!PhotonNetwork.connected && Globals.TwoPlayer)
            {
                return;
            }

            if (PhotonNetwork.connected)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    PhotonNetwork.Instantiate(this.PlayerPrefab.name, masterPosition, Quaternion.identity, 0);
                }
                else
                {
                    PhotonNetwork.Instantiate(this.PlayerPrefab.name, secondPosition, Quaternion.identity, 0);
                }
            }
            else
            {
                
                Instantiate(PlayerPrefab, masterPosition, Quaternion.identity);
            }
        }
        else
        {
            if (!Globals.TwoPlayer || PhotonNetwork.isMasterClient)
            {
                Player_Move.LocalPlayerInstance.transform.position = masterPosition;
            }
            else
            {
                Player_Move.LocalPlayerInstance.transform.position = secondPosition;
            }

            Player_Move player = Player_Move.LocalPlayerInstance.GetComponent<Player_Move>();

            if (player != null)
            {
                player.ResetMario();
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
