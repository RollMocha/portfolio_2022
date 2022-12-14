using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart_Scene2 : MonoBehaviour {

    float carttime;
    // Use this for initialization
    void Start () {
        carttime = 8;
	}
	
	// Update is called once per frame
	void Update () {

        if (carttime > 0)
            transform.Translate(-0.5f, 0, 0);

        else
            Destroy(gameObject);
    }
}
