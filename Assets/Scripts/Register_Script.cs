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
    public TMP_InputField Input_NAME;
    public TMP_InputField Input_PNO;
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
            connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "project", "Select_MEMB", "12#4@");
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                Debug.Log("Connecting to MySQL...");
                conn.Open();
                Debug.Log("Connected to MySQL.\r\n");
                
                MySqlCommand SelectCommand = new MySqlCommand();
                SelectCommand.Connection = conn;
                SelectCommand.CommandText = "select * from memb where memb_id = @memb_id";

                MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
                cmd.Parameters.Add("@memb_id", MySqlDbType.VarChar, 20);
                cmd.Parameters[0].Value = Input_ID.text;

                MySqlDataReader table = cmd.ExecuteReader();
                if (table.Read())
                {
                    Debug.Log("중복된 ID 입니다.");
                }
                else
                {
                    connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "project", "Insert_MEMB", "12#4@");            
                    using (MySqlConnection myConnection =new MySqlConnection(connStr)) {

                        myConnection.Open();

                        MySqlCommand insertCommand = new MySqlCommand();
                        insertCommand.Connection = myConnection;

                        insertCommand.CommandText = "INSERT INTO memb(memb_id, memb_pw, memb_name, memb_p_no) VALUES(@memb_id, @memb_pw, @memb_name, @memb_p_no) ";

                        insertCommand.Parameters.Add("@memb_id", MySqlDbType.VarChar, 20);
                        insertCommand.Parameters["@memb_id"].Value = Input_ID.text;
                        insertCommand.Parameters.Add("@memb_pw", MySqlDbType.VarChar, 255);
                        insertCommand.Parameters["@memb_pw"].Value = Input_PW.text;
                        insertCommand.Parameters.Add("@memb_name", MySqlDbType.VarChar, 10);
                        insertCommand.Parameters["@memb_name"].Value = Input_NAME.text;
                        insertCommand.Parameters.Add("@memb_p_no", MySqlDbType.String, 13);
                        insertCommand.Parameters["@memb_p_no"].Value = Input_PNO.text;

                        insertCommand.ExecuteNonQuery();
                    }

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