using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // 現在のシーンを再読み込み（リスタート）
    public void RestartThisScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ステージセレクトに戻る
    public void GoBackStageSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("StageSelectScene");
    }
}
