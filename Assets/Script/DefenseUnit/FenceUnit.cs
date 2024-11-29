using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceUnit : MonoBehaviour
{
    public float[] timerLevels = { 10f, 20f, 30f, 40f, 50f }; // レベルごとのタイマー
    public float[] upgradeCosts = { 10f, 20f, 30f, 40f, 50f }; // レベルごとのアップグレードコスト
    public AudioClip upgradeSound; // アップグレード時の効果音
    public AudioClip destructionSound; // 消滅時の効果音

    private int currentLevel = 1; // 現在のレベル
    private float remainingTime; // 現在の残り時間
    private AudioSource audioSource; // 効果音用のオーディオソース
    private bool isDestroyed = false; // ユニットが既に破壊されているかどうかを管理
    private UIManager uiManager; // UI管理用

    void Start()
    {
        // 初期タイマーを設定
        remainingTime = timerLevels[currentLevel - 1];
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceを追加

        // UIManagerの取得
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetFenceUnitPlacement(); // 設置時のセリフと立ち絵を設定
        }
    }

    void Update()
    {
        // タイマーをカウントダウン
        remainingTime -= Time.deltaTime;

        // タイマーが0になったら消滅
        if (remainingTime <= 0 && !isDestroyed) // すでに消滅していない場合のみ処理
        {
            DestroyFence(); // 消滅処理を実行
        }
    }

    // ユニットのアップグレード処理
    public bool UpgradeUnit()
    {
        if (currentLevel < 5) // レベル5まで
        {
            currentLevel++;
            remainingTime += timerLevels[currentLevel - 1]; // タイマーに追加秒数を加算
            PlayUpgradeSound(); // アップグレード効果音を再生

            // アップグレード時の立ち絵とセリフを設定
            if (uiManager != null)
            {
                uiManager.SetFenceUnitUpgraded();
            }

            return true;
        }
        else
        {
            Debug.Log("FenceUnitは既に最大レベルです。");
            return false;
        }
    }

    // アップグレードに必要なコストを取得
    public float GetUpgradeCost()
    {
        if (currentLevel <= 5) // レベル5までのコスト
        {
            return upgradeCosts[currentLevel - 1];
        }
        return 0f;
    }

    // 柵ユニットを消滅させる処理
    private void DestroyFence()
    {
        if (isDestroyed) return;

        isDestroyed = true; // 二重消滅を防ぐ

        if (destructionSound != null)
        {
            audioSource.PlayOneShot(destructionSound); // 消滅効果音
        }

        Destroy(gameObject, destructionSound != null ? destructionSound.length : 0f); // 消滅効果音再生後に破壊
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null)
        {
            audioSource.PlayOneShot(upgradeSound); // アップグレード時の効果音
        }
    }
}