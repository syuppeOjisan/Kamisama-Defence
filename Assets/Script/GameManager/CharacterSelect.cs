using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    // キャラクター1の選択
    public void SelectCharacter1()
    {
        PlayerPrefs.SetInt("SelectedCharacter", 1); // キャラクター1を保存
        LoadSelectedStage(); // 選択したステージをロード
    }

    // キャラクター2の選択
    public void SelectCharacter2()
    {
        PlayerPrefs.SetInt("SelectedCharacter", 2); // キャラクター2を保存
        LoadSelectedStage(); // 選択したステージをロード
    }

    // 選択したステージをロードする
    private void LoadSelectedStage()
    {
        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1); // デフォルトはステージ1

        // ステージ番号に応じてシーンをロード
        if (selectedStage == 1)
        {
            SceneManager.LoadScene("Stage1Scene");
        }

        if (selectedStage == 2)
        {
            SceneManager.LoadScene("Stage2Scene");
        }

        if (selectedStage == 3)
        {
            SceneManager.LoadScene("Stage3Scene");
        }
        // 他のステージを追加する場合はここに追加
    }

    // ステージセレクトシーンに戻るメソッド
    public void ReturnToStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}