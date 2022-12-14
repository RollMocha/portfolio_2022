using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ���� UI �ڵ�
public class Make_Slimes : MonoBehaviour
{
    public GameObject MakeSlimeButtonObj; //������ ���� ��ư
    public GameObject DefaultSlimesObj; //���� ������ ���
    public GameObject PromoteSlimesObj; //���� ������ ���

    public void ClickMakeSlimeButton()//������ ���� ��ư Ŭ��
    {
        MakeSlimeButtonObj.SetActive(false);
        DefaultSlimesObj.SetActive(true);
        //���� ������ ��� Ȱ��ȭ
    }

    public void ClickDefalutSlimesButton()//���� ������ ��ư Ŭ��
    {
        DefaultSlimesObj.SetActive(true);
        PromoteSlimesObj.SetActive(false);
        //���� ������ ��� Ȱ��ȭ
    }

    public void ClickPromoteSlimesButton()//���� ������ ��ư Ŭ��
    {
        DefaultSlimesObj.SetActive(false);
        PromoteSlimesObj.SetActive(true);
        //���� ������ ��� Ȱ��ȭ
    }

    public void ClickExit()//�ݱ��ư Ŭ��
    {
        DefaultSlimesObj.SetActive(false);
        PromoteSlimesObj.SetActive(false);
        MakeSlimeButtonObj.SetActive(true);
    }
}
