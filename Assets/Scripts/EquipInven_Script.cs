using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipInven_Script : MonoBehaviour, IDragHandler
{
    RectTransform trans;
    GameObject Equip;
    public GameObject player;
    //1.head 2.body 3.foot 4.bag 5.weapon
    public Image[] Slot;
    static string[] E_itemcode = new string[5];

    void Awake() {
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
        LoadEquipImgs();
    }

    // Update is called once per frame
    void Update()
    {
        GetEquipStat();
    }

    void LoadEquipImgs() {
        for (int i=0; i<5; i++) {
            if (E_itemcode[i] != "0000") {
                Slot[i].enabled = true;
            }
        }
    }

    void GetEquipStat() {
        for (int i=0; i<5; i++) {
            if (Slot[i].enabled)
                Debug.Log("test");
        }
    }

    public void OnDrag(PointerEventData eventData) 
    { 
        trans.anchoredPosition = eventData.position; 
    }

    public void CloseButtonClick() {
        Equip.SetActive(false);
    }
}
