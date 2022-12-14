using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(12.45f, 34.5f, -50.85f);

  public float currentZoom = 7.0f;

    float minZoom = 5.0f;
    float maxZoom = 10.0f;

    void Update()
    {

        if (Input.GetKey("a"))
        {
            // �������� ȸ��
            transform.RotateAround(target.position, Vector3.up, 5.0f);

            offset = transform.position - target.position;
            offset.Normalize();
        }
        if (Input.GetKey("d"))
        {
            //���������� ȸ��
            transform.RotateAround(target.position, Vector3.up, -5.0f);

            offset = transform.position - target.position;
            offset.Normalize();
        }

        // ���콺 �ٷ� �� �ξƿ�
        currentZoom -= Input.GetAxis("Mouse ScrollWheel");
        // �� �ּ� �� �ִ� ���� 
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

    }

    void LateUpdate()
    {
        // ����� ī�޶� ��ġ ����
        transform.position = target.position + offset * currentZoom;
        transform.LookAt(target);
    }

}
