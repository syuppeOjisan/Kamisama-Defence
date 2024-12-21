using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UnitUpgradeManager : MonoBehaviour
{
    public TMP_Text unitInfoText; // UIにユニット情報を表示するTextMeshPro

    public Button upgradeButton;
    public TMP_Text feedbackText; // フィードバック用のテキスト
    public TMP_Text faithPointsText; // 信仰ポイントの表示用テキスト
    public GameObject targetPrefab;

    [Header("Audio Settings")]
    public AudioClip successSound; // 強化成功時の効果音
    public AudioClip failureSound; // 強化失敗時の効果音
    private AudioSource audioSource;

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

    [Header("MagicCircleUnit 強化設定")]
    public float magicCircleRangeIncrease = 0.5f;   // 効果範囲の増加量
    public float magicCircleCooldownReduction = 0.5f; // インターバル時間の短縮量
    public float magicCircleCostIncrease = 10f;    // コストの増加量
    public string magicCircleUnitUpgradeMessage = "魔法陣が強化されました！";

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

    [Header("共通設定")]
    public int upgradeCost = 50; // 信仰ポイントの消費量

    // ユニットボタンとそのプレハブを設定
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
        // AudioSource の初期化
        audioSource = gameObject.AddComponent<AudioSource>();

        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeUnit);
        }

        if (feedbackText != null)
        {
            feedbackText.text = ""; // 初期状態では空にする
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
        

        // 信仰ポイントのUIを初期化
        UpdateFaithPointsUI();
    }

    // ユニット情報を表示する
    public void DisplayUnitInfo(GameObject unitPrefab)
    {
        if (unitPrefab == null || unitInfoText == null) return;

        string info = "ユニット情報\n";

        // 各ユニットタイプに対応
        if (unitPrefab.TryGetComponent(out DefenseUnit defenseUnit))
        {
            //灯篭ユニット
            info += "範囲内の敵に対して弾を発射する";
            info += "\n";
            info += $"ダメージ: {defenseUnit.damages[0].ToString("F2")}\n";
            info += $"連射速度: {defenseUnit.fireRates[0].ToString("F2")}/秒\n";
            info += $"弾速: {defenseUnit.projectileSpeeds[0].ToString("F2")}\n";
            info += $"射程距離: {defenseUnit.ranges[0].ToString("F2")}\n";
            info += $"コスト: {defenseUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト20\n・ダメージ＋0.05\n・連射速度＋0.02\n・弾速＋0.02\n・射程距離＋0.05";

        }
        else if (unitPrefab.TryGetComponent(out SpikeUnit spikeUnit))
        {
            //スパイクユニット
            info += "効果範囲に触れた敵に一度だけダメージを与える";
            info += "\n";
            info += $"ダメージ: {spikeUnit.damage[0].ToString("F2")}\n";
            info += $"効果範囲: {spikeUnit.effectiveRange[0].ToString("F2")}\n";
            info += $"コスト: {spikeUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト30\n・ダメージ＋0.1\n・効果範囲＋0.05";
        }
        else if (unitPrefab.TryGetComponent(out CaltropUnit caltropUnit))
        {
            //まきびしユニット
            info += "効果範囲内の敵の移動速度を下げて継続ダメージを与える";
            info += "\n";
            info += $"ダメージ: {caltropUnit.damagePerLevel[0].ToString("F2")}\n";
            info += $"移動速度低下率: {caltropUnit.slowAmountPerLevel[0].ToString("F2")}倍\n";
            info += $"効果範囲: {caltropUnit.effectRadiusPerLevel[0].ToString("F2")}\n";
            info += $"コスト: {caltropUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト20\n・ダメージ＋0.01\n・効果範囲＋0.05";
        }
        else if (unitPrefab.TryGetComponent(out FlameTrapUnit flameTrapUnit))
        {
            //間欠泉ユニット
            info += "熱湯によるダメージとやけどによる継続ダメージを与える";
            info += "\n";
            info += $"初撃ダメージ: {flameTrapUnit.initialDamage[0].ToString("F2")}\n";
            info += $"継続ダメージ: {flameTrapUnit.sustainedDamage[0].ToString("F2")}\n";
            info += $"継続時間: {flameTrapUnit.damageDuration[0].ToString("F2")}\n";
            info += $"効果範囲: {flameTrapUnit.effectRange[0].ToString("F2")}\n";
            info += $"コスト: {flameTrapUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト40\n・初撃ダメージ＋0.05\n・継続ダメージ＋0.05\n・継続時間＋0.02\n・効果範囲＋0.05";
        }
        else if (unitPrefab.TryGetComponent(out BearTrapUnit bearTrapUnit))
        {
            //トラバサミユニット
            info += "ダメージを与えて最大1体までを一定時間敵を拘束する。";
            info += "\n";
            info += $"ダメージ: {bearTrapUnit.damage[0].ToString("F2")}\n";
            info += $"拘束時間: {bearTrapUnit.stunDuration[0].ToString("F2")}秒\n";
            info += $"クールダウン: {bearTrapUnit.cooldownTime[0].ToString("F2")}秒\n";
            info += $"効果範囲: {bearTrapUnit.effectiveRange[0].ToString("F2")}\n";
            info += $"コスト: {bearTrapUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト40\n・ダメージ＋0.2\n・拘束時間＋0.05\n・効果範囲＋0.03\n";
        }
        else if (unitPrefab.TryGetComponent(out FenceUnit fenceUnit))
        {
            //竹柵ユニット
            info += "設置後しばらくすると勝手に壊れる足止め用のユニット";
            info += "\n";
            info += $"{fenceUnit.timerLevels[0].ToString("F2")}秒後に自壊\n";
            info += $"コスト: {fenceUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト20\n・自壊まで＋0.03\n・コスト－0.01";
        }
        else if (unitPrefab.TryGetComponent(out SlowUnit1 slowUnit1))
        {
            //唐傘ユニット(slowunit1)
            info += "敵の移動速度を下げるユニット。盛り塩よりも効果範囲が大きい";
            info += "\n";
            info += $"移動速度低下率: {slowUnit1.slowAmount[0].ToString("F2")}\n";
            info += $"効果範囲: {slowUnit1.slowEffectRange[0].ToString("F2")}\n";
            info += $"コスト: {slowUnit1.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト20\n・効果範囲＋0.05";
        }
        else if (unitPrefab.TryGetComponent(out SlowUnit2 slowUnit2))
        {
            //盛り塩ユニット(slowunit2)
            info += "効果範囲は小さいが、唐傘よりも移動速度を下げ設置コストが安い";
            info += "\n";
            info += $"移動速度低下率: {slowUnit2.slowAmount[0].ToString("F2")}\n";
            info += $"効果範囲: {slowUnit2.slowEffectRange[0].ToString("F2")}\n";
            info += $"コスト: {slowUnit2.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト10\n・効果範囲＋0.01";
        }
        else if (unitPrefab.TryGetComponent(out SlowUnit3 slowUnit3))
        {
            //悪い盛り塩ユニット(slowunit3)
            info += "呪われてしまった盛り塩ユニット。敵の移動速度を上げてしまう";
            info += "\n";
            info += $"移動速度低下率: {slowUnit3.slowAmount[0].ToString("F2")}\n";
            info += $"効果範囲: {slowUnit3.slowEffectRange[0].ToString("F2")}\n";
            info += $"コスト: {slowUnit3.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト5\n・効果範囲＋0.05";
        }
        else if (unitPrefab.TryGetComponent(out MagicCircleUnit magicCircleUnit))
        {
            //魔法陣ユニット
            info += "効果範囲に入った敵をスポーン位置までワープさせる";
            info += "\n";
            info += $"クールダウン: {magicCircleUnit.cooldownTimes[0].ToString("F2")}秒\n";
            info += $"効果範囲: {magicCircleUnit.effectRange[0].ToString("F2")}\n";
            info += $"コスト: {magicCircleUnit.upgradeCosts[0]}\n";
            info += "\n";
            info += "アップグレードコスト60\n・クールダウン-0.02\n・効果範囲＋0.03";
        }
        else if (unitPrefab.TryGetComponent(out ShrineUnit shrineUnit))
        {
            //祠ユニット
            info += "設置すると、一定時間ごとにお賽銭がもらえる";
            info += "\n";
            info += $"追加のお賽銭: {shrineUnit.pointsToAdd[0].ToString("F2")}ポイント\n";
            info += $"{shrineUnit.pointAdditionIntervals[0].ToString("F2")}秒ごとに追加\n";
            info += $"コスト: {shrineUnit.upgradeCosts[0]}\n";
            info += "最大設置数:1\n";
            info += "\n";
            info += "アップグレードコスト100\n・お賽銭獲得までの秒数-0.001";
        }
        else if (unitPrefab.TryGetComponent(out WaterStationUnit waterStationUnit))
        {
            //井戸ユニット
            info += "効果範囲内の参拝客の移動速度を上げる";
            info += "\n";
            info += $"参拝客の移動速度: {waterStationUnit.speedIncreaseAmounts[0].ToString("F2")}倍\n";
            info += $"効果範囲: {waterStationUnit.effectiveRanges[0].ToString("F2")}\n";
            info += $"コスト: {waterStationUnit.upgradeCosts[0]}\n";
            info += "最大設置数:3\n";
            info += "\n";
            info += "アップグレードコスト70\n・効果範囲＋0.1";
        }
        else
        {
            info += "情報が設定されていません。";
        }

        // UIに反映
        unitInfoText.text = info;
    }

    // ボタンのイベント設定用メソッド
    public void SetupButton(Button button, GameObject unitPrefab)
    {
        if (button != null)
        {
            // ボタンのホバーイベント
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
        // 信仰ポイントが足りない場合
        if (FaithPointManager.Instance.GetTotalFaithPoints() < upgradeCost)
        {
            PlaySound(failureSound); // 強化失敗時の音を再生
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
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(defenseUnitUpgradeMessage); // 指定されたメッセージを表示
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // SpikeUnit の場合
        SpikeUnit spikeUnit = targetPrefab.GetComponent<SpikeUnit>();
        if (spikeUnit != null)
        {
            UpgradeSpikeUnit(spikeUnit);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(spikeUnitUpgradeMessage); // 指定されたメッセージを表示
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // CaltropUnit の場合
        CaltropUnit caltropUnit = targetPrefab.GetComponent<CaltropUnit>();
        if (caltropUnit != null)
        {
            UpgradeCaltropUnit(caltropUnit);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(caltropUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // FlameTrapUnit の場合
        FlameTrapUnit flameTrapUnit = targetPrefab.GetComponent<FlameTrapUnit>();
        if (flameTrapUnit != null)
        {
            UpgradeFlameTrapUnit(flameTrapUnit);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(flameTrapUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // BearTrapUnit の場合
        BearTrapUnit bearTrapUnit = targetPrefab.GetComponent<BearTrapUnit>();
        if (bearTrapUnit != null)
        {
            UpgradeBearTrapUnit(bearTrapUnit);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(bearTrapUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // FenceUnit の場合
        FenceUnit fenceUnit = targetPrefab.GetComponent<FenceUnit>();
        if (fenceUnit != null)
        {
            UpgradeFenceUnit(fenceUnit);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(fenceUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // SlowUnit1 の場合
        SlowUnit1 slowUnit1 = targetPrefab.GetComponent<SlowUnit1>();
        if (slowUnit1 != null)
        {
            UpgradeSlowUnit1(slowUnit1);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(slowUnit1UpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // SlowUnit2 の場合
        SlowUnit2 slowUnit2 = targetPrefab.GetComponent<SlowUnit2>();
        if (slowUnit2 != null)
        {
            UpgradeSlowUnit2(slowUnit2);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(slowUnit2UpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // SlowUnit3 の場合
        SlowUnit3 slowUnit3 = targetPrefab.GetComponent<SlowUnit3>();
        if (slowUnit3 != null)
        {
            UpgradeSlowUnit3(slowUnit3);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(slowUnit3UpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // ShrineUnit の場合
        ShrineUnit shrineUnit = targetPrefab.GetComponent<ShrineUnit>();
        if (shrineUnit != null)
        {
            UpgradeShrineUnit(shrineUnit);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(shrineUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // WaterStationUnit の場合
        WaterStationUnit waterStationUnit = targetPrefab.GetComponent<WaterStationUnit>();
        if (waterStationUnit != null)
        {
            UpgradeWaterStationUnit(waterStationUnit);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(waterStationUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // MagicCircleUnit の場合
        MagicCircleUnit magicCircleUnit = targetPrefab.GetComponent<MagicCircleUnit>();
        if (magicCircleUnit != null)
        {
            UpgradeMagicCircleUnit(magicCircleUnit);
            PlaySound(successSound); // 強化成功時の音を再生
            DisplayFeedback(magicCircleUnitUpgradeMessage);
            DisplayUnitInfo(targetPrefab); // 情報を更新
            return;
        }

        // 対応するユニットタイプが見つからない場合
        DisplayFeedback("指定されたユニットはサポートされていません！");
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