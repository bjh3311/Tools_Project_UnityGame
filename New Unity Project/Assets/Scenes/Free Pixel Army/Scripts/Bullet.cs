using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
    
    }
    // Start is called before the first frame update
    void OnBecameInvisible()//화면에서 안보이면
    {
        if(this.gameObject.activeSelf)//True상태로 Set 되어 있을때만 InsertQueue 실행
        {
            Player_Object_Pool.InSertQueue(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)//적과 부딪히면
    {
        if(collision.gameObject.tag=="Enemy")
        {
            Player_Object_Pool.InSertQueue(this.gameObject);
        }
    }
}
