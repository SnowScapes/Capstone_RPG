using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    BoxCollider2D Rng;
    
    void Awake()
    {
        Rng = this.GetComponent<BoxCollider2D>();
    }
}
