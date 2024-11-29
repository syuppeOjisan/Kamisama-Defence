using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteEnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // 複数の敵プレハブ
    public Transform[] spawnPoints; // スポーンポイントのリスト
    public List<int> enemiesPerWave; // 各ウェーブの敵の数
    public List<float> spawnProbabilities; // 秒数ごとのスポーン確率
    public float waveInterval = 1f; // 各敵が湧く間隔

    private int currentWave = 0; // 現在のウェーブインデックス
    private int enemiesSpawnedInWave = 0; // 現在のウェーブでスポーンした敵の数
    private float elapsedTime = 0f; // 経過時間のカウンター
    private int currentSecond = 0; // 秒ごとの抽選用カウンター

    public bool IsWaveComplete => enemiesSpawnedInWave >= enemiesPerWave[currentWave]; // ウェーブ完了の確認

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentWave < enemiesPerWave.Count)
        {
            if (!IsWaveComplete)
            {
                yield return new WaitForSeconds(1f);
                elapsedTime += 1f;
                currentSecond = Mathf.FloorToInt(elapsedTime);

                if (currentSecond < spawnProbabilities.Count)
                {
                    float probability = spawnProbabilities[currentSecond];
                    if (Random.value <= probability / 100f) // 抽選でスポーンが決定
                    {
                        SpawnEnemy();
                        enemiesSpawnedInWave++;
                        ResetSpawnTimer();
                    }
                }
                else if (currentSecond >= spawnProbabilities.Count)
                {
                    SpawnEnemy();
                    enemiesSpawnedInWave++;
                    ResetSpawnTimer();
                }
            }
            yield return null;
        }
    }

    public void StartNextWave()
    {
        if (currentWave < enemiesPerWave.Count - 1)
        {
            currentWave++;
            enemiesSpawnedInWave = 0;
            elapsedTime = 0f;
            currentSecond = 0;
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length > 0 && enemyPrefabs.Count > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("スポーンポイントまたは敵プレハブが設定されていません。");
        }
    }

    void ResetSpawnTimer()
    {
        elapsedTime = 0f;
        currentSecond = 0;
    }
}