using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Enemy_HP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;     //최대 체력
    private float currentHP; // 현재 체력
    private bool isDie = false; // 적이 사망 상태이면 isDie를 true로 설정
    private Enemy_1 enemy_1;
    private FruitSpawner fruitSpawner;
    public int Kill = 0;

    private void Awake()
    {
        currentHP = maxHP; // 현재 체력을 최대 체력과 같게 설정
        enemy_1 = GetComponent<Enemy_1>();
        fruitSpawner = GetComponent<FruitSpawner>();
    }

    public void TakeDamage(float damage)
    {
        if (isDie == true) return; // 현재 적의 상태가 사망 상태이면 아래 코드를 실행하지 않는다.

        currentHP -= damage; // 현재 체력을 damage만큼 감소
        Debug.Log("체력 감소");

        if (currentHP <= 0f) // 체력이 0이하 = 적 캐릭터 사망
        {
            isDie = true; // 적 캐릭터 사망
            Destroy(gameObject);
            fruitSpawner.SpawnFruit();
        }
    }
}
*/