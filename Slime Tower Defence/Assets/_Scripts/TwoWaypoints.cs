using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWaypoints : MonoBehaviour
{
    public static Transform[] tpoints;//TwoWaypoints의 transform

    void Awake()//Start()보다 먼저 작동하는 함수
    {
        tpoints = new Transform[transform.childCount];//transform의 클래스를 가지고 있는 childObject개수만큼 객체를 생성

        for (int i = 0; i < tpoints.Length; i++)
        {
            tpoints[i] = transform.GetChild(i);//각각 tpoints의 객체에 대입
        }
    }
}
