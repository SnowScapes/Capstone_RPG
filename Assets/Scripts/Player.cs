using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController Controller;
    public string PlayerName;
    public int PlayerLevel;
    public float PlayerExp;
    public int PlayerMaxHP;
    public int PlayerMaxMP;
    public int PlayerCurHP;
    public int PlayerCurMP;
    public int PlayerATK;
    public int PlayerDEF;

    public int[] head = new int[5];
    public int[] weapon = new int[5];
    public int[] top = new int[5];
    public int[] bottom = new int[5];
    public int[] shoes = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<PlayerController>();
        Get_Equip_Stat();
        PlayerCurHP = PlayerMaxHP;
        PlayerCurMP = PlayerMaxMP;
    }

    // Update is called once per frame
    void Update()
    {
        Controller.move();
        Controller.attack();
        Controller.sprint();
        Controller.jump();
        //Controller.DoubleJump();
        die();
        Get_Equip_Stat();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("데미지 입음");
        }
    }

    void Get_Equip_Stat()
    {
        PlayerMaxHP = head[1] + weapon[1] + top[1] + bottom[1] + shoes[1];
        PlayerMaxMP = head[2] + weapon[2] + top[2] + bottom[2] + shoes[2];
        PlayerATK = head[3] + weapon[3] + top[3] + bottom[3] + shoes[3];
        PlayerDEF = head[4] + weapon[4] + top[4] + bottom[4] + shoes[4];
    }

    void die()
    {
        if (PlayerCurHP <= 0)
        {
            Controller.P_animation.SetTrigger("die");
            //this.gameObject.SetActive(false);
        }
    }
}
