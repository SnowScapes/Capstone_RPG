using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController Controller;
    //PlayerDB P_Info;
    public int PlayerHP;
    public int PlayerMP;
    public int PlayerATK;
    public int PlayerDEF;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<PlayerController>();
        //P_Info = GetComponent<PlayerDB>();
        //new Thread(() => P_Info.GetData()).Start();
    }

    // Update is called once per frame
    void Update()
    {
        Controller.move();
        Controller.attack();
        Controller.sprint();
        Controller.jump();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("데미지 입음");
        }
    }

    void die()
    {
        if (PlayerHP <= 0)
        {
            Controller.P_animation.SetTrigger("die");
        }
    }
}
