using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    public List<GameObject> unitPrefabs;
    public int selectedUnitIndex = 0;
    public GameObject placementPointerPrefab;
    public AudioClip placementSound;
    public AudioClip removalSound;

    private GridSystem gridSystem; // �X�e�[�W����GridSystem�I�u�W�F�N�g���Q��
    private GameObject placementPointer;
    private bool canPlaceUnit = true;
    private AudioSource audioSource;
    private StageManager stageManager;

    void Start()
    {
        // GridSystem���X�e�[�W�����猟��
        gridSystem = FindObjectOfType<GridSystem>();
        if (gridSystem == null)
        {
            Debug.LogError("�X�e�[�W����GridSystem�I�u�W�F�N�g��������܂���B");
            return;
        }

        // �X�e�[�W�ԍ��ɉ�����CSV�t�@�C����ݒ�
        int currentStage = PlayerPrefs.GetInt("SelectedStage", 0);
        gridSystem.CsvFileName = $"unplaceableCells_stage{currentStage + 1}.csv"; // �X�e�[�W�ԍ��ɉ�����CSV�t�@�C����
        gridSystem.InitializeGrid();

        placementPointer = Instantiate(placementPointerPrefab);
        placementPointer.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();

        stageManager = FindObjectOfType<StageManager>();
        if (stageManager == null)
        {
            Debug.LogError("StageManager���V�[�����ɑ��݂��܂���B");
        }

        // ������ʂőI�񂾃��j�b�g�𔽉f
        if (UnitEquipManager.isEquipSelected)
        {
            unitPrefabs = new List<GameObject>();       // ���X�g������
            unitPrefabs = UnitEquipManager.equipUnits;  // ���������R�s�[
        }
    }

    void Update()
    {
        HandleUnitSelection();
        UpdatePointerPosition();

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton1) && canPlaceUnit)
        {
            Vector3 forwardPosition = transform.position + transform.forward * 2f;
            Vector3 gridPosition = gridSystem.GetGridPosition(forwardPosition);

            if (gridSystem.IsOccupied(gridPosition))
            {
                UpgradeUnitAt(gridPosition);
            }
            else
            {
                PlaceUnit();
            }
        }

        // ���j�b�g�����
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            RemoveUnit();
        }
    }

    // ���j�b�g�̃A�b�v�O���[�h����
    void UpgradeUnitAt(Vector3 gridPosition)
    {
        GameObject existingUnit = gridSystem.GetUnitAt(gridPosition);
        DefenseUnit defenseUnit = existingUnit.GetComponent<DefenseUnit>();
        FenceUnit fenceUnit = existingUnit.GetComponent<FenceUnit>();
        CaltropUnit caltropUnit = existingUnit.GetComponent<CaltropUnit>();
        SpikeUnit spikeUnit = existingUnit.GetComponent<SpikeUnit>();
        SlowUnit1 slowUnit1 = existingUnit.GetComponent<SlowUnit1>();
        SlowUnit2 slowUnit2 = existingUnit.GetComponent<SlowUnit2>();
        SlowUnit3 slowUnit3 = existingUnit.GetComponent<SlowUnit3>();
        FlameTrapUnit flameTrapUnit = existingUnit.GetComponent<FlameTrapUnit>();
        BearTrapUnit bearTrapUnit = existingUnit.GetComponent<BearTrapUnit>();
        ShrineUnit shrineUnit = existingUnit.GetComponent<ShrineUnit>(); // ShrineUnit���`�F�b�N
        WaterStationUnit waterStationUnit = existingUnit.GetComponent<WaterStationUnit>(); // WaterStationUnit�̃`�F�b�N
        Mahojin Mahojin = existingUnit.GetComponent<Mahojin>();//mahojinunit�̃`�F�b�N
        if (defenseUnit != null && stageManager.offeringPoints >= defenseUnit.GetUpgradeCost())
        {
            if (defenseUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-defenseUnit.GetUpgradeCost());
                Debug.Log("DefenseUnit���A�b�v�O���[�h����܂����B");
            }
        }
        else if (fenceUnit != null && stageManager.offeringPoints >= fenceUnit.GetUpgradeCost())
        {
            if (fenceUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-fenceUnit.GetUpgradeCost());
                Debug.Log("FenceUnit���A�b�v�O���[�h����܂����B");
            }
        }
        else if (caltropUnit != null && stageManager.offeringPoints >= caltropUnit.GetUpgradeCost())
        {
            if (caltropUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-caltropUnit.GetUpgradeCost());
                Debug.Log("CaltropUnit���A�b�v�O���[�h����܂����B");
            }
        }
        else if (spikeUnit != null && stageManager.offeringPoints >= spikeUnit.GetUpgradeCost())
        {
            if (spikeUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-spikeUnit.GetUpgradeCost());
                Debug.Log("SpikeUnit���A�b�v�O���[�h����܂����B");
            }
        }
        else if (slowUnit1 != null && stageManager.offeringPoints >= slowUnit1.GetUpgradeCost())
        {
            if (slowUnit1.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-slowUnit1.GetUpgradeCost());
                Debug.Log("SlowUnit1���A�b�v�O���[�h����܂����B");
            }
        }
        else if (slowUnit2 != null && stageManager.offeringPoints >= slowUnit2.GetUpgradeCost())
        {
            if (slowUnit2.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-slowUnit2.GetUpgradeCost());
                Debug.Log("SlowUnit2���A�b�v�O���[�h����܂����B");
            }
        }
        else if (slowUnit3 != null && stageManager.offeringPoints >= slowUnit3.GetUpgradeCost())
        {
            if (slowUnit3.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-slowUnit3.GetUpgradeCost());
                Debug.Log("SlowUnit3���A�b�v�O���[�h����܂����B");
            }
        }
        else if (flameTrapUnit != null && stageManager.offeringPoints >= flameTrapUnit.GetUpgradeCost())
        {
            if (flameTrapUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-flameTrapUnit.GetUpgradeCost());
                Debug.Log("FlameTrapUnit���A�b�v�O���[�h����܂����B");
            }
        }
        else if (bearTrapUnit != null && stageManager.offeringPoints >= bearTrapUnit.GetUpgradeCost())
        {
            if (bearTrapUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-bearTrapUnit.GetUpgradeCost());
                Debug.Log("BearTrapUnit���A�b�v�O���[�h����܂����B");
            }
        }
        else if (shrineUnit != null && stageManager.offeringPoints >= shrineUnit.GetUpgradeCost())
        {
            if (shrineUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-shrineUnit.GetUpgradeCost());
                Debug.Log("ShrineUnit���A�b�v�O���[�h����܂����B");
            }
        }
        else if (waterStationUnit != null && stageManager.offeringPoints >= waterStationUnit.GetUpgradeCost())
        {
            if (waterStationUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-waterStationUnit.GetUpgradeCost());
                Debug.Log("WaterStationUnit���A�b�v�O���[�h����܂����B");
            }
        }
        else if (Mahojin != null && stageManager.offeringPoints >= Mahojin.GetUpgradeCost())
        {
            if (Mahojin.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-Mahojin.GetUpgradeCost());
                Debug.Log("Mahojin���A�b�v�O���[�h����܂����B");
            }
        }
        else
        {
            Debug.Log("�A�b�v�O���[�h�Ώۂ̃��j�b�g��������Ȃ����A�܂��͂��ΑK�|�C���g���s�����Ă��܂��B");
        }
    }

    // �ʏ�̃��j�b�g��ݒu
    void PlaceUnit()
    {
        if (selectedUnitIndex < 0 || selectedUnitIndex >= unitPrefabs.Count)
        {
            Debug.LogWarning("�����ȃ��j�b�g�C���f�b�N�X���I������Ă��܂��B");
            return;
        }

        GameObject unitPrefab = unitPrefabs[selectedUnitIndex];
        DefenseUnit defenseUnit = unitPrefab.GetComponent<DefenseUnit>();
        FenceUnit fenceUnit = unitPrefab.GetComponent<FenceUnit>();
        CaltropUnit caltropUnit = unitPrefab.GetComponent<CaltropUnit>();
        SpikeUnit spikeUnit = unitPrefab.GetComponent<SpikeUnit>();
        SlowUnit1 slowUnit1 = unitPrefab.GetComponent<SlowUnit1>();
        SlowUnit2 slowUnit2 = unitPrefab.GetComponent<SlowUnit2>();
        SlowUnit3 slowUnit3 = unitPrefab.GetComponent<SlowUnit3>();
        FlameTrapUnit flameTrapUnit = unitPrefab.GetComponent<FlameTrapUnit>();
        BearTrapUnit bearTrapUnit = unitPrefab.GetComponent<BearTrapUnit>();
        ShrineUnit shrineUnit = unitPrefab.GetComponent<ShrineUnit>(); // ShrineUnit���`�F�b�N
        WaterStationUnit waterStationUnit = unitPrefab.GetComponent<WaterStationUnit>(); // WaterStationUnit�̃`�F�b�N
        Mahojin Mahojin = unitPrefab.GetComponent<Mahojin>();//mahojinunit�̃`�F�b�N

        float unitCost = 0f;

        if (defenseUnit != null)
        {
            unitCost = defenseUnit.upgradeCosts[0];
        }
        else if (fenceUnit != null)
        {
            unitCost = fenceUnit.upgradeCosts[0];
        }
        else if (caltropUnit != null)
        {
            unitCost = caltropUnit.upgradeCosts[0];
        }
        else if (spikeUnit != null)
        {
            unitCost = spikeUnit.upgradeCosts[0];
        }
        else if (slowUnit1 != null)
        {
            unitCost = slowUnit1.upgradeCosts[0];
        }
        else if (slowUnit2 != null)
        {
            unitCost = slowUnit2.upgradeCosts[0];
        }
        else if (slowUnit3 != null)
        {
            unitCost = slowUnit3.upgradeCosts[0];
        }
        else if (flameTrapUnit != null)
        {
            unitCost = flameTrapUnit.upgradeCosts[0];
        }
        else if (bearTrapUnit != null)
        {
            unitCost = bearTrapUnit.upgradeCosts[0];
        }
        else if (shrineUnit != null)
        {
            unitCost = shrineUnit.upgradeCosts[0];
        }
        else if (waterStationUnit != null)
        {
            unitCost = waterStationUnit.upgradeCosts[0];
        }
        else if (Mahojin != null)
        {
            unitCost = Mahojin.upgradeCosts[0];
        }

        Vector3 forwardPosition = transform.position + transform.forward * 2f;
        Vector3 gridPosition = gridSystem.GetGridPosition(forwardPosition);

        if (stageManager.offeringPoints >= unitCost && gridSystem.CanPlaceUnit(gridPosition))
        {
            gridSystem.PlaceUnit(gridPosition, unitPrefab);
            stageManager.AddOfferingPoints(-unitCost);
            PlaySound(placementSound);
        }
        else
        {
            Debug.Log("���ΑK�|�C���g���s�����Ă��܂��B�܂��̓��j�b�g��z�u�ł��܂���B");
        }
    }


    // �v���C���[�̐��ʂ̃��j�b�g���폜
    void RemoveUnit()
    {
        Vector3 forwardPosition = transform.position + transform.forward * 2f;
        Vector3 gridPosition = gridSystem.GetGridPosition(forwardPosition);

        // GridSystem��RemoveUnit���\�b�h���Ăяo���ă��j�b�g���폜
        gridSystem.RemoveUnit(gridPosition);

        PlaySound(removalSound); // ���j�b�g�폜���̌��ʉ�
        Debug.Log("���j�b�g���폜����܂����B");
    }

    // ���ʉ��Đ�
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); // ���ʉ����Đ�
        }
    }

    void HandleUnitSelection()
    {
        int totalUnits = unitPrefabs.Count;

        // 1�L�[�܂���LB�{�^���őO�̃��j�b�g�ɐ؂�ւ���
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            selectedUnitIndex--;
            if (selectedUnitIndex < 0)
            {
                selectedUnitIndex = totalUnits - 1; // ���Ƀ��[�v
            }
            UpdateSlotUI(); // UI�X�V
        }
        // 2�L�[�܂���RB�{�^���Ŏ��̃��j�b�g�ɐ؂�ւ���
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            selectedUnitIndex++;
            if (selectedUnitIndex >= totalUnits)
            {
                selectedUnitIndex = 0; // �O�Ƀ��[�v
            }
            UpdateSlotUI(); // UI�X�V
        }

        Debug.Log("�I�𒆂̃��j�b�g: " + selectedUnitIndex);
    }

    // �X���b�gUI�̍X�V���Ăяo��
    void UpdateSlotUI()
    {
        StageUIManager stageUIManager = FindObjectOfType<StageUIManager>();
        if (stageUIManager != null)
        {
            stageUIManager.UpdateSelectedUnit(selectedUnitIndex);
        }
    }

    // �v���C���[�̐��ʂɃ|�C���^���X�V���郁�\�b�h
    void UpdatePointerPosition()
    {
        Vector3 forwardPosition = transform.position + transform.forward * 2f;
        Vector3 gridPosition = gridSystem.GetGridPosition(forwardPosition);

        if (gridSystem.CanPlaceUnit(gridPosition) || gridSystem.IsOccupied(gridPosition))
        {
            placementPointer.SetActive(true);
            placementPointer.transform.position = gridPosition + new Vector3(0, 0.1f, 0);
            canPlaceUnit = true;
        }
        else
        {
            placementPointer.SetActive(false);
            canPlaceUnit = false;
        }
    }
}
