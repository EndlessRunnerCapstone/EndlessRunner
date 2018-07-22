using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MatchMaker
{
    /// <summary>
    /// Allows us to put all pun calls behind this interface for testability
    /// </summary>
    public interface INetworkAbstractionLayer
    {
        bool autoJoinLobby { get; set; }

        bool automaticallySyncScene { get; set; }

        void JoinRandomRoom();

        void ConnectUsingSettings(string version);
    }
}
