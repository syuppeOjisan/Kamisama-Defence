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

    // 信仰ポイントUIを更新する
    private void UpdateFaithPointsUI()
    {
        if (totalFaithPointsText != null)
        {
            totalFaithPointsText.text = $"所持信仰ポイント: {FaithPointManager.Instance.GetTotalFaithPoints()}";
        }
    }

    // ステージセレクトシーンに戻る
    public void ReturnToStageSelect()
    {
        equipManager.SetEquipUnits();   // 装備したユニットのリストを作成

        if (UnitEquipManager.isEquipSelected)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
        else
        {
            if (errorMessage != null)
            {
                errorMessage.gameObject.SetActive(true);
                errorMessage.alpha = 1.5f;
            }
        }
    }
}