using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUIManager : MonoBehaviour
{
    Enemy_1 target; // hp Bar 연동 몬스터 정보

    Canvas hpCanvas; // 소환될 캔버스 정보

    RectTransform rectParent; // 캔버스의 위치 값
    RectTransform rectHp; // hp Bar의 캔버스 상의 위치 값
    Camera mainCamera; // 메인 카메라
    Slider slider;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        hpCanvas = GetComponentInParent<Canvas>();
        rectParent = hpCanvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
        slider = this.GetComponent<Slider>();
        mainCamera = hpCanvas.worldCamera;

        offset = new Vector3(0, 5f, -1.5f); // 몬스터 기준 hp bar 배치 위치
    }

    private void LateUpdate() // 업데이트 이후에 실행
    {
        if (target == null) // hp bar가 관리하는 몬스터가 없는 경우 제거
        {
            Destroy(this.gameObject);
            return;
        }

        // 맵 상의 몬스터 좌표를 스크린 좌표로 이동
        Vector3 monsterPositionInScreen = Camera.main.WorldToScreenPoint(
            target.gameObject.transform.position + offset);

        /*
        if (monsterPositionInScreen.z < 0.0f)
        {
            monsterPositionInScreen *= -1.0f;
        }
        */

        // 스크린 좌표를 다시 체력바 UI 캔버스 좌표로 변환
        Vector2 localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent,
            monsterPositionInScreen, mainCamera, out localPos);

        rectHp.localPosition = localPos; // 체력바 위치 조정
    }

    // hp bar 몬스터 조정
    public void HpBarSetInMonster(Enemy_1 enemy)
    {
        target = enemy;
        //slider.maxValue = (float)enemy.maxHp;
    }
}
