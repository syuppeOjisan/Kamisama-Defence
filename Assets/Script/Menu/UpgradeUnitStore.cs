using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUnitStore : MonoBehaviour
{
    [Header("�������������j�b�g")]
    public GameObject unit;
    [Header("�����������X�e�[�^�X��I��")]
    public bool UpgradeAtkPower = false;    // �U���͂������������ꍇ
    public bool UpgradeAtkRange = false;    // �U���͈͂������������ꍇ
    public bool UpgradeLifeTime = false;    // �������Ԃ������������ꍇ
    [Header("�������������l�����")]
    public float UpgradeAmount;             // �����l

    private DefenseUnit_Base baseUnit;  // �������������j�b�g�̃X�e�[�^�X�擾
    private TMP_Text statusNum; // ���݂̃X�e�[�^�X�Ƌ�����̃X�e�[�^�X

    public void Start()
    {
    }

    public void Update()
    {
        if (!unit.TryGetComponent<DefenseUnit_Base>(out baseUnit))
        {
            Debug.LogError("Base���擾�ł��܂���ł���");
        }

        // ��������X�e�[�^�X�ɍ��킹�Đ��l���擾
        float nowStatus = 0.0f;
        if(UpgradeAtkPower)
        {
            nowStatus = baseUnit.GetAttackPower();
        }
        else if(UpgradeAtkRange)
        {
            nowStatus = baseUnit.GetAttackRange();
        }
        else if(UpgradeLifeTime)
        {
            nowStatus = baseUnit.GetLifeTime();
        }

        // �\����ύX
        statusNum = GetComponentInChildren<TMP_Text>();
        if(statusNum)
        {
            statusNum.text = nowStatus.ToString() + "��" + (nowStatus + UpgradeAmount).ToString();
        }
    }
}
