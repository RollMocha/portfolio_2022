using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public Transform[] fruits;// ����
    public int fruitspawnrandom = 20;//���� Ȯ��
    public int fruitsindex = 3;//���� ���� ����
    private Enemy_1 enemy_1;
    private RedFruitUI redFruitUI;//���� ���� ���� UI
    private YellowFruitUI yellowFruitUI;//��� ���� ���� UI
    private BlueFruitUI blueFruitUI;//�Ķ� ���� ���� UI

    public void Start()
    {
        enemy_1 = GetComponent<Enemy_1>();
        redFruitUI = GameObject.Find("FireFruit_UI").GetComponent<RedFruitUI>();
        yellowFruitUI = GameObject.Find("ThunderFruit_UI").GetComponent<YellowFruitUI>();
        blueFruitUI = GameObject.Find("IceFruit_UI").GetComponent<BlueFruitUI>();
    }

    public void SpawnFruit()
    {
        int random = UnityEngine.Random.Range(0, 100);//���� �� ����

        if (random < fruitspawnrandom)//fruitspawnrandom���� ������
        {
            Debug.Log("��÷");
            int fruit_random = UnityEngine.Random.Range(0, fruitsindex);//���� ���� ���� ����

            Instantiate(fruits[fruit_random], enemy_1.transform.position, enemy_1.transform.rotation);//���� ��ȯ

            if (fruit_random == 0)//���� ����
            {
                redFruitUI.GetAddFruit();
            }
            if (fruit_random == 1)//��� ����
            {
                yellowFruitUI.GetAddFruit();
            }
            if (fruit_random == 2)//�Ķ� ����
            {
                blueFruitUI.GetAddFruit();
            }
        }
        else { Debug.Log("��"); }
    }
}
