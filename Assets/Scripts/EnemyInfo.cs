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
    public int mob_atk;
    public int mob_def;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GET_MONS(string map_code)
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
            UpdateCommand.CommandText = "call map_mons_prod(@map_code) ";

            MySqlCommand cmd = new MySqlCommand(UpdateCommand.CommandText, conn);
            cmd.Parameters.Add("@map_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = map_code; // p_info.현재 위치 값이 parameter로 사용될 예정

            MySqlDataReader table = cmd.ExecuteReader();

            while (table.Read())
            {
                // 단일 행 X -> 행단위 처리 필요

                // 추가 필요사항
                // mons_code와 몬스터에 대한 연결 필요(현재 캐릭터와 db 값 간의 상호연결이 되어 있지 않음)

                // 생성 방식
                // 1. x좌표에 대한 random 형식으로 몬스터 생성 함수 호출
                // 2. 일정 좌표를 기준으로 간격을 둔 상태로 몬스터 생성 함수 호출
            }

            table.Close();
            conn.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}
