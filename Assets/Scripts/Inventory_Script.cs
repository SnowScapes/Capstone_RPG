using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MySql.Data.MySqlClient;

public class Inventory_Script : MonoBehaviour, IDragHandler
{
    public GameObject UserInfo;
    public Image[] Equip_Slot;
    public Image[] Consume_Slot;
    public Image[] Resource_Slot;
    static string[] itemcode = new string[18];
    static int[] item_mnt = new int[18];
    GameObject Inven;

    RectTransform trans;

    void Awake()
    {
        UserInfo = GameObject.Find("UserInfo");
        Inven = GameObject.Find("Inventory_Box");
        for (int i=0; i<18; i++)
        {
            itemcode[i] = "0000";
            Equip_Slot[i].enabled = false;
            Consume_Slot[i].enabled = false;
            Resource_Slot[i].enabled = false;
        }
    }

    void Start()
    {
        trans = this.GetComponent<RectTransform>();
        //SHOW_OWND_item(UserInfo.GetComponent<UserInfo>().CHCT_CODE);
        LOAD_ITEM_IMGS();
        Inven.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UPD_OWND_itme(string chct_code, string[] item_code, int[] item_mnt)
    {

        string DB_ipAddress = "127.0.0.1";
        string DB_Name = "project";
        string DB_ID = "update_chct";
        string DB_PW = "12#4@";

        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);

        MySqlConnection conn = new MySqlConnection(connStr);

        try
        {
            for (int i = 0; i < 18; i++)
            {
                if (itemcode[i] != "0000")
                {
                    conn.Open();
                    Debug.Log("Connected to MySQL.");

                    MySqlCommand SelectCommand = new MySqlCommand();
                    SelectCommand.Connection = conn;
                    SelectCommand.CommandText = "call update_chct_item_prod(@chct_code, @item_code, @chct_item_num)";

                    MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
                    cmd.Parameters.Add("@chct_code", MySqlDbType.VarChar, 8);
                    cmd.Parameters[0].Value = chct_code;
                    cmd.Parameters.Add("@item_code", MySqlDbType.VarChar, 8);
                    cmd.Parameters[0].Value = item_code[i];
                    cmd.Parameters.Add("@chct_item_num", MySqlDbType.Int64, 3);
                    cmd.Parameters[0].Value = item_mnt[i];

                    MySqlDataReader table = cmd.ExecuteReader();

                    table.Close();
                    conn.Close();
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    void LOAD_ITEM_IMGS()
    {
        for (int i = 0; i < 18; i++)
        {
            Debug.Log(itemcode[i]);
            if (itemcode[i] != "0000")
            {
                //Slot[i].enabled = true;
                string PATH = string.Format("items/{0}", itemcode[i]);
                Sprite sprite = Resources.Load<Sprite>(PATH);
                //Slot[i].sprite = sprite;
            }
        }
    }

    // 아이템 창에서 아이템 보유 상태를 보여주기 위한 코드
    /*public static void SHOW_OWND_item(string chct_code)
    {
        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", "127.0.0.1", "project", "select_chct", "12#4@");

        MySqlConnection conn = new MySqlConnection(connStr);

        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "call show_ownd_item_prod(@chct_code)";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@chct_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = chct_code;

            MySqlDataReader table = cmd.ExecuteReader();

            int index = 0;
            while (table.Read())
            {
                itemcode[index] = table[1].ToString();
                index++;
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
    }*/

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

    void get_eqit(string chct_code) //장비아이템
    {

        string DB_ipAddress = "127.0.0.1";
        string DB_Name = "project";
        string DB_ID = "select_item";
        string DB_PW = "12#4@";

        //int i = 0;

        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);

        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "call eqit_item_prod(@chct_code) ";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@chct_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = chct_code;

            MySqlDataReader table = cmd.ExecuteReader();

            while (table.Read())
            {
                // table[0] : chct_code, table[1] : item_code, table[2] : item_name, table[3] : item_text, table[4] : chct_item_num

                //eq_itemcode[i] = table[0].ToString();
                //eq_item_mnt[i] = int.Parse(table[4].ToString()); 
            }

            table.Close();
            conn.Close();
        }
        catch (Exception e)
        {
            conn.Close();
            Debug.Log(e.ToString());
        }
    }

    void get_cnit(string chct_code) //소비아이템
    {
        string DB_ipAddress = "127.0.0.1";
        string DB_Name = "project";
        string DB_ID = "select_item";
        string DB_PW = "12#4@";

        //int i = 0;

        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);

        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "call cnit_item_prod(@chct_code) ";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@chct_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = chct_code;

            MySqlDataReader table = cmd.ExecuteReader();

            while (table.Read())
            {
                // table[0] : chct_code, table[1] : item_code, table[2] : item_name, table[3] : item_text, table[4] : chct_item_num

                //cn_itemcode[i] = table[0].ToString();
                //cn_item_mnt[i] = int.Parse(table[4].ToString()); 
            }

            table.Close();
            conn.Close();
        }
        catch (Exception e)
        {
            conn.Close();
            Debug.Log(e.ToString());
        }
    }

    void get_cmit(string chct_code) //재료아이템
    {

        string DB_ipAddress = "127.0.0.1";
        string DB_Name = "project";
        string DB_ID = "select_item";
        string DB_PW = "12#4@";

        //int i = 0;

        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);

        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "call cmit_item_prod(@chct_code) ";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@chct_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = chct_code;

            MySqlDataReader table = cmd.ExecuteReader();

            while (table.Read())
            {
                // table[0] : chct_code, table[1] : item_code, table[2] : item_name, table[3] : item_text, table[4] : chct_item_num

                //cm_itemcode[i] = table[0].ToString();
                //cm_item_mnt[i] = int.Parse(table[4].ToString()); 
            }

            table.Close();
            conn.Close();
        }
        catch (Exception e)
        {
            conn.Close();
            Debug.Log(e.ToString());
        }
    }

    public void OnDrag(PointerEventData eventData) 
    { 
        trans.anchoredPosition = eventData.position; 
    }

    public void CloseButtonClick() {
        Inven.SetActive(false);
    }
 }
