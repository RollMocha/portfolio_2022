using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart_Generator : MonoBehaviour {

    public GameObject Cart1;
    public GameObject Cart2;

    float span = 1.0f;
    float delta = 0;

    public float ax ,bx;
    public float ay ,by;

    void Start()
    {

    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go1 = Instantiate<GameObject>(Cart1);
            GameObject go2 = Instantiate<GameObject>(Cart2);

            go1.transform.position = new Vector3(ax, ay, 0);
            go2.transform.position = new Vector3(bx, by, 0);
        }
    }
}
