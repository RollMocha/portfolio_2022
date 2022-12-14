using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HowToPlay 오브젝트 활성화/ 비활성화
public class SetActiveHTP : MonoBehaviour
{
    public GameObject HowToPlayobj;
    //public GameObject Titleobj;
    public GameObject Describe2obj;

    public void ClickHTPButton()//HowToPlayButton 클릭시
    {
        HowToPlayobj.SetActive(true);
        //HowToPlayDiscribe오브젝트 활성화
        //Titleobj.SetActive(false);
    }

    public void ClickHTP()//HowToPlayDescribe 클릭시
    {
        HowToPlayobj.SetActive(false);
        //HowToPlayDiscribe오브젝트 비활성화
        Describe2obj.SetActive(true);
    }

    public void ClickDescribe2()//Describe2 클릭시
    {
        Describe2obj.SetActive(false);
        //Describe2 비활성화
        //Titleobj.SetActive(true);
    }
}
