using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PointAllocationUIManager : MonoBehaviour
{
    [Header("���j�b�g���\���pUI")]
    public TMP_Text unitNameText;
    public TMP_Text damageText;
    public TMP_Text rangeText;
    public TMP_Text costText;
    public TMP_Text descriptionText;

    // �����X�V���郁�\�b�h
    public void UpdateUnitInfo(string unitName, float damage, float range, float cost, string description)
    {
        unitNameText.text = $"���j�b�g��: {unitName}";
        damageText.text = $"�_���[�W: {damage}";
        rangeText.text = $"���ʔ͈�: {range}";
        costText.text = $"�R�X�g: {cost}";
        descriptionText.text = description;
    }

    // �����N���A���郁�\�b�h�i�K�v�Ȃ����A�����g���p�j
    public void ClearUnitInfo()
    {
        unitNameText.text = "";
        damageText.text = "";
        rangeText.text = "";
        costText.text = "";
        descriptionText.text = "";
    }
}