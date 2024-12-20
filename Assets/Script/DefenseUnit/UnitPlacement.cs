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

    private GridSystem gridSystem; // ステージ内のGridSystemオブジェクトを参照
    private GameObject placementPointer;
    private bool canPlaceUnit = true;
    private AudioSource audioSource;
    private StageManager stageManager;

    void Start()
    {
        // GridSystemをステージ内から検索
        gridSystem = FindObjectOfType<GridSystem>();
        if (gridSystem == null)
        {
            Debug.LogError("ステージ内にGridSystemオブジェクトが見つかりません。");
            return;
        }

        // ステージ番号に応じたCSVファイルを設定
        int currentStage = PlayerPrefs.GetInt("SelectedStage", 0);
        gridSystem.CsvFileName = $"unplaceableCells_stage{currentStage + 1}.csv"; // ステージ番号に応じたCSVファイル名
        gridSystem.InitializeGrid();

        placementPointer = Instantiate(placementPointerPrefab);
        placementPointer.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();

        stageManager = FindObjectOfType<StageManager>();
        if (stageManager == null)
        {
            Debug.LogError("StageManagerがシーン内に存在しません。");
        }

        // 装備画面で選んだユニットを反映
        if (UnitEquipManager.isEquipSelected)
        {
            unitPrefabs = new List<GameObject>();       // リスト初期化
            unitPrefabs = UnitEquipManager.equipUnits;  // 装備情報をコピー
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveUnit();
        }
    }

    // ユニットのアップグレード処理
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
        ShrineUnit shrineUnit = existingUnit.GetComponent<ShrineUnit>(); // ShrineUnitもチェック
        WaterStationUnit waterStationUnit = existingUnit.GetComponent<WaterStationUnit>(); // WaterStationUnitのチェック
        Mahojin Mahojin = existingUnit.GetComponent<Mahojin>();//mahojinunitのチェック
        if (defenseUnit != null && stageManager.offeringPoints >= defenseUnit.GetUpgradeCost())
        {
            if (defenseUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-defenseUnit.GetUpgradeCost());
                Debug.Log("DefenseUnitがアップグレードされました。");
            }
        }
        else if (fenceUnit != null && stageManager.offeringPoints >= fenceUnit.GetUpgradeCost())
        {
            if (fenceUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-fenceUnit.GetUpgradeCost());
                Debug.Log("FenceUnitがアップグレードされました。");
            }
        }
        else if (caltropUnit != null && stageManager.offeringPoints >= caltropUnit.GetUpgradeCost())
        {
            if (caltropUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-caltropUnit.GetUpgradeCost());
                Debug.Log("CaltropUnitがアップグレードされました。");
            }
        }
        else if (spikeUnit != null && stageManager.offeringPoints >= spikeUnit.GetUpgradeCost())
        {
            if (spikeUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-spikeUnit.GetUpgradeCost());
                Debug.Log("SpikeUnitがアップグレードされました。");
            }
        }
        else if (slowUnit1 != null && stageManager.offeringPoints >= slowUnit1.GetUpgradeCost())
        {
            if (slowUnit1.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-slowUnit1.GetUpgradeCost());
                Debug.Log("SlowUnit1がアップグレードされました。");
            }
        }
        else if (slowUnit2 != null && stageManager.offeringPoints >= slowUnit2.GetUpgradeCost())
        {
            if (slowUnit2.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-slowUnit2.GetUpgradeCost());
                Debug.Log("SlowUnit2がアップグレードされました。");
            }
        }
        else if (slowUnit3 != null && stageManager.offeringPoints >= slowUnit3.GetUpgradeCost())
        {
            if (slowUnit3.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-slowUnit3.GetUpgradeCost());
                Debug.Log("SlowUnit3がアップグレードされました。");
            }
        }
        else if (flameTrapUnit != null && stageManager.offeringPoints >= flameTrapUnit.GetUpgradeCost())
        {
            if (flameTrapUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-flameTrapUnit.GetUpgradeCost());
                Debug.Log("FlameTrapUnitがアップグレードされました。");
            }
        }
        else if (bearTrapUnit != null && stageManager.offeringPoints >= bearTrapUnit.GetUpgradeCost())
        {
            if (bearTrapUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-bearTrapUnit.GetUpgradeCost());
                Debug.Log("BearTrapUnitがアップグレードされました。");
            }
        }
        else if (shrineUnit != null && stageManager.offeringPoints >= shrineUnit.GetUpgradeCost())
        {
            if (shrineUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-shrineUnit.GetUpgradeCost());
                Debug.Log("ShrineUnitがアップグレードされました。");
            }
        }
        else if (waterStationUnit != null && stageManager.offeringPoints >= waterStationUnit.GetUpgradeCost())
        {
            if (waterStationUnit.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-waterStationUnit.GetUpgradeCost());
                Debug.Log("WaterStationUnitがアップグレードされました。");
            }
        }
        else if (Mahojin != null && stageManager.offeringPoints >= Mahojin.GetUpgradeCost())
        {
            if (Mahojin.UpgradeUnit())
            {
                stageManager.AddOfferingPoints(-Mahojin.GetUpgradeCost());
                Debug.Log("Mahojinがアップグレードされました。");
            }
        }
        else
        {
            Debug.Log("アップグレード対象のユニットが見つからないか、またはお賽銭ポイントが不足しています。");
        }
    }

    // 通常のユニットを設置
    void PlaceUnit()
    {
        if (selectedUnitIndex < 0 || selectedUnitIndex >= unitPrefabs.Count)
        {
            Debug.LogWarning("無効なユニットインデックスが選択されています。");
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
        ShrineUnit shrineUnit = unitPrefab.GetComponent<ShrineUnit>(); // ShrineUnitもチェック
        WaterStationUnit waterStationUnit = unitPrefab.GetComponent<WaterStationUnit>(); // WaterStationUnitのチェック
        Mahojin Mahojin = unitPrefab.GetComponent<Mahojin>();//mahojinunitのチェック

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
            Debug.Log("お賽銭ポイントが不足しています。またはユニットを配置できません。");
        }
    }


    // プレイヤーの正面のユニットを削除
    void RemoveUnit()
    {
        Vector3 forwardPosition = transform.position + transform.forward * 2f;
        Vector3 gridPosition = gridSystem.GetGridPosition(forwardPosition);

        // GridSystemのRemoveUnitメソッドを呼び出してユニットを削除
        gridSystem.RemoveUnit(gridPosition);

        PlaySound(removalSound); // ユニット削除時の効果音
        Debug.Log("ユニットが削除されました。");
    }

    // 効果音再生
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); // 効果音を再生
        }
    }

    void HandleUnitSelection()
    {
        int totalUnits = unitPrefabs.Count;

        // 1キーまたはLBボタンで前のユニットに切り替える
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            selectedUnitIndex--;
            if (selectedUnitIndex < 0)
            {
                selectedUnitIndex = totalUnits - 1; // 後ろにループ
            }
            UpdateSlotUI(); // UI更新
        }
        // 2キーまたはRBボタンで次のユニットに切り替える
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            selectedUnitIndex++;
            if (selectedUnitIndex >= totalUnits)
            {
                selectedUnitIndex = 0; // 前にループ
            }
            UpdateSlotUI(); // UI更新
        }

        Debug.Log("選択中のユニット: " + selectedUnitIndex);
    }

    // スロットUIの更新を呼び出す
    void UpdateSlotUI()
    {
        StageUIManager stageUIManager = FindObjectOfType<StageUIManager>();
        if (stageUIManager != null)
        {
            stageUIManager.UpdateSelectedUnit(selectedUnitIndex);
        }
    }

    // プレイヤーの正面にポインタを更新するメソッド
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
