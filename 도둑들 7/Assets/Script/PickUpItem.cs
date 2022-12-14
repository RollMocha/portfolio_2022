using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
  
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();

            if (item.itemType == ItemType.Score)
            {
                //SoundManager.instance.PlaySE("Score");
                ScoreManager.extraScore += item.extraScore;
            }

            Destroy(other.gameObject);
        }
        

        if (other.transform.CompareTag("heart"))
        {
            Item item = other.GetComponent<Item>();

            /*
            if (item.itemType == ItemType.Hp)
            {
                HpManager.extraHp += item.extraHp;
            }
            */
            if (item.itemType == ItemType.Score)
            {
                //SoundManager.instance.PlaySE("Score");
                ScoreManager.extraScore += item.extraScore;
            }

            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("next stage"))
        {
            Item item = other.GetComponent<Item>();


            if (item.itemType == ItemType.Score)
            {
                //SoundManager.instance.PlaySE("Score");
                ScoreManager.extraScore += item.extraScore;
            }

            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("ring"))
        {
            Item item = other.GetComponent<Item>();


            if (item.itemType == ItemType.Score)
            {
                //SoundManager.instance.PlaySE("Score");
                ScoreManager.extraScore += item.extraScore;
            }

            Destroy(other.gameObject);
        }
    }
}

