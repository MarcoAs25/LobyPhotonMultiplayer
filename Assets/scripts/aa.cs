using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
public class aa : MonoBehaviour
{
    bool achou=false;
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (!achou)
        {
            GameObject[] aa = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject game in aa)
            {
                if (game.GetComponent<PhotonView>().IsMine)
                {
                    achou = true; 
                    GetComponent<CinemachineVirtualCamera>().Follow = game.transform;
                    GetComponent<CinemachineVirtualCamera>().LookAt = game.transform;
                }
            }
        }
        
    }
}
