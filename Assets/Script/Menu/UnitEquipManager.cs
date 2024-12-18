using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitEquipManager : MonoBehaviour
{
    [Header("トグルボタン")]
    public Toggle[] toggles;    // 使用するトグルボタン
    [Header("ボタンに対応したユニット")]
    public GameObject[] units;  // ユニットリスト
    public Sprite[] unitIcons;  // 各ユニットに対応するアイコン画像

    [Header("UI関連")]
    public TextMeshProUGUI equipLimitText; // 残り装備可能数を表示するUI
    public TextMeshProUGUI errorMessage;   // 警告メッセージ用UI

    [HideInInspector]
    public static List<GameObject> equipUnits; // 装備したユニットのリスト
    [HideInInspector]
    public static List<Sprite> equipUnitIcons; // 装備したユニットのアイコンリスト
    public static bool isEquipSelected = false; // 装備を変更したかどうか

    private const int maxEquipLimit = 6; // 最大装備数

    void Start()
    {
        // リストの初期化
        if (equipUnits == null)
        {
            equipUnits = new List<GameObject>();
            equipUnitIcons = new List<Sprite>();
        }

        // トグルにリスナーを追加してリアルタイム更新を有効に
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate { UpdateEquipLimitUI(); });
        }

        // 初期状態をUIに反映
        SetToggles();
        UpdateEquipLimitUI();
        HideErrorMessage();
    }

    // ボタンで選択されたユニットをリストに反映
    public void SetEquipUnits()
    {
        equipUnits.Clear(); // リストを更新するので既存内容をクリア
        equipUnitIcons.Clear();

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                equipUnits.Add(units[i]);
                equipUnitIcons.Add(unitIcons[i]); // アイコンも保存
            }
        }

        isEquipSelected = equipUnits.Count > 0; // 少なくとも1つ装備しているか確認
        UpdateEquipLimitUI();
    }

    // トグル状態をUIに反映
    public void SetToggles()
    {
        foreach (GameObject unit in equipUnits)
        {
            int index = Array.IndexOf(units, unit);

            if (index >= 0)
            {
                toggles[index].isOn = true;
            }
        }

        UpdateEquipLimitUI();
    }

    // 残り装備可能数をUIで更新
    private void UpdateEquipLimitUI()
    {
        int selectedCount = CountSelectedUnits();
        int remainingCount = maxEquipLimit - selectedCount;

        if (remainingCount > 0)
        {
            equipLimitText.text = $"あと{remainingCount}個装備できるよ";
            equipLimitText.color = Color.green;

            EnableToggles(true); // トグルを有効化
        }
        else
        {
            equipLimitText.text = "装備上限です";
            equipLimitText.color = Color.red;

            EnableToggles(false); // トグルを無効化
        }
    }

    // トグルの有効/無効を切り替え
    private void EnableToggles(bool enable)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            // 選択されていないトグルのみ制御
            if (!toggles[i].isOn)
            {
                toggles[i].interactable = enable;
            }
        }
    }

    // 選択されているユニットの数をカウント
    private int CountSelectedUnits()
    {
        int count = 0;

        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                count++;
            }
        }

        return count;
    }

    // 警告メッセージを表示
    public void ShowErrorMessage(string message)
    {
        if (errorMessage != null)
        {
            errorMessage.text = message;
            errorMessage.gameObject.SetActive(true);
        }
    }

    // 警告メッセージを非表示
    public void HideErrorMessage()
    {
        if (errorMessage != null)
        {
            errorMessage.gameObject.SetActive(false);
        }
    }

    // ステージセレクトに戻る際のチェック
    public bool CanReturnToStageSelect()
    {
        if (equipUnits.Count == 0)
        {
            ShowErrorMessage("最低1つはユニットを装備してね");
            return false;
        }

        return true;
    }
}