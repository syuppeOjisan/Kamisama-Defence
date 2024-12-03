using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    private FadeController fadeController;
    private Button[] allButtons; // シーン内のすべてのボタン

    void Start()
    {
        // FadeControllerをシーン内から探す
        fadeController = FindObjectOfType<FadeController>();

        // シーン内のすべてのボタンを取得
        allButtons = FindObjectsOfType<Button>();

        // シーン開始時にフェードインを実行
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeIn(() =>
            {
                SetButtonsInteractable(true); // フェードイン完了後にボタンを有効化
            }));
        }
    }

    // キャラクター1を選択
    public void SelectCharacter1()
    {
        SetButtonsInteractable(false); // ボタンを無効化
        PlayerPrefs.SetInt("SelectedCharacter", 1); // キャラクター1を保存

        // 選択したステージをロード（フェードアウト付き）
        LoadSelectedStage();
    }

    // キャラクター2を選択
    public void SelectCharacter2()
    {
        SetButtonsInteractable(false); // ボタンを無効化
        PlayerPrefs.SetInt("SelectedCharacter", 2); // キャラクター2を保存

        // 選択したステージをロード（フェードアウト付き）
        LoadSelectedStage();
    }

    // ステージをロードする
    private void LoadSelectedStage()
    {
        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1); // デフォルトはステージ1
        string sceneName = $"Stage{selectedStage}Scene";

        // フェードアウトしてシーン遷移
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene(sceneName));
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    // ステージセレクトシーンに戻る
    public void ReturnToStageSelect()
    {
        SetButtonsInteractable(false); // ボタンを無効化

        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("StageSelectScene"));
        }
        else
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }

    // ボタンの有効/無効を設定
    private void SetButtonsInteractable(bool interactable)
    {
        foreach (Button button in allButtons)
        {
            button.interactable = interactable;
        }
    }
}