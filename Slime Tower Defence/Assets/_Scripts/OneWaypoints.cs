using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWaypoints : MonoBehaviour
{
    public static Transform[] opoints;//OneWaypoints의 transform

    void Awake()//Start()보다 먼저 작동하는 함수
    {
        opoints = new Transform[transform.childCount];//transform의 클래스를 가지고 있는 childObject개수만큼 객체를 생성

        for (int i = 0; i < opoints.Length; i++)
        {
            opoints[i] = transform.GetChild(i);//각각 opoints의 객체에 대입
        }
    }
}
