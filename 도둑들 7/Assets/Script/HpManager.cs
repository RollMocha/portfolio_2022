using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    int currentHp; // 현재 점수
    public static int extraHp; // 아이템 점수

    [SerializeField] Text text_Hp;

    // Update is called once per frame
    void Update()
    {
        currentHp = extraHp;
        text_Hp.text = string.Format("♥", currentHp);
    }
}