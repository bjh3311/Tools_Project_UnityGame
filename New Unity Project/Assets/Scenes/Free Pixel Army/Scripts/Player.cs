using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movePower = 3f;
    public float jumpPower = 3f;
    Rigidbody2D rigid;

    Vector3 movemet;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame , 1분에 약60번 업데이트
    void Update()
    {
        if(Input.GetButton("Jump"))
        {
            isJumping = true;
        }
    }
    void FixedUpdate()
    {
        Move();
        Jump();
    }
   void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1f, 1f, 1f);
            //X값 스케일을 -1로 주어 좌우반전
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1f, 1f, 1f);
            //X값 스케일을 1로 주어 다시 원위치 
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Jump()
    {
        if(!isJumping)//Jump버튼이 눌리지않았다면
        {
            return;//종료
        }
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        isJumping = false;
    }
}
