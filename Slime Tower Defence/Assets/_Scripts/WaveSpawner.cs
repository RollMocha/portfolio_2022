using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour//적 생성하는 코드
{

    public static WaveSpawner waveSpawner; // 싱글톤 패턴

    //public Transform[] enemyPrefab_1;//Monster_1Prefab의 Transforms
    public Transform spawnPoint_1; //MonsterSpawnWaypoint(적 스폰 GameObject)의 Transform
    public Transform spawnPoint_2; //MonsterSpawnWaypoint(적 스폰 GameObject)의 Transform
    public float countdown = 2f;//시간
    public float timeBetweenWaves = 10f; //적들이 생성되는 시간 간격
    public int point;

    [SerializeField]
    private Transform[] wayPoints;

    private Wave currentWave; // 현재 웨이브 정보
    private WaveSystem waveSystem;
    private List<Enemy_1> enemyList_1;
    public int currentWaveIndex;

    public List<Enemy_1> EnemyList_1 => enemyList_1;

    private void Awake()
    {
        enemyList_1 = new List<Enemy_1>();
        waveSpawner = this; // 싱글톤 패턴
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave; // 매개변수로 받아온 웨이브 정보 저장
        StartCoroutine("SpawnWave"); // 현재 웨이브 시작
    }

    private IEnumerator SpawnWave()// waveIndex 만큼  0.3초마다 SpawnEnemy_1()과 SpawnEnemy_2를 호출
    {
        int spawnEnemyCount = 0; // 현재 웨이브에서 생성한 적 숫자

        //현재 웨이브에서 생성되어야 하는 적의 숫자만큼 적을 생성하고 코루틴 종료
        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            if (HPManager.CurrentHP > 0)
            {
                int enemy_random = UnityEngine.Random.Range(0, currentWave.enemyPrefabs.Length);
                point = UnityEngine.Random.Range(0, 2);//웨이브 경로 렌덤 설정
                if (currentWave.enemyPrefabnumbers[enemy_random] != 0)
                {
                    if (point == 0)
                    {
                        GameObject clone_1 = Instantiate(currentWave.enemyPrefabs[enemy_random], spawnPoint_1.position, spawnPoint_1.rotation);
                        Enemy_1 enemy_1 = clone_1.GetComponent<Enemy_1>();
                        enemy_1.StartWayPoint(point);
                        enemyList_1.Add(enemy_1);
                    }
                    if (point == 1)
                    {
                        GameObject clone_2 = Instantiate(currentWave.enemyPrefabs[enemy_random], spawnPoint_2.position, spawnPoint_2.rotation);
                        Enemy_1 enemy_1 = clone_2.GetComponent<Enemy_1>();
                        enemy_1.StartWayPoint(point);
                        enemyList_1.Add(enemy_1);
                    }
                    currentWave.enemyPrefabnumbers[enemy_random] -= 1;
                    spawnEnemyCount++; // 현재 웨이브에서 생성한 적의 숫자 + 1
                }

                yield return new WaitForSeconds(currentWave.spawnTime);//spawnTime 시간 동안 기다리는 함수
            }
        }
    }
    
    // 도착 지점에 도착 시 적 제거 
    public void DestroyEnemy(Enemy_1 enemy_1)
    {
        enemyList_1.Remove(enemy_1);
        Destroy(enemy_1.gameObject);
    }
}
