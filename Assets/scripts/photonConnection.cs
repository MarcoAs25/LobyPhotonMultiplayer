using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class photonConnection : MonoBehaviourPunCallbacks
{
    GameObject menuLogin = null;
    GameObject menuLoby = null;
    GameObject txtPlayerList = null;
    GameObject buttomStart = null;
    // Start is called before the first frame update
    

    void Awake()
    {
         menuLogin = GameObject.FindGameObjectWithTag("menuLogin");
         menuLoby = GameObject.FindGameObjectWithTag("menuLoby");
         txtPlayerList = GameObject.FindGameObjectWithTag("txtPlayerList");
         buttomStart = GameObject.FindGameObjectWithTag("ButtomStart");
         menuLogin.SetActive(true);
         menuLoby.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Join()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        GameObject txtNick = GameObject.FindGameObjectWithTag("txtNick");
        GameObject txtRoom = GameObject.FindGameObjectWithTag("txtRoom");
        if (!txtNick.GetComponent<Text>().text.Equals(""))
        {
            PhotonNetwork.NickName = txtNick.GetComponent<Text>().text;
            if (txtRoom.GetComponent<Text>().text.Equals(""))
            {
                PhotonNetwork.JoinRandomOrCreateRoom();
            }
            else
            {
                PhotonNetwork.JoinOrCreateRoom(txtRoom.GetComponent<Text>().text.ToString(), new RoomOptions(), new TypedLobby("default", LobbyType.Default));

            }
        }
        else
        {
            PhotonNetwork.Disconnect();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Conectou");
        photonView.RPC("atualizaLista", RpcTarget.All);
        menuLogin.SetActive(false);
        menuLoby.SetActive(true);
    }

    public void leave()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Saiu da Room");
    }
    
    public void startGame()
    {
        //PhotonNetwork.CurrentRoom.IsOpen = false;
        photonView.RPC("loadGame", RpcTarget.AllBuffered, "game");
        
    }
    [PunRPC]
    void loadGame(string n)
    {
        PhotonNetwork.LoadLevel(n);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        photonView.RPC("atualizaLista", RpcTarget.All);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        menuLogin.SetActive(true);
        menuLoby.SetActive(false);
    }


    [PunRPC]
    public void atualizaLista()
    {
        txtPlayerList.GetComponent<Text>().text = "";
        foreach (var players in PhotonNetwork.PlayerList)
        {
            txtPlayerList.GetComponent<Text>().text += players.NickName + "\n";
        }
        buttomStart.GetComponent<Button>().interactable = PhotonNetwork.IsMasterClient;

    }


}
