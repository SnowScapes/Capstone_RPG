using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using MySql.Data.MySqlClient;

public class LOGIN_Script : MonoBehaviour
{
    static MySqlConnection conn;
    static string sql;

    public GameObject LoginInfo;
    public GameObject Login_Popup;
    public GameObject Register_Popup;

    public TMP_InputField Input_ID;
    public TMP_InputField Input_PW;
    public Button Button_Login;
    public Button Button_Register;

    static string MEMB_CODE;
    static string MEMB_ID;
    static string MEMB_PW;
    static string MEMB_NAME;
    static string MEMB_P_NO;

    string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "project", "Select_MEMB", "12#4@");

    void Awake()
    {
        try
        {
            conn = new MySqlConnection(connStr);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    void Start()
    {
        Login_Popup.SetActive(true);
    }

    public void LoginButtonClick()
    {
        if(Input_ID.text == "" || Input_PW.text == "")
        {
            Debug.Log("ID 또는 PW 칸은 빈칸이 될 수 없습니다");
        }
        else
        {
            try
            {
                Debug.Log("Connecting to MySQL...");
                conn.Open();
                Debug.Log("Connected to MySQL.\r\n");

                MySqlCommand SelectCommand = new MySqlCommand();
                SelectCommand.Connection = conn;
                SelectCommand.CommandText = "select * from memb where memb_id = @memb_id and memb_pw = sha2(@memb_pw, 256) ";

                MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
                cmd.Parameters.Add("@memb_id", MySqlDbType.VarChar, 20);
                cmd.Parameters[0].Value = Input_ID.text;
                cmd.Parameters.Add("@memb_pw", MySqlDbType.VarChar, 255);
                cmd.Parameters[1].Value = Input_PW.text;
                MySqlDataReader table = cmd.ExecuteReader();

                if(table.Read())
                {
                    Debug.Log("로그인 성공");
                    LoginInfo.GetComponent<UserInfo>().MEMB_CODE = table[0].ToString();
                    LoginInfo.GetComponent<UserInfo>().MEMB_NAME = table[1].ToString();
                    StartCoroutine(LoadMyAsyncScene());
                }
                else
                {
                    Debug.Log("로그인 실패");
                }
                table.Close();
                conn.Close();
                Debug.Log("Disconnected to MySQL.");
            }
            catch (Exception e)
            {
                conn.Close();
                Debug.Log(e.ToString());
            }
        }
    }

    public void RegisterButtonClick()
    {
        Register_Popup.SetActive(true);
        Login_Popup.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        conn.Close();
    }

    IEnumerator LoadMyAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("New Scene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
