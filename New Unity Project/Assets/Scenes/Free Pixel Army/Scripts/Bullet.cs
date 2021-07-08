using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Player p;//Player 스크립트
    public GameObject Player;
    private void Start()
    {
    
    }
    // Start is called before the first frame update
    void OnBecameInvisible()//화면밖으로 나갈때
    {
        this.gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision)//적과 부딪히면
    {
        if(collision.gameObject.tag=="Enemy")
        {
            this.gameObject.SetActive(false);
        }
    }
}
