﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public Transform target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10f);
    }
}
