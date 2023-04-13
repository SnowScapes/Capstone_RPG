using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using MySql.Data.MySqlClient;

public class CHCTReg_Script : MonoBehaviour
{
    public GameObject Chctname_Popup;

    public TMP_InputField Input_Nick;
    public Button Button_Check;

    public GameObject UserInfo;

    void Awake()
    {
        UserInfo = GameObject.Find("UserInfo");
    }

    // Start is called before the first frame update
    void Start()
    {
        Chctname_Popup.SetActive(false);
    }

    public void CheckButtonClick()
    {
        if(NEW_Player(Input_Nick.text, UserInfo.GetComponent<UserInfo>().MEMB_CODE))
        {
            Debug.Log("캐릭터 생성 성공!");
            StartCoroutine(LoadMyAsyncScene());
        }
            
    }

    public static bool NEW_Player(string chct_name, string memb_code)
    {
        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", "127.0.0.1", "project", "Insert_CHCT", "12#4@");
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "select * from chct where memb_chct_code = @memb_code";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@memb_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = memb_code;

            MySqlDataReader table = cmd.ExecuteReader();

            if (table.Read())
            {
                Debug.Log("캐릭터 존재");
                table.Close();
                conn.Close();
                return false;
            }
            else
            {
                table.Close();
                MySqlCommand InsertCommand = new MySqlCommand();
                InsertCommand.Connection = conn;
                InsertCommand.CommandText = "insert into CHCT(CHCT_NAME, MEMB_CHCT_CODE) values (@chct_name, @memb_code) ";

                MySqlCommand cmd1 = new MySqlCommand(InsertCommand.CommandText, conn);
                cmd1.Parameters.Add("@chct_name", MySqlDbType.VarChar, 10);
                cmd1.Parameters[0].Value = chct_name;
                cmd1.Parameters.Add("@memb_code", MySqlDbType.VarChar, 8);
                cmd1.Parameters[1].Value = memb_code;

                cmd1.ExecuteNonQuery();
                conn.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }
    }

    IEnumerator LoadMyAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("In_Dungeon");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
