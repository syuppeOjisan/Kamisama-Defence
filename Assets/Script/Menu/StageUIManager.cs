using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour
{
    [Header("ユニットスロットUI")]
    public Image[] unitSlots; // スロットUI（右上）

    private int selectedUnitIndex = 0;

    void Start()
    {
        UpdateUnitSlots(UnitEquipManager.equipUnitIcons, selectedUnitIndex);
    }

    // スロットの表示を更新するメソッド
    public void UpdateUnitSlots(List<Sprite> equippedIcons, int selectedIndex)
    {
        // スロットを初期化
        for (int i = 0; i < unitSlots.Length; i++)
        {
            if (i < equippedIcons.Count)
            {
                unitSlots[i].sprite = equippedIcons[i];
                unitSlots[i].color = (i == selectedIndex) ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1f); // 選択中は明るく、それ以外は暗め
            }
            else
            {
                unitSlots[i].sprite = null;
                unitSlots[i].color = new Color(0, 0, 0, 0.5f); // 空スロットは黒
            }
        }
    }

    // 選択されたユニットインデックスの更新
    public void UpdateSelectedUnit(int newIndex)
    {
        selectedUnitIndex = newIndex;
        UpdateUnitSlots(UnitEquipManager.equipUnitIcons, selectedUnitIndex);
    }
}