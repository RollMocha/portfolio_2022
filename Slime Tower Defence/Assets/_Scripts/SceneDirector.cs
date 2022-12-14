using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//씬 전환을 위한 스크립트
public class SceneDirector : MonoBehaviour
{
    public void GameSceneChange()//게임 씬 로드
    {
        SceneManager.LoadScene("GameScene");
    }

    public void TitleSceneChange()//타이틀 씬 로드
    {
        SceneManager.LoadScene("TitleScene");
    }
}
