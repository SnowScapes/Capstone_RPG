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
    public GameObject Login_Popup;

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
            Login_Popup.SetActive(true);
            Chctname_Popup.SetActive(false);
        }
            
    }

    public static bool NEW_Player(string chct_name, string memb_code)
    {
        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", "127.0.0.1", "project", "select_chct", "12#4@");
        
        MySqlConnection conn = new MySqlConnection(connStr);
        
        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "call select_memb_chct_prod(@memb_chct_code)";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@memb_chct_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = memb_code;

            MySqlDataReader table = cmd.ExecuteReader();

            if (table.Read())
            {
                //duplicate(unique에 대해 중복) error로 발생으로 인해 catch문으로 이동 -> throw 처리 해야 할 듯?
                Debug.Log("캐릭터 존재");
                table.Close();
                conn.Close();
                return false;
            }
            else
            {
                table.Close();
                
                string connStr2 = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", "127.0.0.1", "project", "insert_chct", "12#4@");
                
                using (MySqlConnection conn2 =new MySqlConnection(connStr2)) {
                    conn2.Open();
                    
                    MySqlCommand InsertCommand = new MySqlCommand();
                    InsertCommand.Connection = conn2;
                    InsertCommand.CommandText = "call insert_chct_prod(null, @chct_name, @memb_code) "; //관리자 CHCT는 form을 통한 insert가 아닌 db에서 procedure 호출을 통한 insert 바람

                    MySqlCommand cmd1 = new MySqlCommand(InsertCommand.CommandText, conn2);
                    cmd1.Parameters.Add("@chct_name", MySqlDbType.VarChar, 10);
                    cmd1.Parameters[0].Value = chct_name;
                    cmd1.Parameters.Add("@memb_code", MySqlDbType.VarChar, 8);
                    cmd1.Parameters[1].Value = memb_code;

                    cmd1.ExecuteNonQuery();
                    conn2.Close();
                    return true;
                }
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
