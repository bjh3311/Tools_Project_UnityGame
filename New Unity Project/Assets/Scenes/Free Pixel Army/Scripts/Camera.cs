using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(Player.transform.localPosition.x, 1.95f, -10f);
    }
}
