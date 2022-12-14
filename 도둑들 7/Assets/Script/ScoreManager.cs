using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    int currentScore; // 현재 점수
    public static int extraScore; // 아이템 점수


    [SerializeField] Text text_Score;
	
	// Update is called once per frame
	void Update () {
        currentScore = extraScore;
        text_Score.text = string.Format("{0:000000}", currentScore);
	}


}
