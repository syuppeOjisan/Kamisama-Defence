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
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 1); // デフォルトはキャラクター1

        // ステージデータとゲームデータをリセット
        ResetStageData();

        // 選択したキャラクターとステージを保持したまま再スタート
        if (lastPlayedStage == 1)
        {
            SceneManager.LoadScene("Stage1Scene");
        }
        // 他のステージを追加する場合はここに追加
    }

    // ステージデータをリセットするメソッド
    private void ResetStageData()
    {
        // お賽銭ポイントのリセット
        PlayerPrefs.SetFloat("OfferingPoints", 0);

        // ここで他のリセットが必要なデータをリセットする
        // 例えば、ユニット配置や敵・参拝客の情報をリセットする場合は、シーン再読み込み時に初期化されるようにする
    }

    // ステージセレクトシーンに戻るボタン
    public void ReturnToStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}