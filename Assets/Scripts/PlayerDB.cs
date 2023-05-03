using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MySql.Data.MySqlClient;

public class PlayerDB : MonoBehaviour
{
    //static MySqlConnection conn;

    Player p_info;
    public GameObject UserInfo;

    static string DB_ipAddress = "127.0.0.1";
    static string DB_ID;
    static string DB_PW;
    static string DB_Name;
    
    string[] Datas;

    void Awake()
    {
        UserInfo = GameObject.Find("UserInfo");
        p_info = GetComponent<Player>();
        GetData(UserInfo.GetComponent<UserInfo>().MEMB_CODE);
    }

    void GetData(string memb_code)
    {
        DB_Name = "project";
        DB_ID = "select_chct";
        DB_PW = "12#4@";
        
        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);
        
        MySqlConnection conn = new MySqlConnection(connStr);
        try {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "call select_memb_chct_prod(@memb_chct_code) ";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@memb_chct_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = memb_code;

            MySqlDataReader table = cmd.ExecuteReader();
            
            //ArrayList 사용시 속도 저하로 인해 Array 사용
            //데이터 사용 시 형 변환 필요
            if (table.Read()) {
                //CHCT_Data[0] = (table[0].ToString()); //CHCT_CODE
                p_info.PlayerName = table[1].ToString(); //CHCT_NAME
                p_info.PlayerLevel = int.Parse(table[2].ToString()); //CHCT_LV
                p_info.PlayerExp = float.Parse(table[3].ToString()); //CHCT_EXP
                p_info.PlayerMaxHP = int.Parse(table[4].ToString()); //CHCT_HP
                p_info.PlayerMaxMP = int.Parse(table[5].ToString()); //CHCT_MP
                p_info.PlayerATK = int.Parse(table[6].ToString()); //CHCT_ATK
                p_info.PlayerDEF = int.Parse(table[7].ToString()); //CHCT_DEF
                //CHCT_Data[8] = (table[8].ToString()); //MEMB_CHCT_CODE
            }
            
            table.Close();
            conn.Close();
        } catch (Exception e) {
            conn.Close();
            Debug.Log(e.ToString());
        }
    }

    //CHCT_Data 배열과 MEMB_CODE를 읽어와 업데이트하는 방식
    void SaveData(string[] CHCT_Datas, string memb_code) {
        DB_Name = "project";
        DB_ID = "update_chct";
        DB_PW = "12#4@";
        
        String connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);
        MySqlConnection conn = new MySqlConnection(connStr);
            try {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand UpdateCommand = new MySqlCommand();
            UpdateCommand.Connection = conn;
            UpdateCommand.CommandText = "call update_chct_prod(@memb_code, @chct_name, @chct_lv, @chct_exp, @chct_hp, @chct_mp, @chct_atk, @chct_def) ";

            MySqlCommand cmd = new MySqlCommand(UpdateCommand.CommandText, conn);
            cmd.Parameters.Add("@memb_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = memb_code;
            cmd.Parameters.Add("@chct_name", MySqlDbType.VarChar, 10);
            cmd.Parameters[1].Value = CHCT_Datas[1];
            cmd.Parameters.Add("@chct_lv", MySqlDbType.Int16);
            cmd.Parameters[2].Value = CHCT_Datas[2];
            cmd.Parameters.Add("@chct_exp", MySqlDbType.Float);
            cmd.Parameters[3].Value = CHCT_Datas[3];
            cmd.Parameters.Add("@chct_hp", MySqlDbType.Int16);
            cmd.Parameters[4].Value = CHCT_Datas[4];
            cmd.Parameters.Add("@chct_mp", MySqlDbType.Int16);
            cmd.Parameters[5].Value = CHCT_Datas[5];
            cmd.Parameters.Add("@chct_atk", MySqlDbType.Int16);
            cmd.Parameters[6].Value = CHCT_Datas[6];
            cmd.Parameters.Add("@chct_def", MySqlDbType.Int16);
            cmd.Parameters[7].Value = CHCT_Datas[7];


            cmd.ExecuteNonQuery();
            conn.Close();
        } catch (Exception e) {
        Debug.Log(e.ToString());
        }
    }
}
