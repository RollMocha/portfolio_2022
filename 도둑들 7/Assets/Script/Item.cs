using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Score, Hp, Ring
}

public class Item : MonoBehaviour
{

    public ItemType itemType;

    public int extraScore;

    public int extraHp;


}

