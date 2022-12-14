using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;

    void Start()
    {
        //PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
       else if(!paused)
       {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
       }
    }

    public void Resume()
    {
        paused = !paused;
    }
}
