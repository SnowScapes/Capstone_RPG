using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    PlayerController pctrl;
    Player player_stat;
    GameObject player;
    EnemyInfo Stat;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    int next;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        pctrl = player.GetComponent<PlayerController>();
        Stat = this.GetComponent<EnemyInfo>();
        player_stat = player.GetComponent<Player>();

        Invoke("randomVelocity", Random.Range(2, 4));
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(next*2, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + next * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        // 시작,방향 색깔

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    void randomVelocity()
    {
        next = Random.Range(-1, 2);

        if (next != 0)
        {
            spriteRenderer.flipX = (next == 1); //nextMove가 1이면 방향바꾸기
        }

        Invoke("randomVelocity", Random.Range(2, 4));
    }

    void Turn()
    {
        next = next * (-1);
        spriteRenderer.flipX = (next == 1); //nextMove가 1이면 방향바꾸기


        CancelInvoke();
        Invoke("randomVelocity", Random.Range(2, 4));
    }
}
