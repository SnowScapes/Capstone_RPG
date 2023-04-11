using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    Transform p_Trans;
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        p_Trans = player.transform;
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        trans.position = new Vector3(p_Trans.position.x, p_Trans.position.y+3, 0);
    }
}
