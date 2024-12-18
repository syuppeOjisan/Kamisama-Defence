using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitUpgradeManager : MonoBehaviour
{
    public Button upgradeButton;
    public TMP_Text feedbackText; // �t�B�[�h�o�b�N�p�̃e�L�X�g
    public TMP_Text faithPointsText; // �M�|�C���g�̕\���p�e�L�X�g
    public GameObject targetPrefab;

    [Header("DefenseUnit �����ݒ�")]
    public float fireRateIncrease = 0.1f;
    public float projectileSpeedIncrease = 1f;
    public float damageIncrease = 5f;
    public float rangeIncrease = 0.5f;
    public float costIncrease = 10f;
    public string defenseUnitUpgradeMessage = "���U����������܂����I"; // �蓮�ݒ�\�ȃ��b�Z�[�W

    [Header("SpikeUnit �����ݒ�")]
    public float spikeDamageIncrease = 10f;        // �_���[�W�̑�����
    public float spikeRangeIncrease = 0.5f;       // ���ʔ͈͂̑�����
    public float spikeCostIncrease = 10f;         // �R�X�g�̑�����
    public string spikeUnitUpgradeMessage = "�X�p�C�N����������܂����I"; // �蓮�ݒ�\�ȃ��b�Z�[�W

    [Header("CaltropUnit �����ݒ�")]
    public float caltropDamageIncrease = 2f;       // �_���[�W����
    public float caltropRangeIncrease = 0.3f;      // ���ʔ͈͑���
    public float caltropSlowAmountIncrease = 0.05f;// �ړ����x�ቺ������
    public float caltropCostIncrease = 5f;         // �R�X�g����
    public string caltropUnitUpgradeMessage = "�T���H����������܂����I";

    [Header("FlameTrapUnit �����ݒ�")]
    public float flameTrapRangeIncrease = 0.5f;          // ���ʔ͈͑���
    public float flameTrapInitialDamageIncrease = 5f;    // �����_���[�W����
    public float flameTrapSustainedDamageIncrease = 2f;  // �p���_���[�W����
    public float flameTrapDurationIncrease = 1f;         // �������ԑ���
    public float flameTrapCostIncrease = 10f;            // �R�X�g����
    public string flameTrapUnitUpgradeMessage = "�Ή�㩂���������܂����I";

    [Header("BearTrapUnit �����ݒ�")]
    public float bearTrapDamageIncrease = 5f;          // �_���[�W����
    public float bearTrapStunDurationIncrease = 1f;    // �S�����ԑ���
    public float bearTrapCooldownReduction = 0.5f;     // �N�[���_�E�����ԒZ�k
    public float bearTrapRangeIncrease = 0.5f;         // ���ʔ͈͑���
    public float bearTrapCostIncrease = 10f;           // �R�X�g����
    public string bearTrapUnitUpgradeMessage = "�g���o�T�~����������܂����I";

    [Header("FenceUnit �����ݒ�")]
    public float fenceUnitTimerIncrease = 5f;          // �^�C�}�[�����l
    public float fenceUnitCostIncrease = 10f;         // �R�X�g�����l
    public string fenceUnitUpgradeMessage = "�򃆃j�b�g����������܂����I";

    [Header("SlowUnit1 �����ݒ�")]
    public float slowUnit1EffectRangeIncrease = 0.5f;   // ���ʔ͈͂̑�����
    public float slowUnit1AmountIncrease = 0.1f;       // �ړ����x�ቺ�ʂ̑����l
    public float slowUnit1CostIncrease = 10f;          // �R�X�g�̑����l
    public string slowUnit1UpgradeMessage = "���P����������܂����I";

    [Header("SlowUnit2 �����ݒ�")]
    public float slowUnit2EffectRangeIncrease = 0.5f;   // ���ʔ͈͂̑�����
    public float slowUnit2AmountIncrease = 0.1f;       // �ړ����x�ቺ�ʂ̑����l
    public float slowUnit2CostIncrease = 10f;          // �R�X�g�̑����l
    public string slowUnit2UpgradeMessage = "���艖����������܂����I";

    [Header("SlowUnit3 �����ݒ�")]
    public float slowUnit3EffectRangeIncrease = 0.5f;   // ���ʔ͈͂̑�����
    public float slowUnit3AmountIncrease = 0.1f;       // �ړ����x�ቺ�ʂ̑����l
    public float slowUnit3CostIncrease = 10f;          // �R�X�g�̑����l
    public string slowUnit3UpgradeMessage = "�������艖����������܂����I";

    [Header("ShrineUnit �����ݒ�")]
    public float shrineUnitIntervalReduction = 0.1f; // �|�C���g�t�^�C���^�[�o���̒Z�k��
    public float shrineUnitPointIncrease = 5f;       // �t�^�|�C���g�ʂ̑����l
    public float shrineUnitCostIncrease = 10f;       // �R�X�g�̑����l
    public string shrineUnitUpgradeMessage = "�K����������܂����I";

    [Header("WaterStationUnit �����ݒ�")]
    public float waterStationRangeIncrease = 0.5f;      // ���ʔ͈͂̑�����
    public float waterStationSpeedIncrease = 0.1f;     // �ړ����x�㏸�ʂ̑����l
    public float waterStationCostIncrease = 10f;       // �R�X�g�̑����l
    public string waterStationUpgradeMessage = "��˂���������܂����I";

    [Header("MagicCircleUnit �����ݒ�")]
    public float magicCircleRangeIncrease = 0.5f;   // ���ʔ͈͂̑�����
    public float magicCircleCooldownReduction = 0.5f; // �C���^�[�o�����Ԃ̒Z�k��
    public float magicCircleCostIncrease = 10f;    // �R�X�g�̑�����
    public string magicCircleUnitUpgradeMessage = "���@�w����������܂����I";

    [Header("���ʐݒ�")]
    public int upgradeCost = 50; // �M�|�C���g�̏����

    private void Start()
    {
        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeUnit);
        }

        if (feedbackText != null)
        {
            feedbackText.text = ""; // ������Ԃł͋�ɂ���
        }

        // �M�|�C���g��UI��������
        UpdateFaithPointsUI();
    }

    public void UpgradeUnit()
    {
        // �M�|�C���g������Ȃ��ꍇ
        if (FaithPointManager.Instance.GetTotalFaithPoints() < upgradeCost)
        {
            DisplayFeedback("�M�|�C���g������܂���I");
            return;
        }

        // �Ώۃv���n�u���ݒ肳��Ă��Ȃ��ꍇ
        if (targetPrefab == null)
        {
            DisplayFeedback("�����Ώۂ̃��j�b�g���ݒ肳��Ă��܂���I");
            return;
        }

        // DefenseUnit �̏ꍇ
        DefenseUnit defenseUnit = targetPrefab.GetComponent<DefenseUnit>();
        if (defenseUnit != null)
        {
            UpgradeDefenseUnit(defenseUnit);
            DisplayFeedback(defenseUnitUpgradeMessage); // �w�肳�ꂽ���b�Z�[�W��\��
            return;
        }

        // SpikeUnit �̏ꍇ
        SpikeUnit spikeUnit = targetPrefab.GetComponent<SpikeUnit>();
        if (spikeUnit != null)
        {
            UpgradeSpikeUnit(spikeUnit);
            DisplayFeedback(spikeUnitUpgradeMessage); // �w�肳�ꂽ���b�Z�[�W��\��
            return;
        }

        // CaltropUnit �̏ꍇ
        CaltropUnit caltropUnit = targetPrefab.GetComponent<CaltropUnit>();
        if (caltropUnit != null)
        {
            UpgradeCaltropUnit(caltropUnit);
            DisplayFeedback(caltropUnitUpgradeMessage);
            return;
        }

        // FlameTrapUnit �̏ꍇ
        FlameTrapUnit flameTrapUnit = targetPrefab.GetComponent<FlameTrapUnit>();
        if (flameTrapUnit != null)
        {
            UpgradeFlameTrapUnit(flameTrapUnit);
            DisplayFeedback(flameTrapUnitUpgradeMessage);
            return;
        }

        // BearTrapUnit �̏ꍇ
        BearTrapUnit bearTrapUnit = targetPrefab.GetComponent<BearTrapUnit>();
        if (bearTrapUnit != null)
        {
            UpgradeBearTrapUnit(bearTrapUnit);
            DisplayFeedback(bearTrapUnitUpgradeMessage);
            return;
        }

        // FenceUnit �̏ꍇ
        FenceUnit fenceUnit = targetPrefab.GetComponent<FenceUnit>();
        if (fenceUnit != null)
        {
            UpgradeFenceUnit(fenceUnit);
            DisplayFeedback(fenceUnitUpgradeMessage);
            return;
        }

        // SlowUnit1 �̏ꍇ
        SlowUnit1 slowUnit1 = targetPrefab.GetComponent<SlowUnit1>();
        if (slowUnit1 != null)
        {
            UpgradeSlowUnit1(slowUnit1);
            DisplayFeedback(slowUnit1UpgradeMessage);
            return;
        }

        // SlowUnit2 �̏ꍇ
        SlowUnit2 slowUnit2 = targetPrefab.GetComponent<SlowUnit2>();
        if (slowUnit2 != null)
        {
            UpgradeSlowUnit2(slowUnit2);
            DisplayFeedback(slowUnit2UpgradeMessage);
            return;
        }

        // SlowUnit3 �̏ꍇ
        SlowUnit3 slowUnit3 = targetPrefab.GetComponent<SlowUnit3>();
        if (slowUnit3 != null)
        {
            UpgradeSlowUnit3(slowUnit3);
            DisplayFeedback(slowUnit3UpgradeMessage);
            return;
        }

        // ShrineUnit �̏ꍇ
        ShrineUnit shrineUnit = targetPrefab.GetComponent<ShrineUnit>();
        if (shrineUnit != null)
        {
            UpgradeShrineUnit(shrineUnit);
            DisplayFeedback(shrineUnitUpgradeMessage);
            return;
        }

        // WaterStationUnit �̏ꍇ
        WaterStationUnit waterStationUnit = targetPrefab.GetComponent<WaterStationUnit>();
        if (waterStationUnit != null)
        {
            UpgradeWaterStationUnit(waterStationUnit);
            DisplayFeedback(waterStationUpgradeMessage);
            return;
        }

        // MagicCircleUnit �̏ꍇ
        MagicCircleUnit magicCircleUnit = targetPrefab.GetComponent<MagicCircleUnit>();
        if (magicCircleUnit != null)
        {
            UpgradeMagicCircleUnit(magicCircleUnit);
            DisplayFeedback(magicCircleUnitUpgradeMessage);
            return;
        }

        // �Ή����郆�j�b�g�^�C�v��������Ȃ��ꍇ
        DisplayFeedback("�w�肳�ꂽ���j�b�g�̓T�|�[�g����Ă��܂���I");
    }

    private void UpgradeDefenseUnit(DefenseUnit defenseUnit)
    {
        for (int i = 0; i < defenseUnit.fireRates.Length; i++)
        {
            defenseUnit.fireRates[i] += fireRateIncrease;
            defenseUnit.projectileSpeeds[i] += projectileSpeedIncrease;
            defenseUnit.damages[i] += damageIncrease;
            defenseUnit.ranges[i] += rangeIncrease;
            defenseUnit.upgradeCosts[i] += costIncrease;
        }

        ApplyUpgrade();
    }

    private void UpgradeSpikeUnit(SpikeUnit spikeUnit)
    {
        for (int i = 0; i < spikeUnit.damage.Length; i++)
        {
            spikeUnit.damage[i] += spikeDamageIncrease;
            spikeUnit.effectiveRange[i] += spikeRangeIncrease;
            spikeUnit.upgradeCosts[i] += spikeCostIncrease;
        }

        ApplyUpgrade();
    }

    private void UpgradeCaltropUnit(CaltropUnit caltropUnit)
    {
        for (int i = 0; i < caltropUnit.damagePerLevel.Length; i++)
        {
            caltropUnit.damagePerLevel[i] += caltropDamageIncrease;
            caltropUnit.effectRadiusPerLevel[i] += caltropRangeIncrease;
            caltropUnit.slowAmountPerLevel[i] += caltropSlowAmountIncrease;
            caltropUnit.upgradeCosts[i] += caltropCostIncrease;
        }

        ApplyUpgrade();
    }

    private void UpgradeFlameTrapUnit(FlameTrapUnit flameTrapUnit)
    {
        for (int i = 0; i < flameTrapUnit.effectRange.Length; i++)
        {
            flameTrapUnit.effectRange[i] += flameTrapRangeIncrease;
            flameTrapUnit.initialDamage[i] += flameTrapInitialDamageIncrease;
            flameTrapUnit.sustainedDamage[i] += flameTrapSustainedDamageIncrease;
            flameTrapUnit.damageDuration[i] += flameTrapDurationIncrease;
            flameTrapUnit.upgradeCosts[i] += flameTrapCostIncrease;
        }

        ApplyUpgrade();
    }

    private void UpgradeBearTrapUnit(BearTrapUnit bearTrapUnit)
    {
        for (int i = 0; i < bearTrapUnit.damage.Length; i++)
        {
            bearTrapUnit.damage[i] += bearTrapDamageIncrease;
            bearTrapUnit.stunDuration[i] += bearTrapStunDurationIncrease;
            bearTrapUnit.cooldownTime[i] -= bearTrapCooldownReduction;
            bearTrapUnit.effectiveRange[i] += bearTrapRangeIncrease;
            bearTrapUnit.upgradeCosts[i] += bearTrapCostIncrease;

            // �N�[���_�E�����Ԃ����ɂȂ�̂�h��
            if (bearTrapUnit.cooldownTime[i] < 0f)
            {
                bearTrapUnit.cooldownTime[i] = 0f;
            }
        }

        ApplyUpgrade();
    }

    private void UpgradeFenceUnit(FenceUnit fenceUnit)
    {
        for (int i = 0; i < fenceUnit.timerLevels.Length; i++)
        {
            fenceUnit.timerLevels[i] += fenceUnitTimerIncrease;
            fenceUnit.upgradeCosts[i] += fenceUnitCostIncrease;
        }

        ApplyUpgrade();
    }

    private void UpgradeSlowUnit1(SlowUnit1 slowUnit1)
    {
        for (int i = 0; i < slowUnit1.slowEffectRange.Length; i++)
        {
            slowUnit1.slowEffectRange[i] += slowUnit1EffectRangeIncrease;
            slowUnit1.slowAmount[i] += slowUnit1AmountIncrease;
            slowUnit1.upgradeCosts[i] += slowUnit1CostIncrease;
        }

        ApplyUpgrade();
    }

    private void UpgradeSlowUnit2(SlowUnit2 slowUnit2)
    {
        for (int i = 0; i < slowUnit2.slowEffectRange.Length; i++)
        {
            slowUnit2.slowEffectRange[i] += slowUnit2EffectRangeIncrease;
            slowUnit2.slowAmount[i] += slowUnit2AmountIncrease;
            slowUnit2.upgradeCosts[i] += slowUnit2CostIncrease;
        }

        ApplyUpgrade();
    }

    private void UpgradeSlowUnit3(SlowUnit3 slowUnit3)
    {
        for (int i = 0; i < slowUnit3.slowEffectRange.Length; i++)
        {
            slowUnit3.slowEffectRange[i] += slowUnit3EffectRangeIncrease;
            slowUnit3.slowAmount[i] += slowUnit3AmountIncrease;
            slowUnit3.upgradeCosts[i] += slowUnit3CostIncrease;
        }

        ApplyUpgrade();
    }

    private void UpgradeShrineUnit(ShrineUnit shrineUnit)
    {
        for (int i = 0; i < shrineUnit.pointAdditionIntervals.Length; i++)
        {
            shrineUnit.pointAdditionIntervals[i] = Mathf.Max(0.1f, shrineUnit.pointAdditionIntervals[i] - shrineUnitIntervalReduction); // �C���^�[�o����Z�k�i�Œ�l0.1�b�j
            shrineUnit.pointsToAdd[i] += shrineUnitPointIncrease;
            shrineUnit.upgradeCosts[i] += shrineUnitCostIncrease;
        }

        ApplyUpgrade();
    }

    private void UpgradeWaterStationUnit(WaterStationUnit waterStationUnit)
    {
        for (int i = 0; i < waterStationUnit.effectiveRanges.Length; i++)
        {
            waterStationUnit.effectiveRanges[i] += waterStationRangeIncrease;
            waterStationUnit.speedIncreaseAmounts[i] += waterStationSpeedIncrease;
            waterStationUnit.upgradeCosts[i] += waterStationCostIncrease;
        }

        ApplyUpgrade();
    }

    // MagicCircleUnit �̋�������
    private void UpgradeMagicCircleUnit(MagicCircleUnit magicCircleUnit)
    {
        for (int i = 0; i < magicCircleUnit.effectRange.Length; i++)
        {
            magicCircleUnit.effectRange[i] += magicCircleRangeIncrease;
            magicCircleUnit.cooldownTimes[i] = Mathf.Max(0.1f, magicCircleUnit.cooldownTimes[i] - magicCircleCooldownReduction); // �N�[���_�E�����Ԃ�Z�k�i�Œ�l0.1�b�j
            magicCircleUnit.upgradeCosts[i] += magicCircleCostIncrease;
        }

        ApplyUpgrade();
    }

    private void ApplyUpgrade()
    {
        // �M�|�C���g������
        FaithPointManager.Instance.DeductFaithPoints(upgradeCost);

        // �M�|�C���gUI���X�V
        UpdateFaithPointsUI();
    }

    private void DisplayFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message; // ���b�Z�[�W��ݒ�
            feedbackText.color = Color.green;
            CancelInvoke(nameof(ClearFeedback));
            Invoke(nameof(ClearFeedback), 2f); // 2�b��Ƀt�B�[�h�o�b�N���N���A
        }
    }

    private void ClearFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.text = ""; // �t�B�[�h�o�b�N���N���A
        }
    }

    private void UpdateFaithPointsUI()
    {
        if (faithPointsText != null)
        {
            faithPointsText.text = $"�����M�|�C���g: {FaithPointManager.Instance.GetTotalFaithPoints()}";
        }
    }
}