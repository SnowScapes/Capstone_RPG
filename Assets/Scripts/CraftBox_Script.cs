using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftBox_Script : MonoBehaviour
{
    public GameObject Equip_List;
    public GameObject Consume_List;
    public GameObject Inventory;
    public GameObject Accept_Btn;
    public GameObject Popup;

    public Image Craft_Slot1;
    public Image Craft_Slot2;

    RectTransform trans;

    string PATH = "Resources/";
    string slot1_itemcode;
    string slot2_itemcode;
    string craft_itemcode;

    bool slot1 = false;
    bool slot2 = false;
    bool type;

    int slot1_index;
    int slot2_index;

    void Start()
    {
        Popup.SetActive(false);
        trans = this.GetComponent<RectTransform>();
        Craft_Slot1.enabled = false;
        Craft_Slot2.enabled = false;
        Equip_List.SetActive(false);
        Consume_List.SetActive(false);
        Accept_Btn.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void EquipCraft_BtnClick()
    {
        Equip_List.SetActive(true);
        Consume_List.SetActive(false);
    }

    public void ConsumeCraft_BtnClick()
    {
        Equip_List.SetActive(false);
        Consume_List.SetActive(true);
    }

    public void HP_Craft()
    {
        reset();
        craft_itemcode = "CNIT0000";
        slot1_itemcode = "CMIT0000";
        slot2_itemcode = "CMIT0001";
        type = false;
        Get_CraftResources(slot1_itemcode, slot2_itemcode);
        Check_Inventory();
        Btn_Avl();
    }

    public void MP_Craft()
    {
        reset();
        craft_itemcode = "CNIT0001";
        slot1_itemcode = "CMIT0000";
        slot2_itemcode = "CMIT0002";
        type = false;
        Get_CraftResources(slot1_itemcode, slot2_itemcode);
        Check_Inventory();
        Btn_Avl();
    }

    public void Weapon_Craft()
    {
        reset();
        craft_itemcode = "EQIT0004";
        slot1_itemcode = "CMIT0003";
        slot2_itemcode = "CMIT0004";
        type = true;
        Get_CraftResources(slot1_itemcode, slot2_itemcode);
        Check_Inventory();
        Btn_Avl();
    }

    public void Head_Craft()
    {
        reset();
        craft_itemcode = "EQIT0000";
        slot1_itemcode = "CMIT0003";
        slot2_itemcode = "CMIT0002";
        type = true;
        Get_CraftResources(slot1_itemcode, slot2_itemcode);
        Check_Inventory();
        Btn_Avl();
    }

    public void Top_Craft()
    {
        reset();
        craft_itemcode = "EQIT0001";
        slot1_itemcode = "CMIT0003";
        slot2_itemcode = "CMIT0008";
        type = true;
        Get_CraftResources(slot1_itemcode, slot2_itemcode);
        Check_Inventory();
        Btn_Avl();
    }

    public void Bottom_Craft()
    {
        reset();
        craft_itemcode = "EQIT0002";
        slot1_itemcode = "CMIT0003";
        slot2_itemcode = "CMIT0007";
        type = true;
        Get_CraftResources(slot1_itemcode, slot2_itemcode);
        Check_Inventory();
        Btn_Avl();
    }

    public void Bag_Craft()
    {
        reset();
        craft_itemcode = "EQIT0003";
        slot1_itemcode = "CMIT0005";
        slot2_itemcode = "CMIT0006";
        type = true;
        Get_CraftResources(slot1_itemcode, slot2_itemcode);
        Check_Inventory();
        Btn_Avl();
    }

    public void Confirm_Btn()
    {
        Popup.SetActive(true);
        Inventory.GetComponent<Inventory_Script>().input_r_item(slot1_index,slot2_index);
        if(type)
        {
            Inventory.GetComponent<Inventory_Script>().input_e_item(craft_itemcode);
        }
        else
        {
            Inventory.GetComponent<Inventory_Script>().input_c_item(craft_itemcode);
        }
        reset();
        Check_Inventory();
        Btn_Avl();
    }

    public void Cancel_Btn()
    {
        this.gameObject.SetActive(false);
    }

    public void Close_Popup()
    {
        Popup.SetActive(false);
    }

    void Btn_Avl()
    {
        if (slot1 && slot2)
            Accept_Btn.SetActive(true);
        else
            Accept_Btn.SetActive(false);
    }

    void reset()
    {
        slot1 = false;
        slot2 = false;
        Accept_Btn.SetActive(false);
    }

    void Check_Inventory()
    {
        for (int i = 0; i < 18; i++) {
            if (Inventory.GetComponent<Inventory_Script>().Get_Itemcode(i) == slot1_itemcode)
            {
                slot1 = true;
                slot1_index = i;
            }
            if (Inventory.GetComponent<Inventory_Script>().Get_Itemcode(i) == slot2_itemcode)
            {
                slot2 = true;
                slot2_index = i;
            }
        }
    }

    void Get_CraftResources(string Res1, string Res2) {
        Craft_Slot1.enabled = true;
        Craft_Slot2.enabled = true;
        Sprite sprite = Resources.Load<Sprite>(PATH+Res1);
        Craft_Slot1.sprite = sprite;
        sprite = Resources.Load<Sprite>(PATH+Res2);
        Craft_Slot2.sprite = sprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        trans.anchoredPosition = new Vector2(eventData.position.x, eventData.position.y - 170);
    }
}
