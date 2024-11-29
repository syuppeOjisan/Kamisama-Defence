using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeUnit : MonoBehaviour
{
    public float[] damage = new float[5];        // 各レベルのダメージ量
    public float[] effectiveRange = new float[5]; // 各レベルの効果範囲
    public float[] upgradeCosts = new float[5];  // 各レベルのアップグレードコスト
    public AudioClip hitSound;                   // 敵が範囲に触れたときの効果音
    public AudioClip upgradeSound;               // アップグレード時の効果音

    private HashSet<Enemy> enemiesInRange = new HashSet<Enemy>(); // ダメージを与えた敵を管理
    private int currentLevel = 1; // 初期レベルを1に設定
    private AudioSource audioSource; // 効果音再生用
    private SphereCollider triggerCollider; // 効果範囲のためのコライダー
    private Animator animator; // アニメーション制御用
    private UIManager uiManager; // UI管理用

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // 効果範囲のためのトリガーを設定
        triggerCollider = gameObject.AddComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        UpdateEffectRange();

        // Animatorコンポーネントの取得
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("SpikeIdle"); // アニメーション名を適宜変更
        }

        // UIManagerの取得
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetSpikeUnitPlacement(); // 設置時の立ち絵とセリフを設定
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                // ダメージを与え、効果音を再生
                enemy.TakeDamage(damage[currentLevel - 1]);
                PlayHitSound();
                enemiesInRange.Add(enemy); // ダメージを受けた敵をリストに追加
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy); // 範囲から出た敵をリセット
            }
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }

    // レベルアップ処理
    public bool UpgradeUnit()
    {
        if (currentLevel < 5) // 最大レベルを5に固定
        {
            currentLevel++;
            UpdateEffectRange(); // 効果範囲を更新
            PlayUpgradeSound();

            // UIManagerでアップグレード時の立ち絵とセリフを設定
            if (uiManager != null)
            {
                uiManager.SetSpikeUnitUpgraded();
            }

            Debug.Log("SpikeUnitがレベルアップしました: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("SpikeUnitは既に最大レベルです。");
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

    // アップグレードコストを取得
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

    public void AddAttackBuff(float buffAmount)
    {
        for (int i = 0; i < damage.Length; i++)
        {
            damage[i] += buffAmount;
        }
    }

    public void RemoveAttackBuff(float buffAmount)
    {
        for (int i = 0; i < damage.Length; i++)
        {
            damage[i] -= buffAmount;
        }
    }
}