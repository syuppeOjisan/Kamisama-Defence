using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButtonPressed : MonoBehaviour
{
    public Button button;   // 制御するボタン

    private ColorBlock normal;
    private ColorBlock selected;
    private Toggle toggle;

    public void Start()
    {
        if(button == null)
        {
            Debug.LogError("ボタンが設定されていません");
        }
        else
        {
            // 初期化
            normal = button.colors;
            selected = button.colors;
        }

        
        normal = SetColorBlock(normal, Color.white);        // 選択されていないときの色
        selected = SetColorBlock(selected, Color.green);    // 選択されたときの色

        // ボタンの状態に合わせて色を変更
        if (TryGetComponent<Toggle>(out toggle))
        {
            if (toggle.isOn)
            {
                button.colors = selected;
            }
            else
            {
                button.colors = normal;
            }
        }

    }

    public void ButtonPressed()
    {
        if(!TryGetComponent<Toggle>(out Toggle toggle))
        {
            Debug.LogError("トグルが見つかりませんでした");
            return;
        }

        if(toggle.isOn)
        {

            button.colors = selected;
        }
        else
        {
            button.colors = normal;
        }
    }

    private ColorBlock SetColorBlock(ColorBlock colorBlock, Color color)
    {
        colorBlock.normalColor = color;
        colorBlock.pressedColor = color;
        colorBlock.selectedColor = color;
        colorBlock.colorMultiplier = 1.0f;

        color.a = 0.5f;
        colorBlock.highlightedColor = color;

        return colorBlock;
    }
}
