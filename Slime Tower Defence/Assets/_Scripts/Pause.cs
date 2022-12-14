using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject UnPauseObj;
    public bool isPause = false;

    public void ClickPause()
    {
            Time.timeScale = 0;
            Debug.Log("Pause Start");
            isPause = true;
            UnPauseObj.SetActive(true);
    }

    public void ClickUnPause()
    {
        Time.timeScale = 1;
        Debug.Log("Pause End");
        isPause = false;
        UnPauseObj.SetActive(false);
    }
}
