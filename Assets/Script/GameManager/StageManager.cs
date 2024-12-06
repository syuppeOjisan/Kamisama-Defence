using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject character1Prefab;
    public GameObject character2Prefab;

    public Transform playerSpawnPoint; // プレイヤースポーンポイント
    private GameObject spawnedCharacter;

    public float initialOfferingPoints = 100f;
    public float offeringPoints;
    public TMP_Text offeringPointsText;

    public TMP_Text waveInfoText; // 新しいUIテキスト (ウェーブ情報)
    public TMP_Text faithPointsText; // 信仰ポイントUI (ステージ内)
    public List<FiniteEnemySpawner> enemySpawners; // 複数のスポナー
    public List<int> totalEnemiesPerWave; // 各ウェーブの合計敵数
    private int enemiesDefeated = 0;
    private int currentWave = 0;
    private int stageFaithPoints = 0; // ステージ内で取得した信仰ポイント

    public AudioClip waveCompleteSound;
    private AudioSource audioSource;

    void Start()
    {
        offeringPoints = initialOfferingPoints;
        UpdateOfferingPointsUI();
        audioSource = gameObject.AddComponent<AudioSource>();

        SpawnPlayerCharacter();
        SetupCameraFollow();
        UpdateWaveInfoUI(); // 初期UIを更新
        stageFaithPoints = 0;
        UpdateFaithPointsUI();

    }

    void SpawnPlayerCharacter()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");

        if (playerSpawnPoint == null)
        {
            Debug.LogError("プレイヤースポーンポイントが設定されていません！");
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
            offeringPointsText.text = "お賽銭: " + offeringPoints.ToString();
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
            faithPointsText.text = $"信仰ポイント: {stageFaithPoints}";
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        Debug.Log("敵を倒しました。現在の撃破数: " + enemiesDefeated);

        UpdateWaveInfoUI(); // 敵撃破数を更新

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
            UpdateWaveInfoUI(); // 新しいウェーブの情報を更新
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
        Debug.Log("ステージクリア！");
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