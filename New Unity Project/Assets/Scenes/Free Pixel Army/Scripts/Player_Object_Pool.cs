using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Object_Pool : MonoBehaviour
{
    public static Player_Object_Pool Instance;
    public GameObject prefab_bullet;//총알 오브젝트 풀링
    public static Queue<GameObject> bulletPool = new Queue<GameObject>();//총알 Pool
    private readonly int bulletMaxCount = 50;//내가 생성할 총알 갯수
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bulletMaxCount; i++)//총알을 bulletMaxCount만큼 생성
        {
            GameObject b = Instantiate<GameObject>(prefab_bullet);
            b.SetActive(false);//총알 발사하기 전까지는 비활성화
            bulletPool.Enqueue(b);
        }
    }
    public static void InSertQueue(GameObject b_object)//사용한 총알을 오브젝트 풀에 다시 넣는다
    {
        bulletPool.Enqueue(b_object);
        b_object.SetActive(false);
    }
    public static GameObject GetQueue()//오브젝트 풀에서 총알을 빼온다
    {
        if(bulletPool.Count<=0)
        {
            return null;
        }
        else
        {
            GameObject b_object = bulletPool.Dequeue();
            b_object.SetActive(true);
            return b_object;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
