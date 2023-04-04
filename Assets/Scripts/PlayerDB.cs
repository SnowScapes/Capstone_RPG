using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MySql.Data.MySqlClient;

public class PlayerDB : MonoBehaviour
{
    //static MySqlConnection conn;

    static string DB_ipAddress = "127.0.0.1";
    static string DB_ID = "Userid";
    static string DB_PW = "Userpw";
    static string DB_Name = "DB_Name";

    string strConn = string.Format("server={0};uid={1},pwd={2};database={3};charset=utf8",
        DB_ipAddress, DB_ID, DB_PW, DB_Name);

    public string[] Datas;

    /*void Awake()
    {
        try
        {
            conn = new MySqlConnection(strConn);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GetData()
    {

    }

    void SaveData()
    {

    }
}
