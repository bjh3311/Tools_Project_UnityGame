﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_FSM : MonoBehaviour
{
    public enum CurrentState { idle,attack,walk,dead};
    public CurrentState curState = CurrentState.idle;//초기상태는 idle
    private Transform _transform;
    private Transform _player_transform;

    public SpriteRenderer rend;

    public float traceDist = 20.0f;//추적거리
    public float attackDist = 4.0f;//공격거리

    private Animator _animator;

    //사망여부
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        _player_transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _animator = this.gameObject.GetComponent<Animator>();
        StartCoroutine(this.CheckState());//현재상태를 체크하는 코루틴 시작
        StartCoroutine(this.CheckStateForAction());
        rend = this.gameObject.GetComponent<SpriteRenderer>();
    }

    IEnumerator CheckState()
    {
        while(!isDead)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector2.Distance(_player_transform.position, _transform.position);
            if(dist<=attackDist)//
            {
                curState = CurrentState.attack;
            }
            else if(dist<=traceDist)//추적거리 안에있다면 walk 실행
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
            switch(curState)
            {
                case CurrentState.idle:
                    _animator.SetBool("ismoving", false);
                    _animator.SetBool("isattack", false);
                    break;
                case CurrentState.walk:
                    if(_player_transform.position.x-_transform.position.x<0)//플레이어를 바라보게 해주는 함수
                    {
                        rend.flipX = true;
                    }
                    else
                    {
                        rend.flipX = false;
                    }
                    _animator.SetBool("ismoving", true);
                    _animator.SetBool("isattack", false);
                    MoveToTarget();
                    break;
                case CurrentState.attack:
                    _animator.SetBool("ismoving", false);
                    _animator.SetBool("isattack", true);
                    break;
            }
        }
        yield return null;

    }
    // Update is called once per frame
    public void MoveToTarget()
    {
        float dir = _player_transform.position.x - _transform.position.x;
        dir = (dir < 0) ? -1 : 1;//참이면 -1 거짓이면 1
        _transform.Translate(new Vector2(dir, 0) * 3.0f * Time.deltaTime);
    }
    void Update()
    {
        
    }
}
