using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWave : MonoBehaviour
{
    public int addwave = 1; //���̺� ��°�
    public int currentWave; //�÷��̾� ���� ���̺�
    public int MaxWave = 5; //�ִ� ���̺�

    public Text PlayerWave;

    private void Awake()
    {
        currentWave = 0; //������̺� �ʱ�ȭ
    }

    void Update()
    {
        
    }

    public void GetAddWave()
    {
        currentWave = currentWave + addwave; //���̺� ����
    }
}
