using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 speed_vec;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame , 1분에 약60번 업데이트
    void Update()
    {
        speed_vec = Vector2.zero;//1초마다 계속 0으로 업데이트
        if(Input.GetKey(KeyCode.RightArrow))//오른쪽 방향키 누르면
        {
            speed_vec.x += 0.1f;
        }
        if(Input.GetKey(KeyCode.LeftArrow))//왼쪽 방향키 누르면
        {
            speed_vec.x += -0.1f;
        }
        if(Input.GetKey(KeyCode.Space))//스페이스바 누르면
        {
            speed_vec.y += 0.1f;
        }
        transform.Translate(speed_vec);
    }
}
