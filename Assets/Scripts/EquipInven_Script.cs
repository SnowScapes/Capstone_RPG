using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MySql.Data.MySqlClient;
using TMPro;

public class EquipInven_Script : MonoBehaviour, IDragHandler
{
    RectTransform trans;
    GameObject Equip;
    public GameObject player;
    public GameObject UserInfo;
    Player p_stat;
    //1.head 2.body 3.foot 4.bag 5.weapon
    public Image[] Slot;
    public TextMeshProUGUI[] Stats;
    static string[] E_itemcode = new string[5];

    void Awake() {
        UserInfo = GameObject.Find("UserInfo");
        Equip = GameObject.Find("Equipment_Box");
        trans = this.GetComponent<RectTransform>();
        for (int i=0; i<5; i++) {
            E_itemcode[i] = "0000";
            Slot[i].enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        p_stat = player.GetComponent<Player>();
        get_equip_info(UserInfo.GetComponent<UserInfo>().CHCT_CODE);
        Debug.Log(p_stat.PlayerATK);
        Equip.SetActive(false);
        LoadEquipImgs();
    }

    // Update is called once per frame
    void Update()
    {
        GetStat();
    }

    void get_equip_info(string chct_code)
    {
        string DB_ipAddress = "127.0.0.1";
        string DB_Name = "project";
        string DB_ID = "select_chct";
        string DB_PW = "12#4@";

        string connStr = string.Format("Server={0};Port=3308;Database={1};Uid={2};Pwd={3};charset=utf8 ", DB_ipAddress, DB_Name, DB_ID, DB_PW);

        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL.");

            MySqlCommand SelectCommand = new MySqlCommand();
            SelectCommand.Connection = conn;
            SelectCommand.CommandText = "call is_equip_prod(@chct_code) ";

            MySqlCommand cmd = new MySqlCommand(SelectCommand.CommandText, conn);
            cmd.Parameters.Add("@chct_code", MySqlDbType.VarChar, 8);
            cmd.Parameters[0].Value = chct_code;

            MySqlDataReader table = cmd.ExecuteReader();

            while (table.Read())
            {
                // table[0] : item_type, table[1] : chct_code, table[2] : item_code, table[3] : item_lv
                // table[4] : item_hp, table[5] : item_mp, table[6] : item_atk, table[7] item_def
                switch (table[0])
                {
                    case "head":
                        E_itemcode[0] = table[2].ToString();
                        p_stat.head[0] = int.Parse(table[3].ToString());
                        p_stat.head[1] = int.Parse(table[4].ToString());
                        p_stat.head[2] = int.Parse(table[5].ToString());
                        p_stat.head[3] = int.Parse(table[6].ToString());
                        p_stat.head[4] = int.Parse(table[7].ToString());
                        break;
                    case "weapon":
                        E_itemcode[4] = table[2].ToString();
                        p_stat.weapon[0] = int.Parse(table[3].ToString());
                        p_stat.weapon[1] = int.Parse(table[4].ToString());
                        p_stat.weapon[2] = int.Parse(table[5].ToString());
                        p_stat.weapon[3] = int.Parse(table[6].ToString());
                        p_stat.weapon[4] = int.Parse(table[7].ToString());
                        break;
                    case "top":
                        E_itemcode[1] = table[2].ToString();
                        p_stat.top[0] = int.Parse(table[3].ToString());
                        p_stat.top[1] = int.Parse(table[4].ToString());
                        p_stat.top[2] = int.Parse(table[5].ToString());
                        p_stat.top[3] = int.Parse(table[6].ToString());
                        p_stat.top[4] = int.Parse(table[7].ToString());
                        break;
                    case "bag":
                        E_itemcode[3] = table[2].ToString();
                        p_stat.bottom[0] = int.Parse(table[3].ToString());
                        p_stat.bottom[1] = int.Parse(table[4].ToString());
                        p_stat.bottom[2] = int.Parse(table[5].ToString());
                        p_stat.bottom[3] = int.Parse(table[6].ToString());
                        p_stat.bottom[4] = int.Parse(table[7].ToString());
                        break;
                    case "shoes":
                        E_itemcode[2] = table[2].ToString();
                        p_stat.shoes[0] = int.Parse(table[3].ToString());
                        p_stat.shoes[1] = int.Parse(table[4].ToString());
                        p_stat.shoes[2] = int.Parse(table[5].ToString());
                        p_stat.shoes[3] = int.Parse(table[6].ToString());
                        p_stat.shoes[4] = int.Parse(table[7].ToString());
                        break;
                    default:
                        break;
                }
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

    void LoadEquipImgs() {
        for (int i=0; i<5; i++) {
            if (E_itemcode[i] != "0000") {
                Slot[i].enabled = true;
                string PATH = string.Format("Equipments/{0}", E_itemcode[i]);
                Sprite sprite = Resources.Load<Sprite>(PATH);
                Slot[i].sprite = sprite;
            }
        }
    }

    void GetStat() {
        Stats[0].text = p_stat.PlayerMaxHP.ToString();
        Stats[1].text = p_stat.PlayerMaxMP.ToString();
        Stats[2].text = p_stat.PlayerATK.ToString();
        Stats[3].text = p_stat.PlayerDEF.ToString();
        Stats[4].text = p_stat.PlayerExp.ToString();
    }

    public void OnDrag(PointerEventData eventData) 
    { 
        trans.anchoredPosition = eventData.position; 
    }

    public void CloseButtonClick() {
        Equip.SetActive(false);
    }
}
