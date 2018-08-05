using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ReloadScene(string sceneName)
    {
        photonView.RPC("ReloadInternal", PhotonTargets.AllBufferedViaServer, sceneName);
    }

    [PunRPC]
    void ReloadInternal(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Globals.TwoPlayer)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("MainMenu");
            }
        }

    }
}
