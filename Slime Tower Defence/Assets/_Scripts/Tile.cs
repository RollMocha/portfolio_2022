using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isRoad;
    public bool isSlime = false; // �������� �ִ��� Ȯ��
    public Vector3 towerPosition; // �������� ��ġ�� ��ġ

    // Start is called before the first frame update
    void Start()
    {
        Vector3 tilePosition = transform.position;
        towerPosition = new Vector3(tilePosition.x, tilePosition.y + 3f,
            tilePosition.z);
    }
}
