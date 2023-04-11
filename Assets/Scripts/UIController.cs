using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public new Camera camera;
    Transform cam_trans;
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        cam_trans = camera.transform;
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        trans.position = new Vector3(cam_trans.position.x, cam_trans.position.y+3, cam_trans.position.z-1);
    }
}
