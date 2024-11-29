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
    public TextMeshProUGUI errorMessage;        // エラーメッセージ

    void Start()
    {
        // ボタンのクリックイベントを設定
        returnToStageSelectButton.onClick.AddListener(ReturnToStageSelect);

        if(equipManager == null)
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