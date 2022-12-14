using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfGenerator : MonoBehaviour
{

    public GameObject blue_shelf_Prefab;
    float span = 1.0f;
    float delta = 0;

    void Start()
    {

    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate<GameObject>(blue_shelf_Prefab);
            int px = Random.Range(-8, 30);
            go.transform.position = new Vector3(px, 8, 0);
        }
    }
}
