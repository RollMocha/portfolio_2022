using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;              // 현재 스테이지의 모든 웨이브 정보
    [SerializeField]
    private WaveSpawner waveSpawner;
    [SerializeField]
    private float countdown = 2f;
    [SerializeField]
    private float timeBetweenWaves = 20f;

    public GameObject gameclear;
    public GameObject gameclear_button;

    public Text CurrentWaveText;
    private CurrentWave currentWave;

    public int currentWaveIndex = -1; // 현재 웨이브 인덱스

    public void Start()
    {
        currentWave = GameObject.Find("CurrentWave_UI").GetComponent<CurrentWave>();
        waveSpawner = GetComponent<WaveSpawner>();
    }

    private void Update()
    {
        CurrentWaveText.text = (currentWaveIndex + 1) + " WAVE";
        if (countdown <= 0f)
        {
            EndWave();
            StartWave();
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    public void StartWave()
    {
        if (waveSpawner.EnemyList_1.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            waveSpawner.StartWave(waves[currentWaveIndex]);
            currentWave.GetAddWave();
        }
    }

    public void EndWave()
    {
        if (waveSpawner.EnemyList_1.Count == 0 && currentWaveIndex == waves.Length - 1)
        {
            gameclear.SetActive(true);
            gameclear_button.SetActive(true);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;            // 현재 웨이브 적 생성 주기
    public int maxEnemyCount;          // 현재 웨이브 적 등장 숫자
    public GameObject[] enemyPrefabs;  // 현재 웨이브 적 등장 종류
    public int[] enemyPrefabnumbers;   // 현재 웨이브 각각 적 마리 수
}