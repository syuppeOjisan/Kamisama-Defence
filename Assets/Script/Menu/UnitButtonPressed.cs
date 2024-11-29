using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButtonPressed : MonoBehaviour
{
    public Button button;   // ���䂷��{�^��

    private ColorBlock normal;
    private ColorBlock selected;
    private Toggle toggle;

    public void Start()
    {
        if(button == null)
        {
            Debug.LogError("�{�^�����ݒ肳��Ă��܂���");
        }
        else
        {
            // ������
            normal = button.colors;
            selected = button.colors;
        }

        
        normal = SetColorBlock(normal, Color.white);        // �I������Ă��Ȃ��Ƃ��̐F
        selected = SetColorBlock(selected, Color.green);    // �I�����ꂽ�Ƃ��̐F

        // �{�^���̏�Ԃɍ��킹�ĐF��ύX
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
            Debug.LogError("�g�O����������܂���ł���");
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
