using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI MPtext;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ExpText;
    int HP;
    int MP;

    // Start is called before the first frame update
    void Start()
    {
        NameText.text = player.GetComponent<Player>().PlayerName;
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
    }
}
