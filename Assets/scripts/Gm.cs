using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Gm : MonoBehaviourPunCallbacks
{
    public int playersCount = 0;
    public GameObject jogador;
    // Start is called before the first frame update
    public Gm Instancia { get; private set; }
    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instancia = this;
    }


    void Start()
    {
        // photonView.RPC("addPlayer", RpcTarget.AllBuffered);
        addPlayer();
    }


   // [PunRPC]
    public void addPlayer()
    {
        playersCount++;
        //if(playersCount == PhotonNetwork.PlayerList.Length)
        //{
        
           GameObject go =  PhotonNetwork.Instantiate(jogador.name, transform.position, Quaternion.identity);
           
        //}
    }

 


}
