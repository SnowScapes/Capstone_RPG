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
    public GameObject CHCTReg_Popup;

    public TMP_InputField Input_ID;
    public TMP_InputField Input_PW;
    public Button Button_Login;
    public Button Button_Register;

    static string MEMB_CODE;
    static string MEMB_ID;
    static string MEMB_PW;
    static string MEMB_NAME;
    static string MEMB_P_NO;

    public Image image;

    string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "project", "select_memb", "12#4@");

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
                SelectCommand.CommandText = "call login_prod(@memb_id, @memb_pw) ";

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
                    
                    string connStr2 = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "project", "select_chct", "12#4@");
                    MySqlConnection conn2 = new MySqlConnection(connStr2);
                    conn2.Open();

                    MySqlCommand SelectCommand2 = new MySqlCommand();
                    SelectCommand2.Connection = conn2;
                    SelectCommand2.CommandText = "call select_memb_chct_prod(@memb_chct_code) ";

                    MySqlCommand cmd2 = new MySqlCommand(SelectCommand2.CommandText, conn2);
                    cmd2.Parameters.Add("@memb_chct_code", MySqlDbType.VarChar, 8);
                    cmd2.Parameters[0].Value = table[0].ToString();
                    
                    MySqlDataReader table2 = cmd2.ExecuteReader();                   
                    
                    if(table2.Read()) {
                        StartCoroutine("MainSplash");
                        StartCoroutine(LoadMyAsyncScene());
                    }
                    else {
                        CHCTReg_Popup.SetActive(true);
                        Login_Popup.SetActive(false);
                    }   
                    table2.Close();
                    conn2.Close();
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

    }

    IEnumerator LoadMyAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("In_Dungeon");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator MainSplash()
    {
        Color color = image.color;

        for (int i = 100; i >= 0; i--)
        {
            color.a -= Time.deltaTime * 0.005f;

            image.color = color;
        }
        yield return null;
    }
}
