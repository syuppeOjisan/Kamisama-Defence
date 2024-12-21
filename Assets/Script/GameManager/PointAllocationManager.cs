using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PointAllocationManager : MonoBehaviour
{
    public Button returnToStageSelectButton; // ステージセレクトに戻るボタン
    public UnitEquipManager equipManager;   // 装備したユニットの情報
    public TextMeshProUGUI errorMessage;    // エラーメッセージ
    public TMP_Text totalFaithPointsText;   // 累計信仰ポイントUI

    void Start()
    {
        // 初期化処理
        if (totalFaithPointsText != null)
        {
            UpdateFaithPointsUI();
        }

        if (errorMessage != null)
        {
            errorMessage.gameObject.SetActive(false); // 初期状態では非表示
        }

        // ボタンのクリックイベントを設定
        returnToStageSelectButton.onClick.AddListener(ReturnToStageSelect);

        if (equipManager == null)
        {
            Debug.LogError("装備マネージャーがアタッチされていません");
        }

        if (errorMessage == null)
        {
            Debug.LogError("エラーメッセージがセットされていません");
        }
        else
        {
            errorMessage.alpha = 0;
            errorMessage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (errorMessage != null && errorMessage.alpha > 0)
        {
            errorMessage.alpha -= 0.001f;
        }
        else if (errorMessage.alpha <= 0)
        {
            errorMessage.gameObject.SetActive(false);
        }
    }

    // エラーメッセージを表示する
    public void ShowErrorMessage(string message)
    {
        if (errorMessage != null)
        {
            errorMessage.text = message;
            errorMessage.gameObject.SetActive(true);
            Invoke(nameof(HideErrorMessage), 2f); // 2秒後に非表示
        }
    }

    // エラーメッセージを非表示にする
    public void HideErrorMessage()
    {
        if (errorMessage != null)
        {
            errorMessage.gameObject.SetActive(false);
        }
    }

    // 信仰ポイントUIを更新する
    private void UpdateFaithPointsUI()
    {
        if (totalFaithPointsText != null)
        {
            totalFaithPointsText.text = $"所持信仰ポイント: {FaithPointManager.Instance.GetTotalFaithPoints()}";
        }
    }

    public void ReturnToStageSelect()
    {
        if (equipManager != null)
        {
            equipManager.SetEquipUnits(); // 装備したユニットのリストを作成

            if (UnitEquipManager.isEquipSelected)
            {
                SceneManager.LoadScene("StageSelectScene");
            }
            else
            {
                ShowErrorMessage("最低1つはユニットを装備してね");
            }
        }
        else
        {
            Debug.LogError("equipManager が設定されていません。");
        }
    }
}