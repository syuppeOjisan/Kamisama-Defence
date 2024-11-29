using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteEnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // �����̓G�v���n�u
    public Transform[] spawnPoints; // �X�|�[���|�C���g�̃��X�g
    public List<int> enemiesPerWave; // �e�E�F�[�u�̓G�̐�
    public List<float> spawnProbabilities; // �b�����Ƃ̃X�|�[���m��
    public float waveInterval = 1f; // �e�G���N���Ԋu

    private int currentWave = 0; // ���݂̃E�F�[�u�C���f�b�N�X
    private int enemiesSpawnedInWave = 0; // ���݂̃E�F�[�u�ŃX�|�[�������G�̐�
    private float elapsedTime = 0f; // �o�ߎ��Ԃ̃J�E���^�[
    private int currentSecond = 0; // �b���Ƃ̒��I�p�J�E���^�[

    public bool IsWaveComplete => enemiesSpawnedInWave >= enemiesPerWave[currentWave]; // �E�F�[�u�����̊m�F

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
                    if (Random.value <= probability / 100f) // ���I�ŃX�|�[��������
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
            Debug.LogWarning("�X�|�[���|�C���g�܂��͓G�v���n�u���ݒ肳��Ă��܂���B");
        }
    }

    void ResetSpawnTimer()
    {
        elapsedTime = 0f;
        currentSecond = 0;
    }
}