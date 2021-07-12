using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Object_Pool : MonoBehaviour
{
    public static Boss_Object_Pool Instance;
    public GameObject prefab_fireball;//fireball 오브젝트 풀링
    public static Queue<GameObject> fireballPool = new Queue<GameObject>();//fireball Pool
    private readonly int fireballMaxCount = 5;//내가 생성할 fireball 갯수
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < fireballMaxCount; i++)//fireball을 fireballMaxCount만큼 생성
        {
            GameObject b = Instantiate<GameObject>(prefab_fireball);
            b.SetActive(false);//fireball 생성할때까지는 비활성화
            fireballPool.Enqueue(b);
        }
    }
    public static void InSertQueue(GameObject f_object)//사용한 fireball을 오브젝트 풀에 다시 넣는다
    {
        fireballPool.Enqueue(f_object);
        f_object.SetActive(false);
    }
    public static GameObject GetQueue()//오브젝트 풀에서 fireball을 빼온다
    {
        
        if (fireballPool.Count <= 0)//비어있으면 아무것도 반환하지 않는다
        {
            return null;
        }
        else
        {
            GameObject f_object = fireballPool.Dequeue();
            f_object.SetActive(true);
            return f_object;
        }
    }
}
