using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    private float spawntime;

    void Start()
    {

    }

    void Update()
    {
        spawntime += Time.deltaTime;//ī��Ʈ �ٿ�

        DestroyFruits();
    }

    private void DestroyFruits()
    {
        if (spawntime >= 1)//1�� �Ѱ� �ȴٸ� �����
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
