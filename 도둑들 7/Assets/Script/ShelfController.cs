using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    GameObject Player;

    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Translate(0, -0.3f, 0);
        if (transform.position.y < -15.0f)
        {
            Destroy(gameObject);
        }

        Vector2 p1 = transform.position;
        Vector2 p2 = this.Player.transform.position;
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 2.0f;
        float r2 = 2.0f;

        if (d < r1 + r2)
        {
            Destroy(gameObject);

        }
    }
}

