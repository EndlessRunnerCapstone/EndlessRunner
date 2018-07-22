using Assets.Scripts.MatchMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAbstractionLayerSub : INetworkAbstractionLayer {

    public bool autoJoinLobby { get; set; }

    public bool automaticallySyncScene { get; set; }

    public void ConnectUsingSettings(string version)
    {
        throw new System.NotImplementedException();
    }

    public void JoinRandomRoom()
    {
        throw new System.NotImplementedException();
    }

}
