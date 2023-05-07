using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject player;
    public GameObject InvenBox;
    public GameObject EquipBox;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI MPtext;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ExpText;
    int HP;
    int MP;

    RectTransform inven_trans;
    RectTransform equip_trans;

    // Start is called before the first frame update
    void Start()
    {
        NameText.text = player.GetComponent<Player>().PlayerName;
        inven_trans = InvenBox.GetComponent<RectTransform>();
        equip_trans = EquipBox.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        HP = player.GetComponent<Player>().PlayerCurHP;
        MP = player.GetComponent<Player>().PlayerCurMP;
        HPtext.text = HP.ToString();
        MPtext.text = MP.ToString();
        LevelText.text = string.Format("Lv : {0}",player.GetComponent<Player>().PlayerLevel.ToString());
        ExpText.text = string.Format("EXP : {0}",player.GetComponent<Player>().PlayerExp.ToString());
        show_inventory();
        move_inventory();
    }

    void show_inventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InvenBox.activeSelf)
                InvenBox.SetActive(false);
            else
                InvenBox.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (EquipBox.activeSelf)
                EquipBox.SetActive(false);
            else
                EquipBox.SetActive(true);
        }
    }

    void move_inventory()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);
                if (hit.collider.CompareTag("Enemy")) // 태그가 "test"인 게임 오브젝트를 클릭했을 때
                {
                    Debug.Log("터치");
                    //equip_trans.anchoredPosition = Input.mousePosition;
                }
            }
            
        }
    }
}
