using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;

    public SpriteRenderer rend;

    public Transform target;// 타겟을 지정 boss의 타겟은 Player다
    public float fieldOfVision=30f;//boss의 시야 범위

    public float maxShotDelay;
    public float curShotDelay;//발사간 속도
    public GameObject bulletObj;

    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public int atkSpeed;
    RectTransform hpBar;
    public float height = 5.0f;
    // Start is called before the first frame update
    private void SetEnmeyStatus(string _enemyName, int _maxHp,int _atkDmg,int _atkSpeed)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
    }
    Image nowHpbar;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bullet"))
        {
            nowHp = nowHp-1;
            if(nowHp<=0)
            {
                Destroy(gameObject);
                Destroy(hpBar.gameObject);
            }
        }
    }
    void Start()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        SetEnmeyStatus("Enemy1", 100, 10, 1);
        rend = GetComponent<SpriteRenderer>();
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        Reload();
        float distance = Vector3.Distance(transform.position, target.position);//BOSS와 Player간의 거리
        if(distance<=fieldOfVision)
        {
            FaceTarget();
            Fire();//일정거리 안에 있을때 공격
        }
        Vector3 _hpBarPos =
           Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
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
        int random = Random.Range(1, 10);
        if (random>=1&&random<4)
        {
            GameObject bullet = Instantiate(bulletObj, transform.position + Vector3.left * 2.5f + Vector3.up * 1.5f, transform.rotation);
            Rigidbody2D rigid_bullet = bullet.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
        }
        else if(random>=4&&random<7)
        {
            GameObject bullet2 = Instantiate(bulletObj, transform.position + Vector3.left * 2.5f + Vector3.up * -0.2f, transform.rotation);
            Rigidbody2D rigid_bullet_2 = bullet2.GetComponent<Rigidbody2D>();
            rigid_bullet_2.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
        }
        else
        {
            GameObject bullet3 = Instantiate(bulletObj, transform.position + Vector3.left * 2.5f + Vector3.up * -2.7f, transform.rotation);
            //현재 위치보다 왼쪽위에 총알생성 
            Rigidbody2D rigid_bullet_3 = bullet3.GetComponent<Rigidbody2D>();
            rigid_bullet_3.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
        }
        curShotDelay = 0;//꼭 초기화해줘야된다.
    }

    void FaceTarget()//Player를 바라보게 만들어주는 함수
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            rend.flipX = false;
        }
        else // 타겟이 오른쪽에 있을 때
        {
            rend.flipX = true;
        }
    }
}
