using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchMaker : PunBehaviour
{
    private const string GameVersion = "1";

    private void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
    }

    // Use this for initialization
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Connect()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(GameVersion);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnectedFromPhoton()
    {
        // TODO
        base.OnDisconnectedFromPhoton();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        // Create a random room if there is no room for us to join
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        // TODO: Change to room level
        Debug.Log("TODO!!!!");
    }
}
