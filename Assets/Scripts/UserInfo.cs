using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string MEMB_CODE;
    public string MEMB_NAME;
    public string CHCT_CODE;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
