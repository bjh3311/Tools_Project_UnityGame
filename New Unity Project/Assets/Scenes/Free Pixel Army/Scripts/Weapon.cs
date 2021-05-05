using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    SpriteRenderer rend;
    public Vector2 speed_vec;
    public Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame , 1분에 약60번 업데이트
    void Update()
    {
        Vector2 position = gameObject.transform.localPosition;
        speed_vec = Vector2.zero;//1초마다 계속 0으로 업데이트
        if (Input.GetKey(KeyCode.RightArrow))//오른쪽 방향키 누르면
        {
            if(rend.flipX)//원래 왼쪽을 보고있었다면
            {
                position.x = position.x + 0.205f;//2.05는 캐릭터의 가로 크기
                transform.position = new Vector2(position.x, position.y);
            }
            rend.flipX = false;//오른쪽이면 뒤집지 않는다
            speed_vec.x += 0.1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))//왼쪽 방향키 누르면
        {
            if (rend.flipX)//원래 왼쪽을 보고있었다면
            {
                position.x = position.x - 0.205f;//2.05는 캐릭터의 가로 크기
                transform.position = new Vector2(position.x, position.y);

            }
            rend.flipX = true;//왼쪽이면 뒤집는다
            speed_vec.x += -0.1f;
        }
        if (Input.GetKey(KeyCode.Space))//스페이스바 누르면
        {
            speed_vec.y += 0.1f;
        }
        transform.Translate(speed_vec);
    }
}
