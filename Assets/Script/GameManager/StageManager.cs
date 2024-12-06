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

    public TMP_Text waveInfoText; // �V����UI�e�L�X�g (�E�F�[�u���)
    public TMP_Text faithPointsText; // �M�|�C���gUI (�X�e�[�W��)
    public List<FiniteEnemySpawner> enemySpawners; // �����̃X�|�i�[
    public List<int> totalEnemiesPerWave; // �e�E�F�[�u�̍��v�G��
    private int enemiesDefeated = 0;
    private int currentWave = 0;
    private int stageFaithPoints = 0; // �X�e�[�W���Ŏ擾�����M�|�C���g

    public AudioClip waveCompleteSound;
    private AudioSource audioSource;

    void Start()
    {
        offeringPoints = initialOfferingPoints;
        UpdateOfferingPointsUI();
        audioSource = gameObject.AddComponent<AudioSource>();

        SpawnPlayerCharacter();
        SetupCameraFollow();
        UpdateWaveInfoUI(); // ����UI���X�V
        stageFaithPoints = 0;
        UpdateFaithPointsUI();

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
            offeringPointsText.text = "���ΑK: " + offeringPoints.ToString();
        }
    }

    public void AddFaithPoints(int amount)
    {
        stageFaithPoints += amount;
        UpdateFaithPointsUI();
    }

    private void UpdateFaithPointsUI()
    {
        if (faithPointsText != null)
        {
            faithPointsText.text = $"�M�|�C���g: {stageFaithPoints}";
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        Debug.Log("�G��|���܂����B���݂̌��j��: " + enemiesDefeated);

        UpdateWaveInfoUI(); // �G���j�����X�V

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
            UpdateWaveInfoUI(); // �V�����E�F�[�u�̏����X�V
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
        FaithPointManager.Instance.AddFaithPoints(stageFaithPoints);
        SceneManager.LoadScene("StageClearScene");
    }

    private void UpdateWaveInfoUI()
    {
        if (waveInfoText != null)
        {
            waveInfoText.text = $"Wave {currentWave + 1} : {enemiesDefeated}/{totalEnemiesPerWave[currentWave]}";
        }
    }

    void GameOver()
    {
        stageFaithPoints = 0;
        FaithPointManager.Instance.ResetFaithPoints();
        SceneManager.LoadScene("GameOverScene");
    }
}