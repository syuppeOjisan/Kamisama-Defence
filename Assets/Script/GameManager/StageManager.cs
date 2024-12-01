using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject character1Prefab;
    public GameObject character2Prefab;

    public Transform playerSpawnPoint; // �v���C���[�X�|�[���|�C���g

    private GameObject spawnedCharacter;

    public float initialOfferingPoints = 100f;
    public float offeringPoints;
    public TMP_Text offeringPointsText;

    public List<FiniteEnemySpawner> enemySpawners; // �����̃X�|�i�[
    public List<int> totalEnemiesPerWave; // �e�E�F�[�u�̍��v�G��
    private int enemiesDefeated = 0;
    private int currentWave = 0;

    public AudioClip waveCompleteSound;
    private AudioSource audioSource;

    void Start()
    {
        offeringPoints = initialOfferingPoints;
        UpdateOfferingPointsUI();
        audioSource = gameObject.AddComponent<AudioSource>();

        SpawnPlayerCharacter();
        SetupCameraFollow();
    }

    void SpawnPlayerCharacter()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");

        if (playerSpawnPoint == null)
        {
            Debug.LogError("�v���C���[�X�|�[���|�C���g���ݒ肳��Ă��܂���I");
            return;
        }

        Vector3 spawnPosition = playerSpawnPoint.position;

        if (selectedCharacter == 1)
        {
            spawnedCharacter = Instantiate(character1Prefab, spawnPosition, Quaternion.identity);
        }
        else if (selectedCharacter == 2)
        {
            spawnedCharacter = Instantiate(character2Prefab, spawnPosition, Quaternion.identity);
        }
    }

    void SetupCameraFollow()
    {
        CameraFollow mainCameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (mainCameraFollow != null)
        {
            mainCameraFollow.playerTransform = spawnedCharacter.transform;
        }

        MiniMapCameraFollow miniMapCameraFollow = FindObjectOfType<MiniMapCameraFollow>();
        if (miniMapCameraFollow != null)
        {
            miniMapCameraFollow.player = spawnedCharacter.transform;
        }
    }

    public void AddOfferingPoints(float amount)
    {
        offeringPoints += amount;
        UpdateOfferingPointsUI();
    }

    public bool DeductOfferingPoints(float amount)
    {
        if (offeringPoints >= amount)
        {
            offeringPoints -= amount;
            UpdateOfferingPointsUI();
            return true;
        }
        return false;
    }

    private void UpdateOfferingPointsUI()
    {
        if (offeringPointsText != null)
        {
            offeringPointsText.text = "Offering Points: " + offeringPoints.ToString();
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        Debug.Log("�G��|���܂����B���݂̌��j��: " + enemiesDefeated);

        if (enemiesDefeated >= totalEnemiesPerWave[currentWave])
        {
            NextWave();
        }
    }

    void NextWave()
    {
        if (currentWave < totalEnemiesPerWave.Count - 1)
        {
            currentWave++;
            enemiesDefeated = 0;
            foreach (FiniteEnemySpawner spawner in enemySpawners)
            {
                spawner.StartNextWave();
            }
            PlayWaveCompleteSound();
        }
        else
        {
            StageClear();
        }
    }

    void PlayWaveCompleteSound()
    {
        if (waveCompleteSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(waveCompleteSound);
        }
    }

    void StageClear()
    {
        Debug.Log("�X�e�[�W�N���A�I");
        SceneManager.LoadScene("StageClearScene");
    }
}