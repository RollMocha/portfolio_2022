using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUIManager : MonoBehaviour
{
    Enemy_1 target; // hp Bar ���� ���� ����

    Canvas hpCanvas; // ��ȯ�� ĵ���� ����

    RectTransform rectParent; // ĵ������ ��ġ ��
    RectTransform rectHp; // hp Bar�� ĵ���� ���� ��ġ ��
    Camera mainCamera; // ���� ī�޶�
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

        offset = new Vector3(0, 5f, -1.5f); // ���� ���� hp bar ��ġ ��ġ
    }

    private void LateUpdate() // ������Ʈ ���Ŀ� ����
    {
        if (target == null) // hp bar�� �����ϴ� ���Ͱ� ���� ��� ����
        {
            Destroy(this.gameObject);
            return;
        }

        // �� ���� ���� ��ǥ�� ��ũ�� ��ǥ�� �̵�
        Vector3 monsterPositionInScreen = Camera.main.WorldToScreenPoint(
            target.gameObject.transform.position + offset);

        /*
        if (monsterPositionInScreen.z < 0.0f)
        {
            monsterPositionInScreen *= -1.0f;
        }
        */

        // ��ũ�� ��ǥ�� �ٽ� ü�¹� UI ĵ���� ��ǥ�� ��ȯ
        Vector2 localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent,
            monsterPositionInScreen, mainCamera, out localPos);

        rectHp.localPosition = localPos; // ü�¹� ��ġ ����
    }

    // hp bar ���� ����
    public void HpBarSetInMonster(Enemy_1 enemy)
    {
        target = enemy;
        //slider.maxValue = (float)enemy.maxHp;
    }
}
