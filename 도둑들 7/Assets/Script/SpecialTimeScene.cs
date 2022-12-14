using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialTimeScene : MonoBehaviour
{
    public Text TimeCount;
    public float TimeCost;

    void Start()
    {
        TimeCost = 30;
    }

    void Update()
    {
        if (TimeCost > 0)
        {
            TimeCost -= Time.deltaTime;
        }

        else
        {
            SceneManager.LoadScene("project_2");
        }

        if (TimeCount)
        {
            TimeCount.text = "Time: " + (int)TimeCost;
        }
        else
        {
            Debug.Log("No game object called wibble found");
        }
    }
}
