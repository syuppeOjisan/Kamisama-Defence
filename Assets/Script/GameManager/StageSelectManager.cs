using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    public Button returnToTitleButton; // タイトルに戻るボタン
    public Button pointAllocationButton; // 永続強化シーンへのボタン

    private FadeController fadeController;

    void Start()
    {
        // FadeControllerをシーン内から探す
        fadeController = FindObjectOfType<FadeController>();

        // シーン開始時にフェードインを実行
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeIn());
        }

        // 初期化：ステージ選択時はキャラクター選択を考慮しない
        stage1Button.interactable = true;
        stage2Button.interactable = true;
        stage3Button.interactable = true;

        // ボタンのクリックイベントを設定
        stage1Button.onClick.AddListener(() => SelectStage(1));
        stage2Button.onClick.AddListener(() => SelectStage(2));
        stage3Button.onClick.AddListener(() => SelectStage(3));
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        pointAllocationButton.onClick.AddListener(GoToPointAllocation);
    }

    // ステージ選択してキャラクターセレクトに移行
    public void SelectStage(int stageNumber)
    {
        // 選択したステージ番号をPlayerPrefsに保存
        PlayerPrefs.SetInt("SelectedStage", stageNumber);

        // キャラクターセレクトシーンに遷移（フェードアウト付き）
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("CharacterSelectScene"));
        }
        else
        {
            SceneManager.LoadScene("CharacterSelectScene");
        }
    }

    // タイトルシーンに戻る
    public void ReturnToTitle()
    {
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("TitleScene"));
        }
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    // 永続強化シーンに遷移する
    public void GoToPointAllocation()
    {
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("PointAllocationScene"));
        }
        else
        {
            SceneManager.LoadScene("PointAllocationScene");
        }
    }
}
