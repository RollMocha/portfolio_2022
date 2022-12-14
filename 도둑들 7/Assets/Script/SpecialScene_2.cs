using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialScene_2 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)

    {

        SceneManager.LoadScene("SpecialScene_2");

    }
}
