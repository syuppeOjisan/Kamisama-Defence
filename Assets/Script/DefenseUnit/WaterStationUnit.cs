using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStationUnit : MonoBehaviour
{
    public float[] speedIncreaseAmounts = new float[5]; // レベルごとの移動速度上昇量
    public float[] effectiveRanges = new float[5]; // レベルごとの効果範囲
    public float[] upgradeCosts = new float[5]; // レベルごとのアップグレードコスト
    public AudioClip upgradeSound; // アップグレード時の効果音

    private int currentLevel = 1; // 初期レベルは1
    private AudioSource audioSource;
    private UIManager uiManager; // UI管理用

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateEffectRange();

        // UIManagerの取得
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetWaterStationUnitPlacement(); // 設置時の立ち絵とセリフを設定
        }
    }

    void UpdateEffectRange()
    {
        SphereCollider rangeCollider = GetComponent<SphereCollider>();
        if (rangeCollider == null)
        {
            rangeCollider = gameObject.AddComponent<SphereCollider>();
        }
        rangeCollider.isTrigger = true;
        rangeCollider.radius = effectiveRanges[currentLevel - 1];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worshipper"))
        {
            Worshipper worshipper = other.GetComponent<Worshipper>();
            if (worshipper != null)
            {
                worshipper.IncreaseSpeed(speedIncreaseAmounts[currentLevel - 1]);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Worshipper"))
        {
            Worshipper worshipper = other.GetComponent<Worshipper>();
            if (worshipper != null)
            {
                worshipper.ResetSpeed();
            }
        }
    }

    public bool UpgradeUnit()
    {
        if (currentLevel < 5)
        {
            currentLevel++;
            PlayUpgradeSound();
            UpdateEffectRange();

            // UIManagerでアップグレード時の立ち絵とセリフを設定
            if (uiManager != null)
            {
                uiManager.SetWaterStationUnitUpgraded();
            }

            return true;
        }
        else
        {
            Debug.Log("WaterStationUnitは既に最大レベルです。");
            return false;
        }
    }

    public float GetUpgradeCost()
    {
        if (currentLevel < 5)
        {
            return upgradeCosts[currentLevel - 1];
        }
        return 0f;
    }

    private void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }
}