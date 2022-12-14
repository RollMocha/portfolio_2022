using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//슬라임 생성 UI 코드
public class Make_Slimes : MonoBehaviour
{
    public GameObject MakeSlimeButtonObj; //슬라임 생성 버튼
    public GameObject DefaultSlimesObj; //하위 슬라임 목록
    public GameObject PromoteSlimesObj; //상위 슬라임 목록

    public void ClickMakeSlimeButton()//슬라임 생성 버튼 클릭
    {
        MakeSlimeButtonObj.SetActive(false);
        DefaultSlimesObj.SetActive(true);
        //하위 슬라임 목록 활성화
    }

    public void ClickDefalutSlimesButton()//하위 슬라임 버튼 클릭
    {
        DefaultSlimesObj.SetActive(true);
        PromoteSlimesObj.SetActive(false);
        //하위 슬라임 목록 활성화
    }

    public void ClickPromoteSlimesButton()//상위 슬라임 버튼 클릭
    {
        DefaultSlimesObj.SetActive(false);
        PromoteSlimesObj.SetActive(true);
        //상위 슬라임 목록 활성화
    }

    public void ClickExit()//닫기버튼 클릭
    {
        DefaultSlimesObj.SetActive(false);
        PromoteSlimesObj.SetActive(false);
        MakeSlimeButtonObj.SetActive(true);
    }
}
