using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWaypoints : MonoBehaviour
{
    public static Transform[] opoints;//OneWaypoints�� transform

    void Awake()//Start()���� ���� �۵��ϴ� �Լ�
    {
        opoints = new Transform[transform.childCount];//transform�� Ŭ������ ������ �ִ� childObject������ŭ ��ü�� ����

        for (int i = 0; i < opoints.Length; i++)
        {
            opoints[i] = transform.GetChild(i);//���� opoints�� ��ü�� ����
        }
    }
}
