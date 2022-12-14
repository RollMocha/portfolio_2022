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
            // 왼쪽으로 회전
            transform.RotateAround(target.position, Vector3.up, 5.0f);

            offset = transform.position - target.position;
            offset.Normalize();
        }
        if (Input.GetKey("d"))
        {
            //오른쪽으로 회전
            transform.RotateAround(target.position, Vector3.up, -5.0f);

            offset = transform.position - target.position;
            offset.Normalize();
        }

        // 마우스 휠로 줌 인아웃
        currentZoom -= Input.GetAxis("Mouse ScrollWheel");
        // 줌 최소 및 최대 설정 
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

    }

    void LateUpdate()
    {
        // 변경된 카메라 위치 적용
        transform.position = target.position + offset * currentZoom;
        transform.LookAt(target);
    }

}
