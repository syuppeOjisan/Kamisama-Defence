using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // ポーズ画面UI

    private bool isPaused = false;

    void Update()
    {
        // Escapeキーでポーズ切り替え
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // ゲームを一時停止
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // ゲームの時間を停止
        pauseMenuUI.SetActive(true); // ポーズメニューを表示
    }

    // ゲームを再開
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // ゲームの時間を再開
        pauseMenuUI.SetActive(false); // ポーズメニューを非表示
    }

    // リトライボタンが押されたとき
    public void RetryStage()
    {
        Time.timeScale = 1; // 時間を再開してからシーンを再ロード
        int lastPlayedStage = PlayerPrefs.GetInt("SelectedStage", 1);
        SceneManager.LoadScene($"Stage{lastPlayedStage}Scene");
    }

    // ステージセレクトに戻るボタンが押されたとき
    public void ReturnToStageSelect()
    {
        Time.timeScale = 1; // 時間を再開してからステージセレクトに戻る
        SceneManager.LoadScene("StageSelectScene");
    }
}