using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Fsm : MonoBehaviour
{
    public enum CurrentState { idle,attack,walk};
    public CurrentState curState = CurrentState.idle;//초기상태는 idle
    private Transform _transform;
    private Transform _player_transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        _player_transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        StartCoroutine(this.CheckState());//현재상태를 체크하는 코루틴 시작
    }

    IEnumerator CheckState()
    {
        yield return new WaitForSeconds(0.2f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
