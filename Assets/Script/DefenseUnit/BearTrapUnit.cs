using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapUnit : MonoBehaviour
{
    public float[] damage = new float[5];        // 各レベルのダメージ量
    public float[] stunDuration = new float[5];  // 各レベルのスタン時間
    public float[] cooldownTime = new float[5];  // 各レベルのクールダウン時間
    public float[] effectiveRange = new float[5]; // 各レベルの効果範囲
    public float[] upgradeCosts = new float[5];  // 各レベルのアップグレードコスト

    public AudioClip triggerSound;               // トリガーが発動する時の効果音
    public AudioClip upgradeSound;               // アップグレード時の効果音

    private int currentLevel = 1; // 初期レベルを1に設定
    private bool isOnCooldown = false;           // クールダウン中かどうか
    private AudioSource audioSource;
    private SphereCollider triggerCollider;
    private UIManager uiManager; // UI管理用

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // 効果範囲用のトリガーを設定
        triggerCollider = gameObject.AddComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        UpdateEffectRange();

        // UIManagerの取得
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetBearTrapUnitPlacement(); // 設置時の立ち絵とセリフを設定
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOnCooldown && (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy")))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                ActivateTrap(enemy);
            }
        }
    }

    private void ActivateTrap(Enemy enemy)
    {
        // ダメージとスタン効果を適用
        enemy.TakeDamage(damage[currentLevel - 1]);
        enemy.ApplyStun(stunDuration[currentLevel - 1]);

        // トリガーサウンド再生
        PlayTriggerSound();

        // クールダウン開始
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime[currentLevel - 1]);
        isOnCooldown = false;
    }

    void PlayTriggerSound()
    {
        if (triggerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(triggerSound);
        }
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }

    public bool UpgradeUnit()
    {
        if (currentLevel < 5)
        {
            currentLevel++;
            UpdateEffectRange();
            PlayUpgradeSound();

            // UIManagerでアップグレード時の立ち絵とセリフを設定
            if (uiManager != null)
            {
                uiManager.SetBearTrapUnitUpgraded();
            }

            Debug.Log("BearTrapUnitがレベルアップしました: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("BearTrapUnitは既に最大レベルです。");
            return false;
        }
    }

    void UpdateEffectRange()
    {
        if (triggerCollider != null)
        {
            triggerCollider.radius = effectiveRange[currentLevel - 1];
        }
    }

    public float GetUpgradeCost()
    {
        if (currentLevel <= 5)
        {
            return upgradeCosts[currentLevel - 1];
        }
        return 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, effectiveRange[currentLevel - 1]);
    }
}