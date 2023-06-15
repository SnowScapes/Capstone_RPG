using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MySql.Data.MySqlClient;

public class EnemyInfo : MonoBehaviour
{
    public string mob_name;
    public int mob_lv;
    public int mob_hp;
    public int mob_curhp;
    public int mob_atk;
    public int mob_def;

    void Start()
    {
        GET_MONS(GameObject.Find("MapCode").GetComponent<MapCode>().Map_code, this.GetComponent<EnemyCode>().Code);
        mob_curhp = mob_hp;
    }

    void Update()
    {
        die();
    }

    void GET_MONS(string map_code, string mons_code)
    {
        string DB_ipAddress = "127.0.0.1";
        string DB_Name = "project";
        string DB_ID = "select_mons";
        string DB_PW = "12#4@";

        String connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand UpdateCommand = new MySqlCommand();
            UpdateCommand.Connection = conn;
            UpdateCommand.CommandText = "call map_mons_prod(@map_code, @mons_code) ";

            MySqlCommand cmd = new MySqlCommand(UpdateCommand.CommandText, conn);
            cmd.Parameters.Add("@map_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = map_code; // p_info.현재 위치 값이 parameter로 사용될 예정
            cmd.Parameters.Add("@mons_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[1].Value = mons_code;

            MySqlDataReader table = cmd.ExecuteReader();

            if (table.Read())
            {
                mob_name = table[2].ToString();
                mob_lv = int.Parse(table[3].ToString());
                mob_hp = int.Parse(table[4].ToString());
                mob_atk = int.Parse(table[5].ToString());
                mob_def = int.Parse(table[6].ToString());
            }

            table.Close();
            conn.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        Debug.Log("Enemy Info Loaded");
    }

    void die()
    {
        if (mob_curhp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
