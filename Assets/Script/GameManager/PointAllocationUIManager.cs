using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PointAllocationUIManager : MonoBehaviour
{
    [Header("ユニット情報表示用UI")]
    public TMP_Text unitNameText;
    public TMP_Text damageText;
    public TMP_Text rangeText;
    public TMP_Text costText;
    public TMP_Text descriptionText;

    // 情報を更新するメソッド
    public void UpdateUnitInfo(string unitName, float damage, float range, float cost, string description)
    {
        unitNameText.text = $"ユニット名: {unitName}";
        damageText.text = $"ダメージ: {damage}";
        rangeText.text = $"効果範囲: {range}";
        costText.text = $"コスト: {cost}";
        descriptionText.text = description;
    }

    // 情報をクリアするメソッド（必要ないが、将来拡張用）
    public void ClearUnitInfo()
    {
        unitNameText.text = "";
        damageText.text = "";
        rangeText.text = "";
        costText.text = "";
        descriptionText.text = "";
    }
}