using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StageSelectManager : MonoBehaviour
{
    public Button[] stageButtons; // 全てのステージボタン
    public Button returnToTitleButton; // タイトルに戻るボタン
    public Button pointAllocationButton; // 永続強化シーンへのボタン
    public GameObject equipPromptPanel; // ユニット装備促進メッセージのパネル
    public TextMeshProUGUI equipPromptText; // メッセージ用テキスト
    public Image equipPromptImage; // 背景用の画像
    public ScrollRect stageScrollView; // スクロールビューのScrollRect

    private FadeController fadeController;

    void Start()
    {
        // FadeControllerをシーン内から探す
        fadeController = FindObjectOfType<FadeController>();

        // ユニット装備状態を確認
        CheckUnitEquipStatus();

        // Fadeイン処理
        if (fadeController != null)
        {
            SetButtonsInteractable(false); // フェード中はボタンを無効化
            StartCoroutine(fadeController.FadeIn(() => SetButtonsInteractable(true)));
        }

        // ボタンのクリックイベントを設定
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageNumber = i + 1;
            stageButtons[i].onClick.AddListener(() => SelectStage(stageNumber));
        }
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
        SetButtonsInteractable(false);

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
        SetButtonsInteractable(false);

        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("PointAllocationScene"));
        }
        else
        {
            SceneManager.LoadScene("PointAllocationScene");
        }
    }

    // ユニットが装備されているかを確認してScroll Viewとボタンを無効化/有効化
    private void CheckUnitEquipStatus()
    {
        bool isUnitEquipped = UnitEquipManager.equipUnits != null && UnitEquipManager.equipUnits.Count > 0;

        if (isUnitEquipped)
        {
            SetStageButtonsInteractable(true);
            stageScrollView.enabled = true; // Scroll Viewの操作を有効化
            equipPromptPanel.SetActive(false); // メッセージ非表示
        }
        else
        {
            SetStageButtonsInteractable(false);
            stageScrollView.enabled = false; // Scroll Viewの操作を無効化
            equipPromptPanel.SetActive(true); // メッセージ表示
            equipPromptText.text = "まずはユニットを装備しよう！";
        }
    }

    // ステージボタンの有効/無効を設定
    private void SetStageButtonsInteractable(bool interactable)
    {
        foreach (Button button in stageButtons)
        {
            button.interactable = interactable;
        }
    }

    // ボタンの有効/無効を設定（フェード時）
    private void SetButtonsInteractable(bool interactable)
    {
        returnToTitleButton.interactable = interactable;
        pointAllocationButton.interactable = interactable;
        SetStageButtonsInteractable(interactable);
    }
}