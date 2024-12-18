using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // リトライボタンが押されたときに呼び出すメソッド
    public void RetryStage()
    {
        // 最後にプレイしていたステージ番号を取得
        int lastPlayedStage = PlayerPrefs.GetInt("SelectedStage", 1); // デフォルトはステージ1

        // ステージデータをリセット
        ResetStageData();

        // ステージ番号に基づいて正しいシーンをロード
        string stageSceneName = $"Stage{lastPlayedStage}Scene";
        if (Application.CanStreamedLevelBeLoaded(stageSceneName)) // シーンが存在するか確認
        {
            SceneManager.LoadScene(stageSceneName);
        }
        else
        {
            Debug.LogError($"指定されたステージシーンが見つかりません: {stageSceneName}");
        }
    }

    // ステージデータをリセットするメソッド
    private void ResetStageData()
    {
        // お賽銭ポイントのリセット
        PlayerPrefs.SetFloat("OfferingPoints", 0);

        // 他のリセット処理を追加可能
        // 例: リセットが必要なゲーム状態やカスタムデータ
    }

    // ステージセレクトシーンに戻るボタン
    public void ReturnToStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}