using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;

    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public int atkSpeed;
    RectTransform hpBar;
    public float height = 5.0f;
    // Start is called before the first frame update
    private void SetEnmeyStatus(string _enemyName, int _maxHp,int _atkDmg,int _atkSpeed)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
    }
    Image nowHpbar;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bullet"))
        {
            nowHp = nowHp-10;
            Debug.Log(nowHp);
            if(nowHp<=0)
            {
                Destroy(gameObject);
                Destroy(hpBar.gameObject);
            }
        }
    }
    void Start()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        SetEnmeyStatus("Enemy1", 100, 10, 1);    
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 _hpBarPos =
           Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
    }
}
