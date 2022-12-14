using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    GameObject Girl;
    ButtonController bun;
    // Use this for initialization
    void Start()
    {
        Girl = GameObject.Find("Player");
        bun = Girl.GetComponent<ButtonController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LeftBtnDown()
    {
        bun.LeftMove = true;
    }

    public void LeftBtnUp()
    {
        bun.LeftMove = false;
    }

    public void RightBtnDown()
    {
        bun.LeftMove = true;
    }

    public void RightBtnUp()
    {
        bun.LeftMove = false;
    }
}
