using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class GameOver : Photon.MonoBehaviour
    {
       private void Awake()
        {
            if (Globals.TwoPlayer)
            {
                PhotonNetwork.automaticallySyncScene = false;
                PhotonNetwork.Disconnect();
            }
        }

        private void Start()
        {
     
        }
    }
}
