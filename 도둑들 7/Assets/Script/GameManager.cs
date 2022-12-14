using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public GirlController girl;
    public AudioClip audioDie;
    public GameObject player;

    public Image[] UIhealth;
    public Text UIPoint;
    public GameObject UIRestartButton;

    public GameObject menuSet;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    void Start()
    {
        GameLoad();
    }

    private void Update() //UI점수 및 UI버튼 부분
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Sub Menu
        if (Input.GetButtonDown("Cancel")) {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else 
                menuSet.SetActive(true);
        }
    }

    public void NextStage() // 다음스테이지
    {
        stageIndex++;

        totalPoint += stagePoint;
        stagePoint = 0;

        UIRestartButton.SetActive(true);
        Text buttonText = UIRestartButton.GetComponentInChildren<Text>(); //버튼
    }

    public void HealthDown() // 체력
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
         

        }

        else 
        {

            UIhealth[0].color = new Color(1, 0, 0, 0.4f);


            //Player Die Effect
            girl.OnDie();
            
                audioSource.clip = audioDie;
                audioSource.Play();
            

            UIRestartButton.SetActive(true);
        




        }
       
    }

  



    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        

            if (health > 1) //health > 1이면 원래위치로 
            {
                collision.attachedRigidbody.velocity = Vector2.zero;
                collision.transform.position = new Vector3(-105, -15, -2);
            }

            HealthDown(); //체력 감소
        }
    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");

        player.transform.position = new Vector3(x, y, 0);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}

 

