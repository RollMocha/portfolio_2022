using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueFruitUI : MonoBehaviour
{
    public int currentfruit = 0; // 현재 갯수
    private FruitSpawner fruitSpawner;

    public Text fruitText; // 열매 텍스트
    public int addfruit = 1;//열매 갯수 추가

    private void Start()
    {
        fruitSpawner = GetComponent<FruitSpawner>();
    }

    private void Update()
    {
        fruitText.text = "X" + currentfruit;
    }

    public void GetAddFruit()
    {
        currentfruit = currentfruit + addfruit; //열매 갯수 포함
    }
    public void GetRemoveFruit()
    {
        currentfruit = currentfruit - addfruit; //열매 갯수 포함
    }
}