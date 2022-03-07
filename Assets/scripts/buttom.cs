using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class buttom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject player,ObjToInteract;
    private bool Active = false;

    void Update()
    {
        if(player != null)
        {
            if (Input.GetKeyDown(KeyCode.F)&& !Active)
            {
                ObjToInteract.GetComponent<particleAction>().Play();
                Active = true;
                Component[] componentes =  GetComponentsInChildren<SpriteRenderer>();
                componentes[1].gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (Input.GetKeyDown(KeyCode.F) && Active)
            {

                Component[] componentes = GetComponentsInChildren<SpriteRenderer>();
                componentes[1].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                ObjToInteract.GetComponent<particleAction>().Stop();
                Active = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PhotonView>().IsMine)
        {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PhotonView>().IsMine)
        {
            player = null;
        }
        
    }
}
