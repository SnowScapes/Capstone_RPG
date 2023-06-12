using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator P_animation;
    Rigidbody2D rigid;
    Transform trans;
    CircleCollider2D ATK_RNG;
    BoxCollider2D Jumpcol;
    CapsuleCollider2D col;
    public float jumpForce;
    public float MoveSpeed;
    public float MaxSpeed;
    
    bool isRun;
    bool isJump;
    bool DJump_able;
    bool isSprint;
    int jumpcount = 2;

    // Start is called before the first frame update
    void Start()
    {
        P_animation = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        ATK_RNG = GetComponent<CircleCollider2D>();
        col = GetComponent<CapsuleCollider2D>();
        Jumpcol = GetComponent<BoxCollider2D>();
        ATK_RNG.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ATK_RNG.enabled = true;
            P_animation.SetTrigger("Attacking");
            ATK_RNG.enabled = false;
        }
    }

    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJump && jumpcount > 0)
        {
            P_animation.SetBool("Jumping", true);
            if (jumpcount == 1)
                P_animation.SetTrigger("DoubleJumping");
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpcount--;
            if (jumpcount == 0)
                isJump = true;
        }
    }

    public void move()
    {
        float axis = Input.GetAxisRaw("Horizontal");
        if (axis != 0)
        {
            rigid.AddForce(Vector2.right * axis * MoveSpeed, ForceMode2D.Impulse);
            trans.localScale = new Vector2(2 * axis, 2);
        }
        if (rigid.velocity.x != 0)
        {
            P_animation.SetBool("isRunning", true);
            isRun = true;
        }
        else
        {
            isRun = false;
            P_animation.SetBool("isRunning", false);
        }
        if (rigid.velocity.x > MaxSpeed && !isSprint)
        {
            rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < MaxSpeed * (-1) && !isSprint)
        {
            rigid.velocity = new Vector2(MaxSpeed * (-1), rigid.velocity.y);
        }
    }

    public void sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isRun && !isJump)
        {
            isSprint = true;
            P_animation.SetTrigger("Sprinting");
            rigid.AddForce(Vector2.right * (MoveSpeed) * Input.GetAxisRaw("Horizontal"), ForceMode2D.Impulse);
            Debug.Log("Sprint!");
            isSprint = false;
        }
    }

    public void Obtain()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
            P_animation.SetBool("Jumping",false);
            isJump = false;
            jumpcount = 2;
            DJump_able = false;
            Debug.Log("바닥에 닿음");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        col.isTrigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        col.isTrigger = false;
    }
}
