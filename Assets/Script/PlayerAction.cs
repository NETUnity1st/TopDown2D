using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameManager gameManager;
    public float Speed;
    float h;
    float v;
    Rigidbody2D rigid;
    bool isHorizonMove;
    Animator anim;
    Vector3 DirVec;
    GameObject ScanObject;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Horizontal"))
            isHorizonMove = true;
        else if (Input.GetButtonDown("Vertical"))
            isHorizonMove = false;
        else if (Input.GetButtonUp("Vertical") || Input.GetButtonUp("Horizontal"))
            isHorizonMove = (h != 0);

        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("IsChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("IsChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("IsChange", false);

        if (v == 1)
            DirVec = Vector3.up;
        else if (v == -1)
            DirVec = Vector3.down;
        else if (h == -1)
            DirVec = Vector3.left;
        else if (h == 1)
            DirVec = Vector3.right;

        if(Input.GetKeyDown(KeyCode.Space) && ScanObject != null){
            rigid.velocity = Vector2.zero;
            gameManager.Action(ScanObject);
        }

    }
    void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        if(!gameManager.IsAction)
            rigid.velocity = moveVec * Speed;
        //Ray
        Debug.DrawRay(rigid.transform.position, DirVec, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast
        (rigid.position, DirVec, 1.0f, LayerMask.GetMask("Object"));
        if (rayHit.collider != null)
        {
            ScanObject = rayHit.collider.gameObject;
        }
        else{
            ScanObject = null;
        }
    }
}
