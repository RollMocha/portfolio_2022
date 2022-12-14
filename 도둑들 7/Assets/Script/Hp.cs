using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour {
    [SerializeField] int maxHp;
    int currentHp;

    [SerializeField] Text[] txt_HpArray;


    void Start()
    {
        currentHp = maxHp;
        UpdateHpStatus();
    }

    public void Decrease(int _num)
    {
         currentHp -= _num;
         UpdateHpStatus();

        if (currentHp <= 0)
            PlayerDead();
    }

    public void Increase(int _num)
    {
        if (currentHp == maxHp)
            return;
        
        currentHp += _num;

        if (currentHp > maxHp)
            currentHp = maxHp;

        UpdateHpStatus();
    }


   void UpdateHpStatus()
    {
       // for(int i= 0; i< txt_HpArray.Length; i++)
        {
           // if (i < currentHp)
           //     txt_HpArray[i].gameObject.SetActive(true);
          //  else if
           //     (i < currentHp)
             //   txt_HpArray[i].gameObject.SetActive(false);
        }
    }

    void PlayerDead()
    {
        Debug.Log("죽엇습니다");
    }
}
