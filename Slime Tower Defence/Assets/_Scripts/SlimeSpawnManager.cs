using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상위 슬라임 소환을 위해 맵에 배치된 슬라임 정보를 관리하는 매니저 클래스
public class SlimeSpawnManager : MonoBehaviour
{
    Dictionary<Tile, Slime> slimeAndTile; // 맵상에 있는 슬라임과 슬라임이 배치된 타일
    public int maxSlimeCount; // 최대 슬라임 배치 수

    static public SlimeSpawnManager slimeSpawnManager; // 싱글톤 패턴
    private RedFruitUI redFruitUI;//빨간 열매 관련 UI
    private YellowFruitUI yellowFruitUI;//노란 열매 관련 UI
    private BlueFruitUI blueFruitUI;//파란 열매 관련 UI

    private void Awake()
    {
        slimeSpawnManager = this; // 싱글톤 패턴
    }

    // Start is called before the first frame update
    void Start()
    {
        slimeAndTile = new Dictionary<Tile, Slime>();
        redFruitUI = GameObject.Find("FireFruit_UI").GetComponent<RedFruitUI>();
        yellowFruitUI = GameObject.Find("ThunderFruit_UI").GetComponent<YellowFruitUI>();
        blueFruitUI = GameObject.Find("IceFruit_UI").GetComponent<BlueFruitUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 배치할 상위 슬라임에 필요한 슬라임 구분 
    SlimeState CheckMaterialSlime_1(SlimeState promoteSlimeState)
    {
        switch (promoteSlimeState) // 소환할 슬라임에 맞는 함수 실행
        {
            case SlimeState.VINE: // 넝쿨 슬라임 : 얼음 슬라임과 번개 슬라임
                return SlimeState.ICE;
            case SlimeState.WATER: // 물 슬라임 : 불 슬라임과 얼음 슬라임
                return SlimeState.ICE;
            case SlimeState.WIND: // 바람 슬라임 : 불 슬라임과 번개 슬라임
                return SlimeState.FIRE;
            default: // 정보가 없는 상위 슬라임이 들어올 경우
                Debug.Log("no information from PromoreSime");
                return SlimeState.DEFAULT;
        }
    }

    // 배치할 상위 슬라임에 필요한 슬라임 구분 
    SlimeState CheckMaterialSlime_2(SlimeState promoteSlimeState)
    {
        switch (promoteSlimeState) // 소환할 슬라임에 맞는 함수 실행
        {
            case SlimeState.VINE: // 넝쿨 슬라임 : 얼음 슬라임과 번개 슬라임
                return SlimeState.THUNDER;
            case SlimeState.WATER: // 물 슬라임 : 불 슬라임과 얼음 슬라임
                return SlimeState.FIRE;
            case SlimeState.WIND: // 바람 슬라임 : 불 슬라임과 번개 슬라임
                return SlimeState.THUNDER;
            default: // 정보가 없는 상위 슬라임이 들어올 경우
                Debug.Log("no information from PromoreSime");
                return SlimeState.DEFAULT;
        }
    }

    // 기본 슬라임 배치
    public void SpawnDefultSlime(Tile tile, Slime slimePrefab)
    {
        // 슬라임 소환 수 제한을 넘겼는지 확인
        if (slimeAndTile.Count >= maxSlimeCount)
        {
            Debug.Log("max Slime Spawn Count");
            return;
        }

        // 슬라임 정보가 있는지 확인
        if (slimePrefab == null || tile == null)
        {
            Debug.LogWarning("tile and slimePrefab error");
            return;
        }

        // 배치할 타일에 타워가 있는지 확인
        if (tile.isSlime)
        {
            Debug.LogWarning("already tile have tower");
            return;
        }

        tile.isSlime = true; // 타일의 슬라임 체크 값 변경

        // 슬라임을 생성 및 리스트에 추가
        Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
        slimeAndTile.Add(tile, attachSlime);
    }

    // 열매 슬라임 배치
    public void ChangeDefultSlime(Tile tile, Slime slimePrefab)
    {

        // 배치할 슬라임 정보가 있는지 확인
        if (slimePrefab == null || tile == null)
        {
            Debug.LogWarning("tile and slimePrefab error");
            return;
        }

        // 배치할 타일에 타워가 있는지 확인
        if (!tile.isSlime)
        {
            Debug.LogWarning("no have target slime");
            return;
        }

        // 바꿀 기본 슬라임이 있는지 확인
        Slime pastSlime;
        if (!slimeAndTile.TryGetValue(tile, out pastSlime))
        {
            Debug.Log("this tile not in List");
            return;
        }

        // 타워가 기본이 맞는지 확인
        if (pastSlime.state != SlimeState.DEFAULT)
        {
            Debug.Log("slime is not default");
            return;
        }

        if (slimePrefab.state == SlimeState.ICE && blueFruitUI.currentfruit != 0)
        {
            // 기존의 기본 슬라임을 제거
            slimeAndTile.Remove(tile);
            Destroy(pastSlime.gameObject);
            // 슬라임을 생성 및 리스트에 추가
            Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
            slimeAndTile.Add(tile, attachSlime);
            blueFruitUI.GetRemoveFruit();
        }

        if (slimePrefab.state == SlimeState.FIRE && redFruitUI.currentfruit != 0)
        {
            // 기존의 기본 슬라임을 제거
            slimeAndTile.Remove(tile);
            Destroy(pastSlime.gameObject);
            // 슬라임을 생성 및 리스트에 추가
            Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
            slimeAndTile.Add(tile, attachSlime);
            redFruitUI.GetRemoveFruit();
        }

        if (slimePrefab.state == SlimeState.THUNDER && yellowFruitUI.currentfruit != 0)
        {
            // 기존의 기본 슬라임을 제거
            slimeAndTile.Remove(tile);
            Destroy(pastSlime.gameObject);
            // 슬라임을 생성 및 리스트에 추가
            Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
            slimeAndTile.Add(tile, attachSlime);
            yellowFruitUI.GetRemoveFruit();
        }
        /*
        // 기존의 기본 슬라임을 제거
        slimeAndTile.Remove(tile);
        Destroy(pastSlime.gameObject);

        // 슬라임을 생성 및 리스트에 추가
        Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
        slimeAndTile.Add(tile, attachSlime);
        */
    }

    // 열매 슬라임 변경
    public void ChangeFruitSlime(Tile tile, Slime slimePrefab)
    {
        // 배치할 슬라임 정보가 있는지 확인
        if (slimePrefab == null || tile == null)
        {
            Debug.LogWarning("tile and slimePrefab error");
            return;
        }

        // 상위 슬라임 배치를 위해 필요한 소재 슬라임 정보들
        SlimeState materialSlimeState_1 = CheckMaterialSlime_1(slimePrefab.state);
        SlimeState materialSlimeState_2 = CheckMaterialSlime_2(slimePrefab.state);
        Slime materialSlime_1 = null; // 소재 슬라임 1
        Slime materialSlime_2 = null; // 소재 슬라임 2
        Tile materialSlimeTile_1 = null; // 소재 슬라임의 타일
        Tile materialSlimeTile_2 = null; // 소재 슬라임의 타일

        // 상위 슬라임을 배치할 때 해당 타일에 재료 슬라임이 있을 경우를 확인
        // 재료 슬라임이 있을 경우 생성 중 사라질 것이기 때문에 isSlime 체크가 필요 없다.
        bool tileHaveMaterialSlime = false;

        // 상위 슬라임 배치를 위해 필요한 재료 슬라임 찾기
        foreach (KeyValuePair<Tile, Slime> pair in slimeAndTile)
        {
            // 첫 번째 재료 슬라임 찾기
            if (pair.Value.state == materialSlimeState_1 && materialSlime_1 == null)
            {
                materialSlime_1 = pair.Value;
                materialSlimeTile_1 = pair.Key;

                if (materialSlimeTile_1 == tile)
                {
                    // 배치할 위치에 재료 슬라임이 있는 경우
                    tileHaveMaterialSlime = true;
                }
                continue;
            }

            // 두 번째 재료 슬라임 찾기
            if (pair.Value.state == materialSlimeState_2 && materialSlime_2 == null)
            {
                materialSlime_2 = pair.Value;
                materialSlimeTile_2 = pair.Key;

                if (materialSlimeTile_2 == tile)
                {
                    // 배치할 위치에 재료 슬라임이 있는 경우
                    tileHaveMaterialSlime = true;
                }
                continue;
            }
        }

        // 필요한 재료 슬라임을 찾지 못하는 경우
        if (materialSlime_1 == null || materialSlime_2 == null)
        {
            Debug.LogWarning("not have material slime");
            return;
        }

        // 배치할 위치에 재료 슬라임 이외의 슬라임이 있는 경우
        if (tile.isSlime)
        {
            if (!tileHaveMaterialSlime)
            {
                Debug.LogWarning("already tile have tower");
                return;
            }
        }

        // 기존의 재료 슬라임 제거
        materialSlimeTile_1.isSlime = false;
        slimeAndTile.Remove(materialSlimeTile_1);
        Destroy(materialSlime_1.gameObject);

        materialSlimeTile_2.isSlime = false;
        slimeAndTile.Remove(materialSlimeTile_2);
        Destroy(materialSlime_2.gameObject);

        // 슬라임을 생성 및 리스트에 추가
        Slime attachSlime = Instantiate(slimePrefab, tile.towerPosition, Quaternion.identity);
        slimeAndTile.Add(tile, attachSlime);
        tile.isSlime = true;
    }
}
