using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class exitGame : MonoBehaviourPunCallbacks
{
    public void ExitGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            menu();
        }
        else
        {
            photonView.RPC("disconnect",RpcTarget.All);
        }
    }
    [PunRPC]
    public void disconnect()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("main");
    }

    public void menu()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("main");
    }
}
