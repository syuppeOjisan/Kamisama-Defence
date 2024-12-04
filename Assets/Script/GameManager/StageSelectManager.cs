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
            SetButtonsInteractable(false); // フェード中はボタンを無効化
            StartCoroutine(fadeController.FadeIn(() =>
            {
                SetButtonsInteractable(true); // フェードイン完了後にボタンを有効化
            }));
        }

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
        SetButtonsInteractable(false); // ボタンを無効化
        PlayerPrefs.SetInt("SelectedStage", stageNumber);

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
        SetButtonsInteractable(false); // ボタンを無効化

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
        SetButtonsInteractable(false); // ボタンを無効化

        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("PointAllocationScene"));
        }
        else
        {
            SceneManager.LoadScene("PointAllocationScene");
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