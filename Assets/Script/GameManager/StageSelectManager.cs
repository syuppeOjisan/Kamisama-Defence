using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectManager : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    public Button stage4Button;
    public Button stage5Button;
    public Button stage6Button;
    public Button stage7Button;
    public Button stage8Button;
    public Button stage9Button;
    public Button stage10Button;
    public Button stage11Button;
    public Button stage12Button;
    public Button stage13Button;
    public Button stage14Button;
    public Button stage15Button;
    public Button stage16Button;
    public Button stage17Button;
    public Button stage18Button;
    public Button returnToTitleButton; // タイトルに戻るボタン
    public Button pointAllocationButton; // 永続強化シーンへのボタン
    public GameObject scrollBar;    // スクロールバーが入ってるオブジェクト

    [Header("ボタンの数")]
    public float buttonAmount = 10;
    [Header("スクロールビューの上と下")]
    public GameObject scrollView_Top;
    public GameObject scrollView_Bottom;

    private FadeController fadeController;
    private Button[] allButtons; // シーン内のすべてのボタン
    private ScrollRect scrollRect;  // スクロールバー本体
    private float moveAmount;   // スクロールバーの移動量

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
        stage4Button.onClick.AddListener(() => SelectStage(4));
        stage5Button.onClick.AddListener(() => SelectStage(5));
        stage6Button.onClick.AddListener(() => SelectStage(6));
        stage7Button.onClick.AddListener(() => SelectStage(7));
        stage8Button.onClick.AddListener(() => SelectStage(8));
        stage9Button.onClick.AddListener(() => SelectStage(9));
        stage10Button.onClick.AddListener(() => SelectStage(10));
        //stage11Button.onClick.AddListener(() => SelectStage(11));
        //stage12Button.onClick.AddListener(() => SelectStage(12));
        //stage13Button.onClick.AddListener(() => SelectStage(13));
        //stage14Button.onClick.AddListener(() => SelectStage(14));
        //stage15Button.onClick.AddListener(() => SelectStage(15));
        //stage16Button.onClick.AddListener(() => SelectStage(16));
        //stage17Button.onClick.AddListener(() => SelectStage(17));
        //stage18Button.onClick.AddListener(() => SelectStage(18));
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        pointAllocationButton.onClick.AddListener(GoToPointAllocation);

        // スクロールバーの本体を取得
        if(!scrollBar.TryGetComponent<ScrollRect>(out scrollRect))  
        {
            Debug.LogError("ScrollRectが取得できませんでした");
        }

        // スクロールバーの移動量を計算
        moveAmount = 1 / (buttonAmount - 6f);
    }

    void Update()
    {
        UpdateScroll();
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

    // コントローラーでスクロールバーを移動
    private void UpdateScroll()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position.y >= scrollView_Top.transform.position.y)
            {
                scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition + moveAmount;
            }
            else if (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position.y <= scrollView_Bottom.transform.position.y)
            {
                scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition - moveAmount;
            }
        }
    }
}