using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class inputplayer : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Rigidbody2D player;
    [SerializeField]
    private float jumpforce, velocity;
    [SerializeField]
    private LayerMask lmChao;
    [SerializeField]
    private float raioPe;
    [SerializeField]
    private Transform posPe;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private bool estaNoChao;
    [SerializeField]
    private bool viradoParaEsquerda = false;
    [SerializeField]
    private GameObject nick;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    private void FixedUpdate()
    {
        estaNoChao = Physics2D.OverlapCircle(posPe.position, raioPe, lmChao);
        nick = gameObject.GetComponentInChildren<Text>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            nick.GetComponent<Text>().text = photonView.Owner.NickName;
            nick.GetComponent<Text>().color = Color.green;
            bool jump = false;
            float direction = Input.GetAxisRaw("Horizontal");
            if(viradoParaEsquerda && direction > 0)
            {
                
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                viradoParaEsquerda = !viradoParaEsquerda;
                nick.transform.localScale = new Vector3(1, nick.transform.localScale.y, nick.transform.localScale.z);
            }
            if (!viradoParaEsquerda && direction < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                viradoParaEsquerda = !viradoParaEsquerda;
                nick.transform.localScale = new Vector3(-1, nick.transform.localScale.y, nick.transform.localScale.z);
            }
            if (Input.GetButton("Jump") && estaNoChao)
            {
                jump = true;
            }
            if (!jump)
            {
                player.velocity = new Vector2(velocity * direction * Time.deltaTime, player.velocity.y);
            }
            else
            {
                player.velocity = new Vector2(velocity * direction * Time.deltaTime, jumpforce);
            }
            anim.SetFloat("velx", Mathf.Abs(player.velocity.x));
            anim.SetFloat("vely", player.velocity.y);
            anim.SetBool("isground", estaNoChao);
        }
        else
        {
            gameObject.GetComponentInChildren<Text>().text = photonView.Owner.NickName;
            gameObject.GetComponentInChildren<Text>().color = Color.red;

            Debug.Log(transform.localScale.x);
            nick.transform.localScale = new Vector3(transform.localScale.x, nick.transform.localScale.y, nick.transform.localScale.z);
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(posPe.position, raioPe);
    }
}
