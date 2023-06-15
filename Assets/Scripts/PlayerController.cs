using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator P_animation;
    public Transform Atk_pos;
    public Vector2 BoxSize;
    Rigidbody2D rigid;
    Player player;
    Transform trans;
    BoxCollider2D Jumpcol;
    CapsuleCollider2D col;
    AudioSource footstep;
    public float jumpForce;
    public float MoveSpeed;
    public float MaxSpeed;
    
    bool isRun;
    bool isJump;
    bool DJump_able;
    bool isSprint;
    bool ragemode = false;
    public bool attacked = false;
    int jumpcount = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<Player>();
        P_animation = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        col = GetComponent<CapsuleCollider2D>();
        Jumpcol = GetComponent<BoxCollider2D>();
        footstep = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void rage()
    {
        if (Input.GetKeyDown(KeyCode.X) && GameObject.Find("UI").GetComponent<UIController>().Ragebar.fillAmount == 1)
        {
            ragemode = true;
            GameObject.Find("UI").GetComponent<UIController>().Ragebar.fillAmount = 0;
        }
    }

    public void attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Atk_pos.position, BoxSize, 0);

            if (ragemode)
            {
                P_animation.SetTrigger("Combo");
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<EnemyInfo>().mob_curhp -= player.PlayerATK;
                        Debug.Log(collider.GetComponent<EnemyInfo>().mob_curhp);
                    }
                }
            }
            else
            {
                P_animation.SetTrigger("Attacking");
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<EnemyInfo>().mob_curhp -= (player.PlayerATK - collider.GetComponent<EnemyInfo>().mob_def / 10);
                        Debug.Log(collider.GetComponent<EnemyInfo>().mob_curhp);
                    }
                }
            }
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
        if (rigid.velocity.x != 0 && jumpcount == 2)
        {
            if (!footstep.isPlaying)
                footstep.Play();
        }
        else
            footstep.Stop();
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
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Atk_pos.position, BoxSize);
    }

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        col.isTrigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        col.isTrigger = false;
    }*/
}
