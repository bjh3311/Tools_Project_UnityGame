﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void OnBecameInvisible()//화면밖으로 나갈때
    {
        Destroy(this.gameObject);//총알 파괴
    }
}