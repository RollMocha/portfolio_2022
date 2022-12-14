using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour//�� �����ϴ� �ڵ�
{

    public static WaveSpawner waveSpawner; // �̱��� ����

    //public Transform[] enemyPrefab_1;//Monster_1Prefab�� Transforms
    public Transform spawnPoint_1; //MonsterSpawnWaypoint(�� ���� GameObject)�� Transform
    public Transform spawnPoint_2; //MonsterSpawnWaypoint(�� ���� GameObject)�� Transform
    public float countdown = 2f;//�ð�
    public float timeBetweenWaves = 10f; //������ �����Ǵ� �ð� ����
    public int point;

    [SerializeField]
    private Transform[] wayPoints;

    private Wave currentWave; // ���� ���̺� ����
    private WaveSystem waveSystem;
    private List<Enemy_1> enemyList_1;
    public int currentWaveIndex;

    public List<Enemy_1> EnemyList_1 => enemyList_1;

    private void Awake()
    {
        enemyList_1 = new List<Enemy_1>();
        waveSpawner = this; // �̱��� ����
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave; // �Ű������� �޾ƿ� ���̺� ���� ����
        StartCoroutine("SpawnWave"); // ���� ���̺� ����
    }

    private IEnumerator SpawnWave()// waveIndex ��ŭ  0.3�ʸ��� SpawnEnemy_1()�� SpawnEnemy_2�� ȣ��
    {
        int spawnEnemyCount = 0; // ���� ���̺꿡�� ������ �� ����

        //���� ���̺꿡�� �����Ǿ�� �ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            if (HPManager.CurrentHP > 0)
            {
                int enemy_random = UnityEngine.Random.Range(0, currentWave.enemyPrefabs.Length);
                point = UnityEngine.Random.Range(0, 2);//���̺� ��� ���� ����
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
                    spawnEnemyCount++; // ���� ���̺꿡�� ������ ���� ���� + 1
                }

                yield return new WaitForSeconds(currentWave.spawnTime);//spawnTime �ð� ���� ��ٸ��� �Լ�
            }
        }
    }
    
    // ���� ������ ���� �� �� ���� 
    public void DestroyEnemy(Enemy_1 enemy_1)
    {
        enemyList_1.Remove(enemy_1);
        Destroy(enemy_1.gameObject);
    }
}
