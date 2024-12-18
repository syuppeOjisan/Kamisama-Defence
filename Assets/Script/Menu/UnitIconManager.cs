using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitIconManager : MonoBehaviour
{
    [Header("ユニットアイコン")]
    public List<Image> unitIcons; // ユニットアイコンのImageコンポーネント
    public Color selectedColor = Color.white; // 明るい色
    public Color unselectedColor = new Color(0.5f, 0.5f, 0.5f, 1f); // 暗い色

    private int selectedUnitIndex = 0; // 現在選択中のユニットインデックス

    void Start()
    {
        UpdateIconColors(); // 初期状態を設定
    }

    void Update()
    {
        HandleUnitSelection();
    }

    // ユニット選択を制御
    void HandleUnitSelection()
    {
        int previousIndex = selectedUnitIndex;

        if (Input.GetKeyDown(KeyCode.Alpha1)) // 次のユニットを選択
        {
            selectedUnitIndex = (selectedUnitIndex + 1) % unitIcons.Count;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // 前のユニットを選択
        {
            selectedUnitIndex = (selectedUnitIndex - 1 + unitIcons.Count) % unitIcons.Count;
        }

        if (previousIndex != selectedUnitIndex)
        {
            UpdateIconColors(); // ユニットが切り替わったらUIを更新
        }
    }

    // アイコンの色を更新
    void UpdateIconColors()
    {
        for (int i = 0; i < unitIcons.Count; i++)
        {
            unitIcons[i].color = (i == selectedUnitIndex) ? selectedColor : unselectedColor;
        }
    }
}