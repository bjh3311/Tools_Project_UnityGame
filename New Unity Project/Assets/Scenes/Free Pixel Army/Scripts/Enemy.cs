using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxShotDelay;
    public float curShotDelay;//발사간 속도
    public GameObject bulletObj;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, 0);    
    }
    void Update()
    {
        Fire();
        Reload();
    }
    void OnBecameInvisible()//화면밖으로 나갈때
    {
        Destroy(gameObject);//
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            curShotDelay = -3000f;
            rigid.velocity = new Vector2(0, 14f);
            Destroy(gameObject,0.6f);
        }
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    void Fire()//발사하는 함수
    {
        if (curShotDelay < maxShotDelay)//장전시간이 충족이안되면
        {
            return;
        }
        GameObject bullet = Instantiate(bulletObj, transform.position + Vector3.left * 2.5f + Vector3.up * 1.0f, transform.rotation);
        GameObject bullet2 = Instantiate(bulletObj, transform.position + Vector3.left * 2.5f + Vector3.up * 1.5f, transform.rotation);
        GameObject bullet3 = Instantiate(bulletObj, transform.position + Vector3.left * 2.5f + Vector3.up * 0.5f, transform.rotation);
        //현재 위치보다 왼쪽위에 총알생성 
        Rigidbody2D rigid_bullet = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid_bullet_2 = bullet2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid_bullet_3 = bullet3.GetComponent<Rigidbody2D>();
        rigid_bullet.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
        rigid_bullet_2.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
        rigid_bullet_3.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
        curShotDelay = 0;//꼭 초기화해줘야된다.
    }
}
