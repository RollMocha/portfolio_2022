using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�� ��ȯ�� ���� ��ũ��Ʈ
public class SceneDirector : MonoBehaviour
{
    public void GameSceneChange()//���� �� �ε�
    {
        SceneManager.LoadScene("GameScene");
    }

    public void TitleSceneChange()//Ÿ��Ʋ �� �ε�
    {
        SceneManager.LoadScene("TitleScene");
    }
}
