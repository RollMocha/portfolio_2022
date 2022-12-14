using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public GameObject Hpgauge;

    void Start()
    {
        if(Hpgauge != null)
        {
            this.Hpgauge = GameObject.Find("Hpgauge");
        }
        else
        {
            Debug.Log("No game object called wibble found1");
        }
	}

    public void DecreaseHp()
    {
        if (Hpgauge != null)
        {
            this.Hpgauge.GetComponent<Image>().fillAmount -= 0.1f;
        }

        else
        {
            Debug.Log("No game object called wibble found2");
        }
    }

    public void HirHp()
    {
        if (Hpgauge != null)
        {
            if (this.Hpgauge.GetComponent<Image>().fillAmount < 0.5f)
                this.Hpgauge.GetComponent<Image>().fillAmount += 0.5f;

            else
                this.Hpgauge.GetComponent<Image>().fillAmount += (1 - this.Hpgauge.GetComponent<Image>().fillAmount);
        }

        else
        {
            Debug.Log("No game object called wibble found3");
        }
    }
}