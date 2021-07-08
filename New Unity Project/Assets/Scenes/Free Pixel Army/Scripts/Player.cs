﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Weapon w;//스크립트
    public GameObject Weapons_0;//무기 오브젝트
    SpriteRenderer rend;
    Rigidbody2D rigid;


    public float maxShotDelay = 0.2f;//최대속도
    public float curShotDelay;//발사간 속도
    public  GameObject prefab_bullet;//총알 오브젝트 풀링T
    public static Queue<GameObject> bulletPool = new Queue<GameObject>();//총알 Pool
    private readonly int bulletMaxCount = 50;//내가 생성할 총알 갯수
    private int curBulletIndex = 0;//현재 장전된 총알의 인덱스

    public GameObject item;

    public bool isGround = false;//점프 제한을 위한 변수
    public Transform groundCheck;
    public LayerMask groundLayers;
    public float jumpPower;

    Animator animator;

    public GameObject nowHpbar;
    public int nowHp = 100;
    Image _nowHpbar;
    // Start is called before the first frame update
    void Start()
    {
        w = Weapons_0.GetComponent<Weapon>();//스크립트 불러옴
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        _nowHpbar = nowHpbar.transform.GetComponent<Image>();
        animator = GetComponent<Animator>();
        for(int i=0;i<bulletMaxCount;i++)//총알을 bulletMaxCount만큼 생성
        {
            GameObject b = Instantiate<GameObject>(prefab_bullet);
            b.SetActive(false);//총알 발사하기 전까지는 비활성화
            bulletPool.Enqueue(b);
        }
        
    }
    // Update is called once per fdbslxl rame , 1분에 약60번 업데이트
    void Update()
    {
        Move();//이동하는 함수
        Fire();//총알을 쏘는 함수
        Reload();//장전한하는 함수
        _nowHpbar.fillAmount = (float)nowHp /(float)100;
        
    }
    void Move()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position,0.5f, groundLayers);
        if (Input.GetKey(KeyCode.LeftArrow))//왼쪽 방향키 누르면
        {
            w.yFlip();
            rend.flipX = true;
            Weapons_0.transform.localPosition = new Vector3(-0.66f, 0.38f, 0);
            rigid.velocity = new Vector2(-10f, 0);
            animator.SetBool("moving", true);//움직인다면 애니메이션 효과 킨다
        }
        else if (Input.GetKey(KeyCode.RightArrow))//오른쪽 방향키 누르면
        {
            w.nFlip();
            Weapons_0.transform.localPosition = new Vector3(0.66f, 0.38f, 0);
            rend.flipX = false;
            rigid.velocity = new Vector2(10f, 0);
            animator.SetBool("moving", true);//움직인다면 애니메이션 효과 킨다
        }
        else
        {
            animator.SetBool("moving", false);//안움직인다면 애니메이션 효과를 끈다
        }
        if(Input.GetKey(KeyCode.UpArrow)&&isGround)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)//적 총알에 맞으면
    {
        if (col.CompareTag("Enemy_Bullet"))
        {
            nowHp = nowHp - 5;
            if (nowHp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    public void InSertQueue(GameObject b_object)//사용한 총알을 오브젝트 풀에 다시 넣는다
    {

        Debug.Log("InsertQueue실행!!");
        bulletPool.Enqueue(b_object);
        b_object.SetActive(false);
    }
    public GameObject GetQueue()//오브젝트 풀에서 총알을 빼온다
    {
        Debug.Log("GetQueue실행!!");
        GameObject b_object = bulletPool.Dequeue();
        b_object.SetActive(true);
        return b_object;
    }
    void Fire()//발사하는 함수
    {
        if (!Input.GetButton("Fire1"))//Fire버튼을 안누르면 종료
        {
            return;
        }
        if (curShotDelay < maxShotDelay)//장전시간이 충족이안되면 종료
        {
            return;
        }
        //오브젝트 풀에 총알이 더 없다면 종료 시킨다
        if (bulletPool.Count<=0)
        {
            return;
        }
        GameObject B = GetQueue();
        if (rend.flipX)//왼쪽을 볼 때
        {
            B.transform.position = this.transform.position + Vector3.left * 2.0f + Vector3.up * 1.0f;
            //현재 위치보다 왼쪽위에 총알생성 
            Rigidbody2D rigid_bullet = B.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
        }
        else if (!rend.flipX)//오른쪽을 볼 때
        {
            B.transform.position = this.transform.position + Vector3.right * 2.0f + Vector3.up * 1.0f;
            //현재 위치보다 오른쪽위에 총알생성 
            Rigidbody2D rigid_bullet = B.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 15, ForceMode2D.Impulse);
        }
        if(curBulletIndex>=bulletMaxCount-1)
        {
            curBulletIndex = 0;
        }
        else
        {
            curBulletIndex++;
        }
        curShotDelay = 0;//꼭 초기화해줘야된다.
    }
}