using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWaypoints : MonoBehaviour
{
    public static Transform[] tpoints;//TwoWaypoints�� transform

    void Awake()//Start()���� ���� �۵��ϴ� �Լ�
    {
        tpoints = new Transform[transform.childCount];//transform�� Ŭ������ ������ �ִ� childObject������ŭ ��ü�� ����

        for (int i = 0; i < tpoints.Length; i++)
        {
            tpoints[i] = transform.GetChild(i);//���� tpoints�� ��ü�� ����
        }
    }
}
