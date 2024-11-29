using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // �X�|�[������G�̃v���n�u
    public float spawnInterval = 5f; // �G���X�|�[������Ԋu�i�b�j
    public Transform[] spawnPoints; // �G���X�|�[������ʒu�̃��X�g

    void Start()
    {
        // �X�|�[�����J��Ԃ��R���[�`�����J�n
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // ���Ԋu�ő҂�
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length > 0)
        {
            // �����_���ȃX�|�[���|�C���g��I��
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // �G���X�|�[��
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("�X�|�[���|�C���g���ݒ肳��Ă��܂���B");
        }
    }
}