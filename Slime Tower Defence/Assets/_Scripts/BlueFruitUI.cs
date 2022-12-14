using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueFruitUI : MonoBehaviour
{
    public int currentfruit = 0; // ���� ����
    private FruitSpawner fruitSpawner;

    public Text fruitText; // ���� �ؽ�Ʈ
    public int addfruit = 1;//���� ���� �߰�

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
        currentfruit = currentfruit + addfruit; //���� ���� ����
    }
    public void GetRemoveFruit()
    {
        currentfruit = currentfruit - addfruit; //���� ���� ����
    }
}