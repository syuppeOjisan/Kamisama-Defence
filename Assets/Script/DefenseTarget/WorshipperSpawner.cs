using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorshipperSpawner : MonoBehaviour
{
    public List<GameObject> worshipperPrefabs; // 複数の参拝客プレハブを設定可能に
    public float spawnInterval = 5f; // 参拝客がスポーンする間隔（秒）
    public Transform[] spawnPoints; // 参拝客がスポーンする位置のリスト

    void Start()
    {
        StartCoroutine(SpawnWorshippers());
    }

    IEnumerator SpawnWorshippers()
    {
        while (true)
        {
            SpawnWorshipper();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnWorshipper()
    {
        if (spawnPoints.Length > 0 && worshipperPrefabs.Count > 0)
        {
            // ランダムなスポーンポイントと参拝客プレハブを選択
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject worshipperPrefab = worshipperPrefabs[Random.Range(0, worshipperPrefabs.Count)];

            // 参拝客をスポーン
            GameObject worshipperObject = Instantiate(worshipperPrefab, spawnPoint.position, spawnPoint.rotation);

            // スポーンした参拝客にスポーンポイントを設定
            Worshipper worshipper = worshipperObject.GetComponent<Worshipper>();
            if (worshipper != null)
            {
                worshipper.SetSpawnPoint(spawnPoint);
            }
        }
        else
        {
            Debug.LogWarning("スポーンポイントまたは参拝客プレハブが設定されていません。");
        }
    }
}