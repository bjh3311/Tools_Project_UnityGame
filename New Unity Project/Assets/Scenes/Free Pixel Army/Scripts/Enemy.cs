using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, 0);    
    }
    void OnBecameInvisible()//화면밖으로 나갈때
    {
        Destroy(gameObject);//
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            rigid.velocity = new Vector2(0, 14f);
            Destroy(gameObject,0.6f);
        }
    }
}
