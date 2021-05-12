using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Weapon w;//스크립트
    public GameObject Weapons_0;//무기 오브젝트
    SpriteRenderer rend;
    Rigidbody2D rigid;
    public float maxShotDelay;//최대속도
    public float curShotDelay;//발사간 속도

    public GameObject bulletObj; 

    // Start is called before the first frame update
    void Start()
    {
        w = Weapons_0.GetComponent<Weapon>();//스크립트 불러옴
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }
    // Update is called once per fdbslxl rame , 1분에 약60번 업데이트
    void Update()
    {
        Move();//이동하는 함수
        Fire();//총알을 쏘는 함수
        Reload();//장전한하는 함수
 
    }
    void Move()
    {
        rigid.velocity = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))//왼쪽 방향키 누르면
        {
            w.yFlip();
            rend.flipX = true;
            Weapons_0.transform.localPosition = new Vector3(-0.66f, 0.38f, 0);
            rigid.velocity = new Vector2(-10f, 0);

        }
        if (Input.GetKey(KeyCode.RightArrow))//오른쪽 방향키 누르면
        {
            w.nFlip();
            Weapons_0.transform.localPosition = new Vector3(0.66f, 0.38f, 0);
            rend.flipX = false;
            rigid.velocity = new Vector2(10f, 0);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigid.AddForce(new Vector2(0, 50f));
        }
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    void Fire()//발사하는 함수
    {
        if(!Input.GetButton("Fire1"))//Fire버튼을 안누르면 종료
        {
            return;
        }
        if(curShotDelay<maxShotDelay)//장전시간이 충족이안되면
        {
            return;
        }
        GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
        Rigidbody2D rigid_bullet = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.right*10,ForceMode2D.Impulse);

        curShotDelay = 0;//꼭 초기화해줘야된다.
    }
}
