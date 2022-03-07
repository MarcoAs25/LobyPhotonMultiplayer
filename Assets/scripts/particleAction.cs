using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class particleAction : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private ParticleSystem ps;
    [SerializeField]
    private bool isOn;
    [SerializeField]
    private bool isUp;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Play()
    {
        ps.Play();
        isOn = true;
    }
    public void Stop()
    {
        ps.Stop();
        isOn = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isOn && isUp)
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().gravityScale > 0)
            {
                Debug.Log("oaka");
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = -3f;
                collision.gameObject.transform.localScale = new Vector3(collision.gameObject.transform.localScale.x, collision.gameObject.transform.localScale.y * -1, collision.gameObject.transform.localScale.z);
            }

        }else if (collision.gameObject.CompareTag("Player") && isOn && !isUp)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().gravityScale < 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3f;
                collision.gameObject.transform.localScale = new Vector3(collision.gameObject.transform.localScale.x, collision.gameObject.transform.localScale.y * -1, collision.gameObject.transform.localScale.z);
            }
        }
    }


}
