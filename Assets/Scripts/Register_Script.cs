using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MySql.Data.MySqlClient;

public class Register_Script : MonoBehaviour
{
    public GameObject Login_Popup;
    public GameObject Register_Popup;

    public TMP_InputField Input_ID;
    public TMP_InputField Input_PW;
    public Button Button_Register;

    int MEMB_CODE;

    void Start()
    {
        Register_Popup.SetActive(false);
    }

    public void RegisterButtonClick()
    {
        if (Input_ID.text == "" || Input_PW.text == "")
        {
            Debug.Log("ID 또는 PW 칸은 빈칸이 될 수 없습니다.");
        }
        else
        {
            string connStr = "";
            connStr = string.Format("Server={0};Port=3306;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "game", "root", "root");
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                Debug.Log("Connecting to MySQL...");
                conn.Open();
                Debug.Log("Connected to MySQL.\r\n");
                string sql = string.Format("SELECT * from memb where MEMB_ID = \"{0}\"", Input_ID.text);

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader table = cmd.ExecuteReader();
                if (table.Read())
                {
                    Debug.Log("중복된 ID 입니다.");
                }
                else
                {
                    table.Close();
                    sql = "select MAX(memb_code) from memb";
                    cmd = new MySqlCommand(sql, conn);
                    table = cmd.ExecuteReader();
                    if (table.Read())
                        MEMB_CODE = table.IsDBNull(0) ? 0 : table.GetInt32(0)+1;
                    table.Close();
                    
                    sql = string.Format("insert into memb(memb_code,memb_id,memb_pw) values('{0}','{1}','{2}')", MEMB_CODE, Input_ID.text, Input_PW.text);
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    table.Close();
                    conn.Close();
                    Debug.Log("회원가입 성공");
                    Login_Popup.SetActive(true);
                    Register_Popup.SetActive(false);
                }
                table.Close();
                conn.Close();
            }
            catch(Exception e)
            {
                conn.Close();
                Debug.Log(e.ToString());
            }
        }
    }
}
