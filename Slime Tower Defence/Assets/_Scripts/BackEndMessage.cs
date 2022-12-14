using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackEndMessage : MonoBehaviour
{
    //[Header("Login & Register")]
    public InputField su_ID;
    public InputField su_PW;
    public InputField su_Email;

    public InputField ID;
    public InputField PW;

    Change_Panel change_Panel = new Change_Panel();
    BackendReturnObject bro = new BackendReturnObject();
    //bool isSuccess = false;

    void Start()
    {
        // �� ��° ��� (�񵿱�)
        Backend.InitializeAsync(true, callback => {
            if (callback.IsSuccess())
            {
                Debug.Log("�ʱ�ȭ ����!");
                // �ʱ�ȭ ���� �� ����
            }
            else
            {
                Debug.LogError("�ʱ�ȭ ����!");
                // �ʱ�ȭ ���� �� ����
            }
        });
    }

    void Update()
    {
        Backend.AsyncPoll();
    }

    //�ʱ�ȭ ���� ���� ��ư ���� ���� �Լ� ����
    public void CustomSignUp()
    {
        string id = su_ID.text; // ���ϴ� ���̵�
        string password = su_PW.text; // ���ϴ� ��й�ȣ
        string email = su_Email.text;

        Backend.BMember.CustomSignUp(id, password, callback => {
            if (callback.IsSuccess())
            {
                Debug.Log("ȸ�����Կ� �����߽��ϴ�");
                change_Panel.onClickChangeLoginButton();
            }
            else
            {
                Debug.LogError("ȸ������ ����!");
                Debug.LogError(bro); // �ڳ��� �������̽��� �α׷� �����ݴϴ�.
            }
        });
        /*
        Backend.BMember.UpdateCustomEmail(email, (callback) => {
            if (callback.IsSuccess())
            {
                Debug.Log("�̸��� ���Կ� �����߽��ϴ�");
            }
            else
            {
                Debug.LogError("�̸��� ���� ����!");
                Debug.LogError(bro); // �ڳ��� �������̽��� �α׷� �����ݴϴ�.
            }
        });
        */
    }

    public void CustomLogin()
    {
        string id = ID.text; // ���ϴ� ���̵�
        string password = PW.text; // ���ϴ� ��й�ȣ

        Backend.BMember.CustomLogin(id, password, callback => {
            if (callback.IsSuccess())
            {
                Debug.Log("�α��ο� �����߽��ϴ�");
                TitleSceneChange();
            }
            else
            {
                Debug.LogError("�α��� ����!");
                Debug.LogError(bro); // �ڳ��� �������̽��� �α׷� �����ݴϴ�.
            }
        });
    }

    public void TitleSceneChange()//���� �� �ε�
    {
        SceneManager.LoadScene("TitleScene");
    }
}