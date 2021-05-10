using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Weapon w;//스크립트
    public GameObject Weapons_0;//무기 오브젝트
    SpriteRenderer rend;
    Rigidbody2D rigid;
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
        rigid.velocity = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))//왼쪽 방향키 누르면
        {
            w.yFlip();
            rend.flipX = true;
            Weapons_0.transform.localPosition = new Vector3(-0.66f, 0.38f, 0);
            rigid.velocity=new Vector2(-10f,0);
            
        }
        if(Input.GetKey(KeyCode.RightArrow))//오른쪽 방향키 누르면
        {
            w.nFlip();
            Weapons_0.transform.localPosition = new Vector3(0.66f, 0.38f, 0);
            rend.flipX = false;
            rigid.velocity=new Vector2(10f,0);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigid.AddForce(new Vector2(0, 50f));
        }

    } 
}
