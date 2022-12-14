using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragEvent : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject dragObject; // 드래그에 쓸 오브젝트
    // 드래그에 쓸 오브젝트의 경우 GameScene_UI에 미리 배치한 뒤 SetActive를 false로 설정할 것

    public Slime slimePrefab; // 배치할 슬라임

    public bool isDragging = false; // 드래그 중인지 확인
    public bool isFruit = false; // 열매인지 확인
    public bool isPromote = false; // 상위 슬라임인지 확인

    // 드래그 시작 시 사용
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragObject == null)
        {
            return;
        }

        dragObject.SetActive(true);

        isDragging = true;
    }

    // 드래그 중 프레임 단위로 호출
    public void OnDrag(PointerEventData eventData)
    {

        // 드래그 중일 때 옮기는 이미지를 마우스에 따라가도록 조정
        if (isDragging)
        {
            dragObject.transform.position = eventData.position;

        }
    }

    // 드래그 끝날 때 실행
    public void OnEndDrag(PointerEventData eventData)
    {

        // 드래그 중인지 확인
        if (!isDragging)
        {
            Debug.Log("OnEndDrag : is not Dragging");
            return;
        }

        // 드래그 오브젝트가 설정되어 있는지 확인
        if (dragObject == null)
        {
            Debug.Log("OnEndDrag : is not Dragging");
            return;
        }

        dragObject.SetActive(false); // 미리 배치한 dragObject 활성화
        isDragging = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라에서 나가는 선

        RaycastHit[] raycastHits = Physics.RaycastAll(ray); // 카메라에서 나가는 선에 부딪힌 오브젝트 모두 계산

        foreach (RaycastHit hit in raycastHits) // 부딪힌 물체들로 반복문 실행
        {
            if (hit.collider.tag != "Tile") // 부딪힌 물체가 타일인지 확인
            {
                // 타일이 아닐경우 다음 RaycastHit 확인
                continue;
            }

            // 배치할 슬라임 정보가 설정되어 있는지 확인
            if (slimePrefab == null)
            {
                Debug.Log("Slime Prefab is not setting");
            }

            // raycast에 부딫힌 오브젝트의 Tile 정보 가져옴
            Tile targetTile = hit.collider.gameObject.GetComponent<Tile>();

            // 슬라임 생성 및 확인
            if (isFruit)
            {
                // 열매 슬라임의 경우
                SlimeSpawnManager.slimeSpawnManager.ChangeDefultSlime(targetTile, slimePrefab);
            }
            else if (isPromote)
            {
                // 상위 슬라임의 경우
                SlimeSpawnManager.slimeSpawnManager.ChangeFruitSlime(targetTile, slimePrefab);
            }
            else
            {
                // 기본 슬라임의 경우
                SlimeSpawnManager.slimeSpawnManager.SpawnDefultSlime(targetTile, slimePrefab);
            }

            return;
        }
    }
}