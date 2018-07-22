using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MatchMaker
{
    public class PunNetwork : INetworkAbstractionLayer
    {
        public bool autoJoinLobby
        {
            get
            {
                return PhotonNetwork.autoJoinLobby;
            }

            set
            {
                PhotonNetwork.autoJoinLobby = value;
            }
        }

        public bool automaticallySyncScene { get { return PhotonNetwork.automaticallySyncScene; } set { PhotonNetwork.automaticallySyncScene = value; } }

        public void ConnectUsingSettings(string version)
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }

        public void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
}
