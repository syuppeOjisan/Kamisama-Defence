using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineUnit : MonoBehaviour
{
    public float[] pointAdditionIntervals = new float[5]; // 各レベルのポイント追加頻度
    public float[] pointsToAdd = new float[5]; // 各レベルで追加するポイント数
    public float[] upgradeCosts = new float[5]; // 各レベルのアップグレードコスト
    private int currentLevel = 0; // 初期レベルは0 (1Lv)

    private StageManager stageManager;
    private float timer; // ポイント追加のためのタイマー
    private UIManager uiManager; // UI管理用

    void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
        if (stageManager == null)
        {
            Debug.LogError("StageManagerが見つかりません。お賽銭ポイントを追加できません。");
        }
        timer = pointAdditionIntervals[currentLevel]; // 初期タイマー設定

        // UIManagerの取得
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetShrineUnitPlacement(); // 設置時の立ち絵とセリフを設定
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            AddOfferingPoints();
            timer = pointAdditionIntervals[currentLevel]; // タイマーをリセット
        }
    }

    // 所持お賽銭ポイントにポイントを追加
    void AddOfferingPoints()
    {
        if (stageManager != null)
        {
            stageManager.AddOfferingPoints(pointsToAdd[currentLevel]);
            Debug.Log("ShrineUnitがポイントを追加しました: " + pointsToAdd[currentLevel]);
        }
    }

    // ユニットのアップグレードメソッド
    public bool UpgradeUnit()
    {
        if (currentLevel < 4) // 最大レベルに達していない場合
        {
            currentLevel++;
            if (uiManager != null)
            {
                uiManager.SetShrineUnitUpgraded(); // アップグレード時の立ち絵とセリフを設定
            }
            Debug.Log("ShrineUnitがレベルアップしました: Lv" + (currentLevel + 1));
            return true;
        }
        else
        {
            Debug.Log("ShrineUnitは既に最大レベルです。");
            return false;
        }
    }

    // 現在のユニットレベルに応じたアップグレードコストを取得
    public float GetUpgradeCost()
    {
        if (currentLevel <= 4)
        {
            return upgradeCosts[currentLevel];
        }
        return 0f; // 最大レベルの場合、アップグレードコストはゼロ
    }
}