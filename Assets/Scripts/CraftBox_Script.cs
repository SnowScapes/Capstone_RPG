using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftBox_Script : MonoBehaviour
{
    public GameObject Equip_List;
    public GameObject Consume_List;

    public Image Craft_Slot1;
    public Image Craft_Slot2;

    public Button Accept_Btn;

    void Start()
    {
        Equip_List.SetActive(false);
        Consume_List.SetActive(false);
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

    }

    public void MP_Craft()
    {

    }

    public void Weapon_Craft()
    {

    }

    public void Head_Craft()
    {

    }

    public void Top_Craft()
    {

    }

    public void Bottom_Craft()
    {

    }

    public void Bag_Craft()
    {

    }

    public void Confirm_Btn()
    {

    }

    public void Cancel_Btn()
    {

    }

    void Check_Inventory()
    {

    }
 }
