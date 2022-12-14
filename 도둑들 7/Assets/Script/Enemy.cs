using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float speed;
    public int hp;
    public int atk;
    public int def;
    public int currentHp;

    public GameObject healthBarBackground;
    public Image healthBarFilled;

    bool isLeft = true;
    // Use this for initialization
    void Start()
    {
        currentHp = hp;
        //healthBarFilled.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "endpoint")
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
        }
    }

    public int Hit(int _playerAtk)
    {

        int playerAtk = _playerAtk;
        int dmg;
        if (def >= playerAtk)
            dmg = 1;
        else
            dmg = playerAtk - def;

        currentHp -= dmg;

        if (currentHp <= 0)
        {
            Destroy(this.gameObject);
        }

        healthBarFilled.fillAmount = (float)currentHp / hp;
        healthBarBackground.SetActive(true);

        return dmg;
    }


}