using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Panel : MonoBehaviour
{
    public GameObject Login_Panel;
    public GameObject Sign_up_Panel;

    public void onClickChangeSignUpButton()//ChangeSignUpButton 클릭시
    {
        Login_Panel.SetActive(false);
        Sign_up_Panel.SetActive(true);
    }

    public void onClickChangeLoginButton()//ChangeSignUpButton 클릭시
    {
        Login_Panel.SetActive(true);
        Sign_up_Panel.SetActive(false);
    }
}
