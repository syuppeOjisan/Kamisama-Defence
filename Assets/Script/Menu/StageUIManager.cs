using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour
{
    [Header("���j�b�g�X���b�gUI")]
    public Image[] unitSlots; // �X���b�gUI�i�E��j

    private int selectedUnitIndex = 0;

    void Start()
    {
        UpdateUnitSlots(UnitEquipManager.equipUnitIcons, selectedUnitIndex);
    }

    // �X���b�g�̕\�����X�V���郁�\�b�h
    public void UpdateUnitSlots(List<Sprite> equippedIcons, int selectedIndex)
    {
        // �X���b�g��������
        for (int i = 0; i < unitSlots.Length; i++)
        {
            if (i < equippedIcons.Count)
            {
                unitSlots[i].sprite = equippedIcons[i];
                unitSlots[i].color = (i == selectedIndex) ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1f); // �I�𒆂͖��邭�A����ȊO�͈Â�
            }
            else
            {
                unitSlots[i].sprite = null;
                unitSlots[i].color = new Color(0, 0, 0, 0.5f); // ��X���b�g�͍�
            }
        }
    }

    // �I�����ꂽ���j�b�g�C���f�b�N�X�̍X�V
    public void UpdateSelectedUnit(int newIndex)
    {
        selectedUnitIndex = newIndex;
        UpdateUnitSlots(UnitEquipManager.equipUnitIcons, selectedUnitIndex);
    }
}