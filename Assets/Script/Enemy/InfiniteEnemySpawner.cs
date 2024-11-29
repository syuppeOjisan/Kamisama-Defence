using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // スポーンする敵のプレハブ
    public float spawnInterval = 5f; // 敵がスポーンする間隔（秒）
    public Transform[] spawnPoints; // 敵がスポーンする位置のリスト

    void Start()
    {
        // スポーンを繰り返すコルーチンを開始
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // 一定間隔で待つ
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length > 0)
        {
            // ランダムなスポーンポイントを選択
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // 敵をスポーン
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("スポーンポイントが設定されていません。");
        }
    }
}