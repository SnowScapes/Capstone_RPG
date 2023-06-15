using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject player;
    public GameObject InvenBox;
    public GameObject EquipBox;
    public GameObject CraftBox;
    public GameObject SplashObj;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI MPtext;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ExpText;
    public Slider HPbar;
    public Slider MPbar;
    public Image image;
    public Image Ragebar;
    int HP;
    int MP;

    public bool checkbool = false;

    RectTransform inven_trans;
    RectTransform equip_trans;
    void Awake()
    {
        InvenBox.SetActive(true);
        EquipBox.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        NameText.text = player.GetComponent<Player>().PlayerName;
        inven_trans = InvenBox.GetComponent<RectTransform>();
        equip_trans = EquipBox.GetComponent<RectTransform>();
        Ragebar.fillAmount = 1;
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
        Player_Stat();

        if (checkbool)                                            //만약 checkbool 이 참이면
        {
            SceneManager.LoadScene("GameOver");
        }
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CraftBox.activeSelf)
                CraftBox.SetActive(false);
            else
                CraftBox.SetActive(true);
        }
    }

    void move_inventory()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.tag);
                if (hit.collider.CompareTag("Enemy")) // 태그가 "test"인 게임 오브젝트를 클릭했을 때
                {
                    Debug.Log("터치");
                    //equip_trans.anchoredPosition = Input.mousePosition;
                }
            }
        }
    }

    void Player_Stat()
    {
        HPbar.maxValue = player.GetComponent<Player>().PlayerMaxHP;
        MPbar.maxValue = player.GetComponent<Player>().PlayerMaxMP;
        HPbar.value = player.GetComponent<Player>().PlayerCurHP;
        MPbar.value = player.GetComponent<Player>().PlayerCurMP;
    }

    IEnumerator MainSplash()
    {
        Color color = image.color;                            //color 에 판넬 이미지 참조

        for (int i = 100; i >= 0; i--)                            //for문 100번 반복 0보다 작을 때 까지
        {
            color.a += Time.deltaTime * 0.005f;               //이미지 알파 값을 타임 델타 값 * 0.01
            image.color = color;                                //판넬 이미지 컬러에 바뀐 알파값 참조
            Debug.Log(image.color.a);
            if (image.color.a >= 1.1)                        //만약 판넬 이미지 알파 값이 0보다 작으면
            {
                checkbool = true;                              //checkbool 참 
            }
        }
        yield return null;                                        //코루틴 종료
    }
}
