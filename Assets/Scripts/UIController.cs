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
    int HP;
    int MP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HP = player.GetComponent<Player>().PlayerCurHP;
        MP = player.GetComponent<Player>().PlayerCurMP;
        HPtext.text = HP.ToString();
        MPtext.text = MP.ToString();
    }
}
