using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitEquipManager : MonoBehaviour
{
    [Header("装備ボタン")]
    public Button[] equipButtons; // 装備ボタンリスト
    [Header("ボタンに対応したユニット")]
    public GameObject[] units; // ユニットリスト
    public Sprite[] unitIcons; // 各ユニットに対応するアイコン画像

    [Header("UI関連")]
    public TextMeshProUGUI equipLimitText; // 残り装備可能数を表示するUI
    public TextMeshProUGUI errorMessage; // 警告メッセージ用UI

    [HideInInspector]
    public static List<GameObject> equipUnits; // 装備したユニットのリスト
    [HideInInspector]
    public static List<Sprite> equipUnitIcons; // 装備したユニットのアイコンリスト
    public static bool isEquipSelected = false; // 装備を変更したかどうか

    private const int maxEquipLimit = 6; // 最大装備数
    private Dictionary<Button, bool> buttonStates = new Dictionary<Button, bool>(); // ボタンの選択状態

    void Start()
    {
        // リストの初期化
        if (equipUnits == null)
        {
            equipUnits = new List<GameObject>();
            equipUnitIcons = new List<Sprite>();
        }

        // ボタン状態を初期化
        foreach (Button button in equipButtons)
        {
            buttonStates[button] = false; // 全てのボタンを未選択状態に設定
            button.onClick.AddListener(() => ToggleEquip(button)); // ボタンにクリックイベントを追加
        }

        // 初期状態をUIに反映
        UpdateEquipLimitUI();
        HideErrorMessage();
    }

    // ボタンが押されたときに装備のオンオフを切り替える
    private void ToggleEquip(Button button)
    {
        if (!buttonStates.ContainsKey(button))
            return;

        int selectedCount = equipUnits.Count;

        if (buttonStates[button]) // 既に選択されている場合
        {
            buttonStates[button] = false;
            button.GetComponent<Image>().color = Color.white; // デフォルト色に戻す

            int index = System.Array.IndexOf(equipButtons, button);
            if (index >= 0)
            {
                equipUnits.Remove(units[index]);
                equipUnitIcons.Remove(unitIcons[index]);
            }
        }
        else if (selectedCount < maxEquipLimit) // 未選択で装備可能な場合
        {
            buttonStates[button] = true;
            button.GetComponent<Image>().color = Color.green; // 選択色に変更

            int index = System.Array.IndexOf(equipButtons, button);
            if (index >= 0)
            {
                equipUnits.Add(units[index]);
                equipUnitIcons.Add(unitIcons[index]);
            }
        }
        else // 装備上限に達している場合
        {
            ShowErrorMessage("装備上限に達しています！");
        }

        isEquipSelected = equipUnits.Count > 0;
        UpdateEquipLimitUI();
    }

    public void SetEquipUnits()
    {
        equipUnits.Clear(); // リストを更新するので既存内容をクリア
        equipUnitIcons.Clear();

        foreach (var kvp in buttonStates)
        {
            if (kvp.Value) // 選択状態がtrueのボタン
            {
                int index = System.Array.IndexOf(equipButtons, kvp.Key);
                if (index >= 0)
                {
                    equipUnits.Add(units[index]);
                    equipUnitIcons.Add(unitIcons[index]); // アイコンも保存
                }
            }
        }

        isEquipSelected = equipUnits.Count > 0; // 少なくとも1つ装備しているか確認
    }

    // 残り装備可能数をUIで更新
    private void UpdateEquipLimitUI()
    {
        int selectedCount = equipUnits.Count;
        int remainingCount = maxEquipLimit - selectedCount;

        if (remainingCount > 0)
        {
            equipLimitText.text = $"あと{remainingCount}個装備できるよ";
            equipLimitText.color = Color.green;
        }
        else
        {
            equipLimitText.text = "装備上限です";
            equipLimitText.color = Color.red;
        }
    }

    // 警告メッセージを表示
    public void ShowErrorMessage(string message)
    {
        if (errorMessage != null)
        {
            errorMessage.text = message;
            errorMessage.gameObject.SetActive(true);
            Invoke(nameof(HideErrorMessage), 2f); // 2秒後に自動的に非表示
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