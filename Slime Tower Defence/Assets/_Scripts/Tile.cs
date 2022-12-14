using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isRoad;
    public bool isSlime = false; // 슬라임이 있는지 확인
    public Vector3 towerPosition; // 슬라임을 배치할 위치

    // Start is called before the first frame update
    void Start()
    {
        Vector3 tilePosition = transform.position;
        towerPosition = new Vector3(tilePosition.x, tilePosition.y + 3f,
            tilePosition.z);
    }
}
