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
        DB_ID = "Select_CHCT";
        DB_PW = "12#4@";
        
        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);
        
        MySqlConnection conn = new MySqlConnection(connStr);
        try {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "select * from chct where memb_chct_code = @memb_code";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@memb_code", MySqlDbType.VarChar, 8);
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
        DB_ID = "Update_CHCT";
        DB_PW = "12#4@";
        
        String connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);
        MySqlConnection conn = new MySqlConnection(connStr);
            try {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand UpdateCommand = new MySqlCommand();
            UpdateCommand.Connection = conn;
            UpdateCommand.CommandText = "update chct set chct_name=@chct_name, chct_lv=@chct_lv, chct_exp=@chct_exp, chct_hp=@chct_hp, chct_mp=@chct_mp, chct_atk=@chct_atk, chct_def=@chct_def where memb_chct_code=@memb_code ";

            MySqlCommand cmd = new MySqlCommand(UpdateCommand.CommandText, conn);
            cmd.Parameters.Add("@chct_name", MySqlDbType.VarChar, 10);
            cmd.Parameters[0].Value = CHCT_Datas[1];
            cmd.Parameters.Add("@chct_lv", MySqlDbType.Int16);
            cmd.Parameters[1].Value = CHCT_Datas[2];
            cmd.Parameters.Add("@chct_exp", MySqlDbType.Float);
            cmd.Parameters[2].Value = CHCT_Datas[3];
            cmd.Parameters.Add("@chct_hp", MySqlDbType.Int16);
            cmd.Parameters[3].Value = CHCT_Datas[4];
            cmd.Parameters.Add("@chct_mp", MySqlDbType.Int16);
            cmd.Parameters[4].Value = CHCT_Datas[5];
            cmd.Parameters.Add("@chct_atk", MySqlDbType.Int16);
            cmd.Parameters[5].Value = CHCT_Datas[6];
            cmd.Parameters.Add("@chct_def", MySqlDbType.Int16);
            cmd.Parameters[6].Value = CHCT_Datas[7];
            cmd.Parameters.Add("@memb_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[7].Value = memb_code;

            cmd.ExecuteNonQuery();
            conn.Close();
        } catch (Exception e) {
        Debug.Log(e.ToString());
        }
    }

    /*
    신규 계정이 들어왔을 떄 insert 형식
    추후 chct_code trigger 생성 후 parmeter에서 제외 할 예정
    
    public static void NEW_Player(string chct_code, String chct_name, string memb_code) {
        DB_Name = "project";
        DB_ID = "Insert_CHCT";
        DB_PW = "12#4@";
        
        String connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);
        MySqlConnection conn = new MySqlConnection(connStr);
            try {
            conn.Open();
            Console.WriteLine("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "select * from chct where memb_chct_code = @memb_code";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@memb_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = memb_code;

            MySqlDataReader table = cmd.ExecuteReader();
            
            if (table.Read()) {
                Console.WriteLine("캐릭터 존재");
                table.Close();
            }

            else {
                table.Close();
                MySqlCommand InsertCommand = new MySqlCommand();
                InsertCommand.Connection = conn;
                InsertCommand.CommandText = "insert into CHCT(CHCT_CODE, CHCT_NAME, MEMB_CHCT_CODE) values (@chct_code, @chct_name, @memb_code) ";

                MySqlCommand cmd1 = new MySqlCommand(InsertCommand.CommandText, conn);
                cmd1.Parameters.Add("@chct_code", MySqlDbType.VarChar, 8);
                cmd1.Parameters[0].Value = chct_code;
                cmd1.Parameters.Add("@chct_name", MySqlDbType.VarChar, 10);
                cmd1.Parameters[1].Value = chct_name;
                cmd1.Parameters.Add("@memb_code", MySqlDbType.VarChar, 8);
                cmd1.Parameters[2].Value = memb_code;

                cmd1.ExecuteNonQuery();
            }
            conn.Close();
        } catch (Exception e) {
        Console.WriteLine(e.ToString());
        }
    }
    */
}
