using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{
    Rigidbody2D rigid;
    NavMeshAgent agent;
    Animator anim;

    public Transform[] arrWaypoint;

    private Vector2 dest;
    private Coroutine moveStop;

    void Start()
    {
        this.rigid = this.GetComponent<Rigidbody2D>();
        this.anim = this.GetComponent<Animator>();

        this.agent = this.GetComponent<NavMeshAgent>();


    }

    private void AiMove()
    {
        int random = Random.Range(0, arrWaypoint.Length);
        Debug.LogFormat("random : {0}", random);
        // 랜덤으로 목적지 선정

        for (int i = 0; i < arrWaypoint.Length; i++)
        {
            if (i == random)
            {
                this.dest = this.arrWaypoint[i].position;
                // 선정한 목적지를 기억

                if (this.moveStop == null)
                {
                    Debug.Log("코루틴시작");
                    this.moveStop = this.StartCoroutine(this.crAiMove());
                    // 목적지와 AI의 거리를 계산하는 메서드 --> 애니매이션 변경을 위해
                }

                this.agent.SetDestination(this.dest);
                // AI에게 목적이로 이동명령을 내림
                this.anim.Play("Walk");
                break;
            }
        }
    }

    IEnumerator crAiMove()
    {
        while (true)
        {
            var dis = Vector2.Distance(this.transform.position, this.dest);
            //목적지와 AI사이의 거리 계산
            if (dis <= 0.2f)
            {
                Debug.Log("목적지 도착");
                this.anim.Play("Idle");
                //도착하면 애니메이션 바꿈
                if (this.moveStop != null)
                {
                    this.StopCoroutine(this.moveStop);
                    this.moveStop = null;
                    Invoke("AiMove", 1.5f);
                    //1.5초뒤 다른 곳으로 이동시킴
                    break;
                }
            }
            yield return null;
        }
    }
}
