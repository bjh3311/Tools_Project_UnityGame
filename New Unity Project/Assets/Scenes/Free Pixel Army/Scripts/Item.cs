using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Player p;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        p = Player.GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)//플레이어와 만나면
    {
        if(col.CompareTag("Bullet"))
        {
            p.maxShotDelay = 0.05f;
            Destroy(this.gameObject);
        }
    }
}
