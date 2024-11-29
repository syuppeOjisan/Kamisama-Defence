using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorshipperSpawner : MonoBehaviour
{
    public List<GameObject> worshipperPrefabs; // �����̎Q�q�q�v���n�u��ݒ�\��
    public float spawnInterval = 5f; // �Q�q�q���X�|�[������Ԋu�i�b�j
    public Transform[] spawnPoints; // �Q�q�q���X�|�[������ʒu�̃��X�g

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
            // �����_���ȃX�|�[���|�C���g�ƎQ�q�q�v���n�u��I��
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject worshipperPrefab = worshipperPrefabs[Random.Range(0, worshipperPrefabs.Count)];

            // �Q�q�q���X�|�[��
            GameObject worshipperObject = Instantiate(worshipperPrefab, spawnPoint.position, spawnPoint.rotation);

            // �X�|�[�������Q�q�q�ɃX�|�[���|�C���g��ݒ�
            Worshipper worshipper = worshipperObject.GetComponent<Worshipper>();
            if (worshipper != null)
            {
                worshipper.SetSpawnPoint(spawnPoint);
            }
        }
        else
        {
            Debug.LogWarning("�X�|�[���|�C���g�܂��͎Q�q�q�v���n�u���ݒ肳��Ă��܂���B");
        }
    }
}