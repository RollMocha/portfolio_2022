using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Enemy_HP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;     //�ִ� ü��
    private float currentHP; // ���� ü��
    private bool isDie = false; // ���� ��� �����̸� isDie�� true�� ����
    private Enemy_1 enemy_1;
    private FruitSpawner fruitSpawner;
    public int Kill = 0;

    private void Awake()
    {
        currentHP = maxHP; // ���� ü���� �ִ� ü�°� ���� ����
        enemy_1 = GetComponent<Enemy_1>();
        fruitSpawner = GetComponent<FruitSpawner>();
    }

    public void TakeDamage(float damage)
    {
        if (isDie == true) return; // ���� ���� ���°� ��� �����̸� �Ʒ� �ڵ带 �������� �ʴ´�.

        currentHP -= damage; // ���� ü���� damage��ŭ ����
        Debug.Log("ü�� ����");

        if (currentHP <= 0f) // ü���� 0���� = �� ĳ���� ���
        {
            isDie = true; // �� ĳ���� ���
            Destroy(gameObject);
            fruitSpawner.SpawnFruit();
        }
    }
}
*/