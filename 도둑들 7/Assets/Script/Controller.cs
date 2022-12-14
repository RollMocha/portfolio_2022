using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    GameObject Player;
    float tc = 0;

    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    void Update()
    {
        /*
        transform.Translate(0, -0.1f, 0);
        
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
        */

        Vector2 p1 = transform.position;
        Vector2 p2 = this.Player.transform.position;
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 1.0f;
        float r2 = 1.0f;

        if (d < r1 + r2)
        {
            if(tc == 0)
            {
                
                tc = tc + 60;
            }

            else
            {
                tc = tc - 1;
            }
        }
    }
}
