using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour {

    int currentPoint;
    public static int totalPoint;

    [SerializeField] Text text_Point;
  
	
	// Update is called once per frame
	void Update () {
        currentPoint = totalPoint;
        text_Point.text = string.Format("{0:000000}", currentPoint);
    }
}
