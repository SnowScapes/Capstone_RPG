using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator P_animation;
    Rigidbody2D rigid;
    Transform trans;
    public float jumpForce;
    public float MoveSpeed;
    public float MaxSpeed;
    bool isRun;
    bool isJump;
    bool isSprint;
    // Start is called before the first frame update
    void Start()
    {
        P_animation = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            P_animation.SetTrigger("Attacking");
        }
    }

    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJump)
        {
            P_animation.SetTrigger("Jumping");
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            Debug.Log("바닥에 닿음");
        }
    }
}
