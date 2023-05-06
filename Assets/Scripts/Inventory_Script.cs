using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Inventory_Script : MonoBehaviour
{
    public GameObject UserInfo;
    public Image[] Slot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 아이템 창에서 아이템 보유 상태를 보여주기 위한 코드
    public static void SHOW_OWND_item(string chct_code)
    {
        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", "127.0.0.1", "project", "select_chct", "12#4@");

        MySqlConnection conn = new MySqlConnection(connStr);

        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "call show_ownd_item(@chct_code)";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@chct_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = chct_code;

            MySqlDataReader table = cmd.ExecuteReader();

            while (table.Read())
            {
                // 단일 행 select가 아님 -> row 단위로 처리가 필요함
                // 아이템 창을 채우는 함수를 만들어서 chct_code, item_code, chct_item_num 값 파라미터로 받아서 동작하는 걸로 하면 될 듯?
            }

            table.Close();
            conn.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    // 이미지를 커서 위에 올렸을 떄 아이템 이름과 설명, 보유 수를 보여주기 위한 코드
    public static void GET_CHCT_ITEM_INFO(string chct_code, string item_code)
    {
        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", "127.0.0.1", "project", "select_chct", "12#4@");

        MySqlConnection conn = new MySqlConnection(connStr);

        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "call chct_item_prod(@chct_code, @item_code)";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@chct_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = chct_code;
            cmd.Parameters.Add("@item_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[1].Value = item_code;

            MySqlDataReader table = cmd.ExecuteReader();

            if (table.Read())
            {
                // table[0] : chct_code, table[1] : item_code, table[2] : item_name, table[3] : item_text, table[4] : chct_item_num
                // 페이지 생성 후 페이지에서 화면에 던지는 방향
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