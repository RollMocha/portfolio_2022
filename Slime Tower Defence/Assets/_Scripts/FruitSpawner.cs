using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public Transform[] fruits;// 열매
    public int fruitspawnrandom = 20;//열매 확률
    public int fruitsindex = 3;//열매 종류 숫자
    private Enemy_1 enemy_1;
    private RedFruitUI redFruitUI;//빨간 열매 관련 UI
    private YellowFruitUI yellowFruitUI;//노란 열매 관련 UI
    private BlueFruitUI blueFruitUI;//파란 열매 관련 UI

    public void Start()
    {
        enemy_1 = GetComponent<Enemy_1>();
        redFruitUI = GameObject.Find("FireFruit_UI").GetComponent<RedFruitUI>();
        yellowFruitUI = GameObject.Find("ThunderFruit_UI").GetComponent<YellowFruitUI>();
        blueFruitUI = GameObject.Find("IceFruit_UI").GetComponent<BlueFruitUI>();
    }

    public void SpawnFruit()
    {
        int random = UnityEngine.Random.Range(0, 100);//렌덤 수 생성

        if (random < fruitspawnrandom)//fruitspawnrandom보다 작으면
        {
            Debug.Log("당첨");
            int fruit_random = UnityEngine.Random.Range(0, fruitsindex);//열매 종류 렌덤 설정

            Instantiate(fruits[fruit_random], enemy_1.transform.position, enemy_1.transform.rotation);//열매 소환

            if (fruit_random == 0)//빨간 열매
            {
                redFruitUI.GetAddFruit();
            }
            if (fruit_random == 1)//노란 열매
            {
                yellowFruitUI.GetAddFruit();
            }
            if (fruit_random == 2)//파란 열매
            {
                blueFruitUI.GetAddFruit();
            }
        }
        else { Debug.Log("꽝"); }
    }
}
