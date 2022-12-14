using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragEvent : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject dragObject; // �巡�׿� �� ������Ʈ
    // �巡�׿� �� ������Ʈ�� ��� GameScene_UI�� �̸� ��ġ�� �� SetActive�� false�� ������ ��

    public Slime slimePrefab; // ��ġ�� ������

    public bool isDragging = false; // �巡�� ������ Ȯ��
    public bool isFruit = false; // �������� Ȯ��
    public bool isPromote = false; // ���� ���������� Ȯ��

    // �巡�� ���� �� ���
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragObject == null)
        {
            return;
        }

        dragObject.SetActive(true);

        isDragging = true;
    }

    // �巡�� �� ������ ������ ȣ��
    public void OnDrag(PointerEventData eventData)
    {

        // �巡�� ���� �� �ű�� �̹����� ���콺�� ���󰡵��� ����
        if (isDragging)
        {
            dragObject.transform.position = eventData.position;

        }
    }

    // �巡�� ���� �� ����
    public void OnEndDrag(PointerEventData eventData)
    {

        // �巡�� ������ Ȯ��
        if (!isDragging)
        {
            Debug.Log("OnEndDrag : is not Dragging");
            return;
        }

        // �巡�� ������Ʈ�� �����Ǿ� �ִ��� Ȯ��
        if (dragObject == null)
        {
            Debug.Log("OnEndDrag : is not Dragging");
            return;
        }

        dragObject.SetActive(false); // �̸� ��ġ�� dragObject Ȱ��ȭ
        isDragging = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ī�޶󿡼� ������ ��

        RaycastHit[] raycastHits = Physics.RaycastAll(ray); // ī�޶󿡼� ������ ���� �ε��� ������Ʈ ��� ���

        foreach (RaycastHit hit in raycastHits) // �ε��� ��ü��� �ݺ��� ����
        {
            if (hit.collider.tag != "Tile") // �ε��� ��ü�� Ÿ������ Ȯ��
            {
                // Ÿ���� �ƴҰ�� ���� RaycastHit Ȯ��
                continue;
            }

            // ��ġ�� ������ ������ �����Ǿ� �ִ��� Ȯ��
            if (slimePrefab == null)
            {
                Debug.Log("Slime Prefab is not setting");
            }

            // raycast�� �΋H�� ������Ʈ�� Tile ���� ������
            Tile targetTile = hit.collider.gameObject.GetComponent<Tile>();

            // ������ ���� �� Ȯ��
            if (isFruit)
            {
                // ���� �������� ���
                SlimeSpawnManager.slimeSpawnManager.ChangeDefultSlime(targetTile, slimePrefab);
            }
            else if (isPromote)
            {
                // ���� �������� ���
                SlimeSpawnManager.slimeSpawnManager.ChangeFruitSlime(targetTile, slimePrefab);
            }
            else
            {
                // �⺻ �������� ���
                SlimeSpawnManager.slimeSpawnManager.SpawnDefultSlime(targetTile, slimePrefab);
            }

            return;
        }
    }
}