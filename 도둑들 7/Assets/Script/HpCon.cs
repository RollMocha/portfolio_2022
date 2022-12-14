using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCon : MonoBehaviour
{
    GameObject Player;

    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    void Update()
    {
        
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
        

        Vector2 p1 = transform.position;
        Vector2 p2 = this.Player.transform.position;
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 1.0f;
        float r2 = 1.0f;

        if (d < r1 + r2)
        {
             GameObject director = GameObject.Find("GameDirector");
             director.GetComponent<GameDirector>().HirHp();
        }
    }
}
