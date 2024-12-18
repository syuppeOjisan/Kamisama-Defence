using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitIconManager : MonoBehaviour
{
    [Header("���j�b�g�A�C�R��")]
    public List<Image> unitIcons; // ���j�b�g�A�C�R����Image�R���|�[�l���g
    public Color selectedColor = Color.white; // ���邢�F
    public Color unselectedColor = new Color(0.5f, 0.5f, 0.5f, 1f); // �Â��F

    private int selectedUnitIndex = 0; // ���ݑI�𒆂̃��j�b�g�C���f�b�N�X

    void Start()
    {
        UpdateIconColors(); // ������Ԃ�ݒ�
    }

    void Update()
    {
        HandleUnitSelection();
    }

    // ���j�b�g�I���𐧌�
    void HandleUnitSelection()
    {
        int previousIndex = selectedUnitIndex;

        if (Input.GetKeyDown(KeyCode.Alpha1)) // ���̃��j�b�g��I��
        {
            selectedUnitIndex = (selectedUnitIndex + 1) % unitIcons.Count;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // �O�̃��j�b�g��I��
        {
            selectedUnitIndex = (selectedUnitIndex - 1 + unitIcons.Count) % unitIcons.Count;
        }

        if (previousIndex != selectedUnitIndex)
        {
            UpdateIconColors(); // ���j�b�g���؂�ւ������UI���X�V
        }
    }

    // �A�C�R���̐F���X�V
    void UpdateIconColors()
    {
        for (int i = 0; i < unitIcons.Count; i++)
        {
            unitIcons[i].color = (i == selectedUnitIndex) ? selectedColor : unselectedColor;
        }
    }
}