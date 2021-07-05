using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss_FSM : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;//HP바 관련
    public float height = 5.0f;
    Image nowHpbar;
    RectTransform hpBar;
    public string enemyName;
    public int maxHp;
    public int nowHp;
    private void SetEnmeyStatus(string _enemyName, int _maxHp)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
    }
    private void OnTriggerEnter2D(Collider2D col)//총알을 맞으면 피가 1단다
    {
        if (col.CompareTag("Bullet"))
        {
            nowHp = nowHp - 1;
        }
    }

    public enum CurrentState { idle,attack,walk,dead};//상태들 나타내는 enum
    public CurrentState curState = CurrentState.idle;//초기상태는 idle

    private Transform _transform;
    private Transform _player_transform;

    public SpriteRenderer rend;

    public float traceDist = 20.0f;//추적거리
    public float attackDist = 6.5f;//공격거리

    private Animator _animator;

    //사망여부
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {

        //HP바 관련
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        SetEnmeyStatus("Enemy1", 100);
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
        //HP바 관련

        _transform = this.gameObject.GetComponent<Transform>();
        _player_transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _animator = this.gameObject.GetComponent<Animator>();
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }
    IEnumerator CheckState()
    {
        while(!isDead)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector2.Distance(_player_transform.position, _transform.position);
            if(nowHp<=0)//현재 체력이 0이하라면 curState는 die
            {
                curState = CurrentState.dead;
            }
            else if(dist<=attackDist)//거리가 attackDist안에 있다면 curState는 attack
            {
                curState = CurrentState.attack;
            }
            else if(dist <= traceDist)//거리가 traceDist안에 있다면 curState는 walk
            {
                curState = CurrentState.walk;
            }
            else
            {
                curState = CurrentState.idle;
            }
        }
    }
    IEnumerator CheckStateForAction()
    {
        
        while(!isDead)
        {
            switch (curState)
            {
                
                case CurrentState.idle:
                    _animator.SetBool("ismoving", false);
                    _animator.SetBool("isattack", false);
                    HP();
                    break;
                case CurrentState.walk:
                    if (_player_transform.position.x - _transform.position.x < 0) // 타겟이 왼쪽에 있을 때
                    {
                        rend.flipX = false;
                    }
                    else // 타겟이 오른쪽에 있을 때
                    {
                        rend.flipX = true;
                    }
                    MoveToTarget();
                    _animator.SetBool("ismoving", true);
                    _animator.SetBool("isattack", false);
                    HP();
                    break;
                case CurrentState.attack:
                    _animator.SetBool("ismoving", false);
                    _animator.SetBool("isattack", true);
                    HP();
                    break;
                case CurrentState.dead:
                    _animator.SetBool("isdie", true);
                    _animator.SetBool("ismoving", false);
                    _animator.SetBool("isattack", false);
                    Destroy(hpBar.gameObject);
                    yield return new WaitForSeconds(1.65f);
                    isDead = true;
                    Destroy(this.gameObject);
                    break;
            }
            yield return null;
        }
    }
    // Update is called once per frame
    public void MoveToTarget()
    {
        float dir = _player_transform.position.x - _transform.position.x;
        dir = (dir < 0) ? -1 : 1;//참이면 -1 거짓이면 1
        _transform.Translate(new Vector2(dir, 0) * 3.0f * Time.deltaTime);
    }
    public void HP()//hp바 위치와 피 깍이는거 구현 함수
    {
        Vector3 _hpBarPos =
           Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
    }
}
