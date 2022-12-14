using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWave : MonoBehaviour
{
    public int addwave = 1; //웨이브 상승값
    public int currentWave; //플레이어 현재 웨이브
    public int MaxWave = 5; //최대 웨이브

    public Text PlayerWave;

    private void Awake()
    {
        currentWave = 0; //현재웨이브 초기화
    }

    void Update()
    {
        
    }

    public void GetAddWave()
    {
        currentWave = currentWave + addwave; //웨이브 증가
    }
}
