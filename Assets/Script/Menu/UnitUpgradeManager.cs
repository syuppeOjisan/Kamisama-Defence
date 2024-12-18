using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitUpgradeManager : MonoBehaviour
{
    public Button upgradeButton;
    public TMP_Text feedbackText; // フィードバック用のテキスト
    public TMP_Text faithPointsText; // 信仰ポイントの表示用テキスト
    public GameObject targetPrefab;

    [Header("DefenseUnit 強化設定")]
    public float fireRateIncrease = 0.1f;
    public float projectileSpeedIncrease = 1f;
    public float damageIncrease = 5f;
    public float rangeIncrease = 0.5f;
    public float costIncrease = 10f;
    public string defenseUnitUpgradeMessage = "灯篭が強化されました！"; // 手動設定可能なメッセージ

    [Header("SpikeUnit 強化設定")]
    public float spikeDamageIncrease = 10f;        // ダメージの増加量
    public float spikeRangeIncrease = 0.5f;       // 効果範囲の増加量
    public float spikeCostIncrease = 10f;         // コストの増加量
    public string spikeUnitUpgradeMessage = "スパイクが強化されました！"; // 手動設定可能なメッセージ

    [Header("CaltropUnit 強化設定")]
    public float caltropDamageIncrease = 2f;       // ダメージ増加
    public float caltropRangeIncrease = 0.3f;      // 効果範囲増加
    public float caltropSlowAmountIncrease = 0.05f;// 移動速度低下率増加
    public float caltropCostIncrease = 5f;         // コスト増加
    public string caltropUnitUpgradeMessage = "撒き菱が強化されました！";

    [Header("FlameTrapUnit 強化設定")]
    public float flameTrapRangeIncrease = 0.5f;          // 効果範囲増加
    public float flameTrapInitialDamageIncrease = 5f;    // 初撃ダメージ増加
    public float flameTrapSustainedDamageIncrease = 2f;  // 継続ダメージ増加
    public float flameTrapDurationIncrease = 1f;         // 持続時間増加
    public float flameTrapCostIncrease = 10f;            // コスト増加
    public string flameTrapUnitUpgradeMessage = "火炎罠が強化されました！";

    [Header("BearTrapUnit 強化設定")]
    public float bearTrapDamageIncrease = 5f;          // ダメージ増加
    public float bearTrapStunDurationIncrease = 1f;    // 拘束時間増加
    public float bearTrapCooldownReduction = 0.5f;     // クールダウン時間短縮
    public float bearTrapRangeIncrease = 0.5f;         // 効果範囲増加
    public float bearTrapCostIncrease = 10f;           // コスト増加
    public string bearTrapUnitUpgradeMessage = "トラバサミが強化されました！";

    [Header("FenceUnit 強化設定")]
    public float fenceUnitTimerIncrease = 5f;          // タイマー増加値
    public float fenceUnitCostIncrease = 10f;         // コスト増加値
    public string fenceUnitUpgradeMessage = "柵ユニットが強化されました！";

    [Header("SlowUnit1 強化設定")]
    public float slowUnit1EffectRangeIncrease = 0.5f;   // 効果範囲の増加量
    public float slowUnit1AmountIncrease = 0.1f;       // 移動速度低下量の増加値
    public float slowUnit1CostIncrease = 10f;          // コストの増加値
    public string slowUnit1UpgradeMessage = "唐傘が強化されました！";

    [Header("SlowUnit2 強化設定")]
    public float slowUnit2EffectRangeIncrease = 0.5f;   // 効果範囲の増加量
    public float slowUnit2AmountIncrease = 0.1f;       // 移動速度低下量の増加値
    public float slowUnit2CostIncrease = 10f;          // コストの増加値
    public string slowUnit2UpgradeMessage = "盛り塩が強化されました！";

    [Header("SlowUnit3 強化設定")]
    public float slowUnit3EffectRangeIncrease = 0.5f;   // 効果範囲の増加量
    public float slowUnit3AmountIncrease = 0.1f;       // 移動速度低下量の増加値
    public float slowUnit3CostIncrease = 10f;          // コストの増加値
    public string slowUnit3UpgradeMessage = "悪い盛り塩が強化されました！";

    [Header("ShrineUnit 強化設定")]
    public float shrineUnitIntervalReduction = 0.1f; // ポイント付与インターバルの短縮量
    public float shrineUnitPointIncrease = 5f;       // 付与ポイント量の増加値
    public float shrineUnitCostIncrease = 10f;       // コストの増加値
    public string shrineUnitUpgradeMessage = "祠が強化されました！";

    [Header("WaterStationUnit 強化設定")]
    public float waterStationRangeIncrease = 0.5f;      // 効果範囲の増加量
    public float waterStationSpeedIncrease = 0.1f;     // 移動速度上昇量の増加値
    public float waterStationCostIncrease = 10f;       // コストの増加値
    public string waterStationUpgradeMessage = "井戸が強化されました！";

    [Header("MagicCircleUnit 強化設定")]
    public float magicCircleRangeIncrease = 0.5f;   // 効果範囲の増加量
    public float magicCircleCooldownReduction = 0.5f; // インターバル時間の短縮量
    public float magicCircleCostIncrease = 10f;    // コストの増加量
    public string magicCircleUnitUpgradeMessage = "魔法陣が強化されました！";

    [Header("共通設定")]
    public int upgradeCost = 50; // 信仰ポイントの消費量

    private void Start()
    {
        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeUnit);
        }

        if (feedbackText != null)
        {
            feedbackText.text = ""; // 初期状態では空にする
        }

        // 信仰ポイントのUIを初期化
        UpdateFaithPointsUI();
    }

    public void UpgradeUnit()
    {
        // 信仰ポイントが足りない場合
        if (FaithPointManager.Instance.GetTotalFaithPoints() < upgradeCost)
        {
            DisplayFeedback("信仰ポイントが足りません！");
            return;
        }

        // 対象プレハブが設定されていない場合
        if (targetPrefab == null)
        {
            DisplayFeedback("強化対象のユニットが設定されていません！");
            return;
        }

        // DefenseUnit の場合
        DefenseUnit defenseUnit = targetPrefab.GetComponent<DefenseUnit>();
        if (defenseUnit != null)
        {
            UpgradeDefenseUnit(defenseUnit);
            DisplayFeedback(defenseUnitUpgradeMessage); // 指定されたメッセージを表示
            return;
        }

        // SpikeUnit の場合
        SpikeUnit spikeUnit = targetPrefab.GetComponent<SpikeUnit>();
        if (spikeUnit != null)
        {
            UpgradeSpikeUnit(spikeUnit);
            DisplayFeedback(spikeUnitUpgradeMessage); // 指定されたメッセージを表示
            return;
        }

        // CaltropUnit の場合
        CaltropUnit caltropUnit = targetPrefab.GetComponent<CaltropUnit>();
        if (caltropUnit != null)
        {
            UpgradeCaltropUnit(caltropUnit);
            DisplayFeedback(caltropUnitUpgradeMessage);
            return;
        }

        // FlameTrapUnit の場合
        FlameTrapUnit flameTrapUnit = targetPrefab.GetComponent<FlameTrapUnit>();
        if (flameTrapUnit != null)
        {
            UpgradeFlameTrapUnit(flameTrapUnit);
            DisplayFeedback(flameTrapUnitUpgradeMessage);
            return;
        }

        // BearTrapUnit の場合
        BearTrapUnit bearTrapUnit = targetPrefab.GetComponent<BearTrapUnit>();
        if (bearTrapUnit != null)
        {
            UpgradeBearTrapUnit(bearTrapUnit);
            DisplayFeedback(bearTrapUnitUpgradeMessage);
            return;
        }

        // FenceUnit の場合
        FenceUnit fenceUnit = targetPrefab.GetComponent<FenceUnit>();
        if (fenceUnit != null)
        {
            UpgradeFenceUnit(fenceUnit);
            DisplayFeedback(fenceUnitUpgradeMessage);
            return;
        }

        // SlowUnit1 の場合
        SlowUnit1 slowUnit1 = targetPrefab.GetComponent<SlowUnit1>();
        if (slowUnit1 != null)
        {
            UpgradeSlowUnit1(slowUnit1);
            DisplayFeedback(slowUnit1UpgradeMessage);
            return;
        }

        // SlowUnit2 の場合
        SlowUnit2 slowUnit2 = targetPrefab.GetComponent<SlowUnit2>();
        if (slowUnit2 != null)
        {
            UpgradeSlowUnit2(slowUnit2);
            DisplayFeedback(slowUnit2UpgradeMessage);
            return;
        }

        // SlowUnit3 の場合
        SlowUnit3 slowUnit3 = targetPrefab.GetComponent<SlowUnit3>();
        if (slowUnit3 != null)
        {
            UpgradeSlowUnit3(slowUnit3);
            DisplayFeedback(slowUnit3UpgradeMessage);
            return;
        }

        // ShrineUnit の場合
        ShrineUnit shrineUnit = targetPrefab.GetComponent<ShrineUnit>();
        if (shrineUnit != null)
        {
            UpgradeShrineUnit(shrineUnit);
            DisplayFeedback(shrineUnitUpgradeMessage);
            return;
        }

        // WaterStationUnit の場合
        WaterStationUnit waterStationUnit = targetPrefab.GetComponent<WaterStationUnit>();
        if (waterStationUnit != null)
        {
            UpgradeWaterStationUnit(waterStationUnit);
            DisplayFeedback(waterStationUpgradeMessage);
            return;
        }

        // MagicCircleUnit の場合
        MagicCircleUnit magicCircleUnit = targetPrefab.GetComponent<MagicCircleUnit>();
        if (magicCircleUnit != null)
        {
            UpgradeMagicCircleUnit(magicCircleUnit);
            DisplayFeedback(magicCircleUnitUpgradeMessage);
            return;
        }

        // 対応するユニットタイプが見つからない場合
        DisplayFeedback("指定されたユニットはサポートされていません！");
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

            // クールダウン時間が負になるのを防ぐ
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
            shrineUnit.pointAdditionIntervals[i] = Mathf.Max(0.1f, shrineUnit.pointAdditionIntervals[i] - shrineUnitIntervalReduction); // インターバルを短縮（最低値0.1秒）
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

    // MagicCircleUnit の強化処理
    private void UpgradeMagicCircleUnit(MagicCircleUnit magicCircleUnit)
    {
        for (int i = 0; i < magicCircleUnit.effectRange.Length; i++)
        {
            magicCircleUnit.effectRange[i] += magicCircleRangeIncrease;
            magicCircleUnit.cooldownTimes[i] = Mathf.Max(0.1f, magicCircleUnit.cooldownTimes[i] - magicCircleCooldownReduction); // クールダウン時間を短縮（最低値0.1秒）
            magicCircleUnit.upgradeCosts[i] += magicCircleCostIncrease;
        }

        ApplyUpgrade();
    }

    private void ApplyUpgrade()
    {
        // 信仰ポイントを消費
        FaithPointManager.Instance.DeductFaithPoints(upgradeCost);

        // 信仰ポイントUIを更新
        UpdateFaithPointsUI();
    }

    private void DisplayFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message; // メッセージを設定
            feedbackText.color = Color.green;
            CancelInvoke(nameof(ClearFeedback));
            Invoke(nameof(ClearFeedback), 2f); // 2秒後にフィードバックをクリア
        }
    }

    private void ClearFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.text = ""; // フィードバックをクリア
        }
    }

    private void UpdateFaithPointsUI()
    {
        if (faithPointsText != null)
        {
            faithPointsText.text = $"所持信仰ポイント: {FaithPointManager.Instance.GetTotalFaithPoints()}";
        }
    }
}