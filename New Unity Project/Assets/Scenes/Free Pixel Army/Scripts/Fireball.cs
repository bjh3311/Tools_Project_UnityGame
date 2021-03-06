using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        if (this.gameObject.activeSelf)//True상태로 Set 되어 있을때만 InsertQueue 실행
        {
            Boss_Object_Pool.InSertQueue(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)//플레이어와 부딪히면
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("플레이어와 부딪힘");
            this.gameObject.SetActive(false);
            Boss_Object_Pool.InSertQueue(this.gameObject);
        }
    }
}
