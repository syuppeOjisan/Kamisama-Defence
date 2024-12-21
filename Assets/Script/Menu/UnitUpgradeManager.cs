using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UnitUpgradeManager : MonoBehaviour
{
    public TMP_Text unitInfoText; // UI�Ƀ��j�b�g����\������TextMeshPro

    public Button upgradeButton;
    public TMP_Text feedbackText; // �t�B�[�h�o�b�N�p�̃e�L�X�g
    public TMP_Text faithPointsText; // �M�|�C���g�̕\���p�e�L�X�g
    public GameObject targetPrefab;

    [Header("Audio Settings")]
    public AudioClip successSound; // �����������̌��ʉ�
    public AudioClip failureSound; // �������s���̌��ʉ�
    private AudioSource audioSource;

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

    [Header("MagicCircleUnit �����ݒ�")]
    public float magicCircleRangeIncrease = 0.5f;   // ���ʔ͈͂̑�����
    public float magicCircleCooldownReduction = 0.5f; // �C���^�[�o�����Ԃ̒Z�k��
    public float magicCircleCostIncrease = 10f;    // �R�X�g�̑�����
    public string magicCircleUnitUpgradeMessage = "���@�w����������܂����I";

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

    [Header("���ʐݒ�")]
    public int upgradeCost = 50; // �M�|�C���g�̏����

    // ���j�b�g�{�^���Ƃ��̃v���n�u��ݒ�
    [Header("Unit Buttons and Prefabs")]
    public Button defenseUnitButton;
    public GameObject defenseUnitPrefab;

    public Button spikeUnitButton;
    public GameObject spikeUnitPrefab;

    public Button caltropUnitButton;
    public GameObject caltropUnitPrefab;

    public Button flameTrapUnitButton;
    public GameObject flameTrapUnitPrefab;

    public Button bearTrapUnitButton;
    public GameObject bearTrapUnitPrefab;

    public Button fenceUnitButton;
    public GameObject fenceUnitPrefab;

    public Button slowUnit1Button;
    public GameObject slowUnit1Prefab;

    public Button slowUnit2Button;
    public GameObject slowUnit2Prefab;

    public Button slowUnit3Button;
    public GameObject slowUnit3Prefab;

    public Button magicCircleUnitButton;
    public GameObject magicCircleUnitPrefab;

    public Button shrineUnitButton;
    public GameObject shrineUnitPrefab;

    public Button waterStationButton;
    public GameObject waterStationPrefab;

    private void Start()
    {
        // AudioSource �̏�����
        audioSource = gameObject.AddComponent<AudioSource>();

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeUnit);
        }

        if (feedbackText != null)
        {
            feedbackText.text = ""; // ������Ԃł͋�ɂ���
        }
      
        if (unitInfoText != null)
            unitInfoText.text = "";

        SetupButton(defenseUnitButton, defenseUnitPrefab);
        SetupButton(spikeUnitButton, spikeUnitPrefab);
        SetupButton(caltropUnitButton, caltropUnitPrefab);
        SetupButton(flameTrapUnitButton, flameTrapUnitPrefab);
        SetupButton(bearTrapUnitButton, bearTrapUnitPrefab);
        SetupButton(fenceUnitButton, fenceUnitPrefab);
        SetupButton(slowUnit1Button, slowUnit1Prefab);
        SetupButton(slowUnit2Button, slowUnit2Prefab);
        SetupButton(slowUnit3Button, slowUnit3Prefab);
        SetupButton(magicCircleUnitButton, magicCircleUnitPrefab);
        SetupButton(shrineUnitButton, shrineUnitPrefab);
        SetupButton(waterStationButton, waterStationPrefab);
        

        // �M�|�C���g��UI��������
        UpdateFaithPointsUI();
    }

    // ���j�b�g����\������
    public void DisplayUnitInfo(GameObject unitPrefab)
    {
        if (unitPrefab == null || unitInfoText == null) return;

        string info = "���j�b�g���\n";

        // �e���j�b�g�^�C�v�ɑΉ�
        if (unitPrefab.TryGetComponent(out DefenseUnit defenseUnit))
        {
            //���U���j�b�g
            info += "�͈͓��̓G�ɑ΂��Ēe�𔭎˂���";
            info += "\n";
            info += $"�_���[�W: {defenseUnit.damages[0].ToString("F2")}\n";
            info += $"�A�ˑ��x: {defenseUnit.fireRates[0].ToString("F2")}/�b\n";
            info += $"�e��: {defenseUnit.projectileSpeeds[0].ToString("F2")}\n";
            info += $"�˒�����: {defenseUnit.ranges[0].ToString("F2")}\n";
            info += $"�R�X�g: {defenseUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g20\n�E�_���[�W�{0.05\n�E�A�ˑ��x�{0.02\n�E�e���{0.02\n�E�˒������{0.05";

        }
        else if (unitPrefab.TryGetComponent(out SpikeUnit spikeUnit))
        {
            //�X�p�C�N���j�b�g
            info += "���ʔ͈͂ɐG�ꂽ�G�Ɉ�x�����_���[�W��^����";
            info += "\n";
            info += $"�_���[�W: {spikeUnit.damage[0].ToString("F2")}\n";
            info += $"���ʔ͈�: {spikeUnit.effectiveRange[0].ToString("F2")}\n";
            info += $"�R�X�g: {spikeUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g30\n�E�_���[�W�{0.1\n�E���ʔ͈́{0.05";
        }
        else if (unitPrefab.TryGetComponent(out CaltropUnit caltropUnit))
        {
            //�܂��т����j�b�g
            info += "���ʔ͈͓��̓G�̈ړ����x�������Čp���_���[�W��^����";
            info += "\n";
            info += $"�_���[�W: {caltropUnit.damagePerLevel[0].ToString("F2")}\n";
            info += $"�ړ����x�ቺ��: {caltropUnit.slowAmountPerLevel[0].ToString("F2")}�{\n";
            info += $"���ʔ͈�: {caltropUnit.effectRadiusPerLevel[0].ToString("F2")}\n";
            info += $"�R�X�g: {caltropUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g20\n�E�_���[�W�{0.01\n�E���ʔ͈́{0.05";
        }
        else if (unitPrefab.TryGetComponent(out FlameTrapUnit flameTrapUnit))
        {
            //�Ԍ��򃆃j�b�g
            info += "�M���ɂ��_���[�W�Ƃ₯�ǂɂ��p���_���[�W��^����";
            info += "\n";
            info += $"�����_���[�W: {flameTrapUnit.initialDamage[0].ToString("F2")}\n";
            info += $"�p���_���[�W: {flameTrapUnit.sustainedDamage[0].ToString("F2")}\n";
            info += $"�p������: {flameTrapUnit.damageDuration[0].ToString("F2")}\n";
            info += $"���ʔ͈�: {flameTrapUnit.effectRange[0].ToString("F2")}\n";
            info += $"�R�X�g: {flameTrapUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g40\n�E�����_���[�W�{0.05\n�E�p���_���[�W�{0.05\n�E�p�����ԁ{0.02\n�E���ʔ͈́{0.05";
        }
        else if (unitPrefab.TryGetComponent(out BearTrapUnit bearTrapUnit))
        {
            //�g���o�T�~���j�b�g
            info += "�_���[�W��^���čő�1�̂܂ł���莞�ԓG���S������B";
            info += "\n";
            info += $"�_���[�W: {bearTrapUnit.damage[0].ToString("F2")}\n";
            info += $"�S������: {bearTrapUnit.stunDuration[0].ToString("F2")}�b\n";
            info += $"�N�[���_�E��: {bearTrapUnit.cooldownTime[0].ToString("F2")}�b\n";
            info += $"���ʔ͈�: {bearTrapUnit.effectiveRange[0].ToString("F2")}\n";
            info += $"�R�X�g: {bearTrapUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g40\n�E�_���[�W�{0.2\n�E�S�����ԁ{0.05\n�E���ʔ͈́{0.03\n";
        }
        else if (unitPrefab.TryGetComponent(out FenceUnit fenceUnit))
        {
            //�|�򃆃j�b�g
            info += "�ݒu�サ�΂炭����Ə���ɉ��鑫�~�ߗp�̃��j�b�g";
            info += "\n";
            info += $"{fenceUnit.timerLevels[0].ToString("F2")}�b��Ɏ���\n";
            info += $"�R�X�g: {fenceUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g20\n�E����܂Ł{0.03\n�E�R�X�g�|0.01";
        }
        else if (unitPrefab.TryGetComponent(out SlowUnit1 slowUnit1))
        {
            //���P���j�b�g(slowunit1)
            info += "�G�̈ړ����x�������郆�j�b�g�B���艖�������ʔ͈͂��傫��";
            info += "\n";
            info += $"�ړ����x�ቺ��: {slowUnit1.slowAmount[0].ToString("F2")}\n";
            info += $"���ʔ͈�: {slowUnit1.slowEffectRange[0].ToString("F2")}\n";
            info += $"�R�X�g: {slowUnit1.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g20\n�E���ʔ͈́{0.05";
        }
        else if (unitPrefab.TryGetComponent(out SlowUnit2 slowUnit2))
        {
            //���艖���j�b�g(slowunit2)
            info += "���ʔ͈͂͏��������A���P�����ړ����x�������ݒu�R�X�g������";
            info += "\n";
            info += $"�ړ����x�ቺ��: {slowUnit2.slowAmount[0].ToString("F2")}\n";
            info += $"���ʔ͈�: {slowUnit2.slowEffectRange[0].ToString("F2")}\n";
            info += $"�R�X�g: {slowUnit2.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g10\n�E���ʔ͈́{0.01";
        }
        else if (unitPrefab.TryGetComponent(out SlowUnit3 slowUnit3))
        {
            //�������艖���j�b�g(slowunit3)
            info += "����Ă��܂������艖���j�b�g�B�G�̈ړ����x���グ�Ă��܂�";
            info += "\n";
            info += $"�ړ����x�ቺ��: {slowUnit3.slowAmount[0].ToString("F2")}\n";
            info += $"���ʔ͈�: {slowUnit3.slowEffectRange[0].ToString("F2")}\n";
            info += $"�R�X�g: {slowUnit3.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g5\n�E���ʔ͈́{0.05";
        }
        else if (unitPrefab.TryGetComponent(out MagicCircleUnit magicCircleUnit))
        {
            //���@�w���j�b�g
            info += "���ʔ͈͂ɓ������G���X�|�[���ʒu�܂Ń��[�v������";
            info += "\n";
            info += $"�N�[���_�E��: {magicCircleUnit.cooldownTimes[0].ToString("F2")}�b\n";
            info += $"���ʔ͈�: {magicCircleUnit.effectRange[0].ToString("F2")}\n";
            info += $"�R�X�g: {magicCircleUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g60\n�E�N�[���_�E��-0.02\n�E���ʔ͈́{0.03";
        }
        else if (unitPrefab.TryGetComponent(out ShrineUnit shrineUnit))
        {
            //�K���j�b�g
            info += "�ݒu����ƁA��莞�Ԃ��Ƃɂ��ΑK�����炦��";
            info += "\n";
            info += $"�ǉ��̂��ΑK: {shrineUnit.pointsToAdd[0].ToString("F2")}�|�C���g\n";
            info += $"{shrineUnit.pointAdditionIntervals[0].ToString("F2")}�b���Ƃɒǉ�\n";
            info += $"�R�X�g: {shrineUnit.upgradeCosts[0]}\n";
            info += "�ő�ݒu��:1\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g100\n�E���ΑK�l���܂ł̕b��-0.001";
        }
        else if (unitPrefab.TryGetComponent(out WaterStationUnit waterStationUnit))
        {
            //��˃��j�b�g
            info += "���ʔ͈͓��̎Q�q�q�̈ړ����x���グ��";
            info += "\n";
            info += $"�Q�q�q�̈ړ����x: {waterStationUnit.speedIncreaseAmounts[0].ToString("F2")}�{\n";
            info += $"���ʔ͈�: {waterStationUnit.effectiveRanges[0].ToString("F2")}\n";
            info += $"�R�X�g: {waterStationUnit.upgradeCosts[0]}\n";
            info += "�ő�ݒu��:3\n";
            info += "\n";
            info += "�A�b�v�O���[�h�R�X�g70\n�E���ʔ͈́{0.1";
        }
        else
        {
            info += "��񂪐ݒ肳��Ă��܂���B";
        }

        // UI�ɔ��f
        unitInfoText.text = info;
    }

    // �{�^���̃C�x���g�ݒ�p���\�b�h
    public void SetupButton(Button button, GameObject unitPrefab)
    {
        if (button != null)
        {
            // �{�^���̃z�o�[�C�x���g
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            entry.callback.AddListener((eventData) => { DisplayUnitInfo(unitPrefab); });
            trigger.triggers.Add(entry);
        }
    }

    public void UpgradeUnit()
    {
        // �M�|�C���g������Ȃ��ꍇ
        if (FaithPointManager.Instance.GetTotalFaithPoints() < upgradeCost)
        {
            PlaySound(failureSound); // �������s���̉����Đ�
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
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(defenseUnitUpgradeMessage); // �w�肳�ꂽ���b�Z�[�W��\��
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // SpikeUnit �̏ꍇ
        SpikeUnit spikeUnit = targetPrefab.GetComponent<SpikeUnit>();
        if (spikeUnit != null)
        {
            UpgradeSpikeUnit(spikeUnit);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(spikeUnitUpgradeMessage); // �w�肳�ꂽ���b�Z�[�W��\��
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // CaltropUnit �̏ꍇ
        CaltropUnit caltropUnit = targetPrefab.GetComponent<CaltropUnit>();
        if (caltropUnit != null)
        {
            UpgradeCaltropUnit(caltropUnit);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(caltropUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // FlameTrapUnit �̏ꍇ
        FlameTrapUnit flameTrapUnit = targetPrefab.GetComponent<FlameTrapUnit>();
        if (flameTrapUnit != null)
        {
            UpgradeFlameTrapUnit(flameTrapUnit);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(flameTrapUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // BearTrapUnit �̏ꍇ
        BearTrapUnit bearTrapUnit = targetPrefab.GetComponent<BearTrapUnit>();
        if (bearTrapUnit != null)
        {
            UpgradeBearTrapUnit(bearTrapUnit);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(bearTrapUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // FenceUnit �̏ꍇ
        FenceUnit fenceUnit = targetPrefab.GetComponent<FenceUnit>();
        if (fenceUnit != null)
        {
            UpgradeFenceUnit(fenceUnit);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(fenceUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // SlowUnit1 �̏ꍇ
        SlowUnit1 slowUnit1 = targetPrefab.GetComponent<SlowUnit1>();
        if (slowUnit1 != null)
        {
            UpgradeSlowUnit1(slowUnit1);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(slowUnit1UpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // SlowUnit2 �̏ꍇ
        SlowUnit2 slowUnit2 = targetPrefab.GetComponent<SlowUnit2>();
        if (slowUnit2 != null)
        {
            UpgradeSlowUnit2(slowUnit2);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(slowUnit2UpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // SlowUnit3 �̏ꍇ
        SlowUnit3 slowUnit3 = targetPrefab.GetComponent<SlowUnit3>();
        if (slowUnit3 != null)
        {
            UpgradeSlowUnit3(slowUnit3);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(slowUnit3UpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // ShrineUnit �̏ꍇ
        ShrineUnit shrineUnit = targetPrefab.GetComponent<ShrineUnit>();
        if (shrineUnit != null)
        {
            UpgradeShrineUnit(shrineUnit);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(shrineUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // WaterStationUnit �̏ꍇ
        WaterStationUnit waterStationUnit = targetPrefab.GetComponent<WaterStationUnit>();
        if (waterStationUnit != null)
        {
            UpgradeWaterStationUnit(waterStationUnit);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(waterStationUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // MagicCircleUnit �̏ꍇ
        MagicCircleUnit magicCircleUnit = targetPrefab.GetComponent<MagicCircleUnit>();
        if (magicCircleUnit != null)
        {
            UpgradeMagicCircleUnit(magicCircleUnit);
            PlaySound(successSound); // �����������̉����Đ�
            DisplayFeedback(magicCircleUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // �����X�V
            return;
        }

        // �Ή����郆�j�b�g�^�C�v��������Ȃ��ꍇ
        DisplayFeedback("�w�肳�ꂽ���j�b�g�̓T�|�[�g����Ă��܂���I");
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
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