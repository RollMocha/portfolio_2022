using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public float damage = 1f; //���� ��� �� ������
    static public float CurrentHP; //�÷��̾� ���� ü��
    public float MaxHP = 20f; //�÷��̾� �ִ� ü��

    public GameObject GameOverobj;
    public Text PlayerHP;

    private void Awake()
    {
        Time.timeScale = 1;
        CurrentHP = MaxHP; //����ü���� �ִ�ü������ �ʱ�ȭ
        PlayerHP.text = "HP: " + CurrentHP;
    }

    void Update()
    {
        PlayerHP.text = "HP: " + CurrentHP;
    }

    void CheckGameOver() //�÷��̾� ü���� 0 ���Ϸ� �������� ���ӿ���
    {
        if (CurrentHP <= 0)
        {
            Time.timeScale = 0;
            GameOverobj.SetActive(true);//���ӿ��� ��ư �� �ؽ�Ʈ Ȱ��ȭ
        }
    }

    void OnTriggerEnter(Collider other) //���Ϳ� �浹 ����, ���� �ı� �� �÷��̾� ü�� ����
    {
        if (other.tag == "Monster" && HPManager.CurrentHP > 0)
        {
            CurrentHP = CurrentHP - damage; //�÷��̾� ü�°���
            //Destroy(other.gameObject); //���� �ı�
            CheckGameOver();//���ӿ��� �Ǻ�

            return;
        }
    }
}
