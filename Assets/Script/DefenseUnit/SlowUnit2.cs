using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowUnit2 : MonoBehaviour
{
    public float[] slowEffectRange = new float[5];  // 各レベルの効果範囲
    public float[] slowAmount = new float[5];       // 各レベルの移動速度低下率
    public float[] upgradeCosts = new float[5];     // 各レベルのアップグレードコスト
    public AudioClip slowEffectSound;               // 敵が効果範囲に入ったときの効果音
    public AudioClip upgradeSound;                  // アップグレード時の効果音

    private int currentLevel = 1; // 初期レベルを1に設定
    private HashSet<Enemy> slowedEnemies = new HashSet<Enemy>(); // 一度効果を与えた敵を追跡
    private AudioSource audioSource; // 効果音再生用のAudioSource
    private SphereCollider triggerCollider; // 効果範囲用のコライダー
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
            uiManager.SetSlowUnit2Placement(); // 設置時の立ち絵とセリフを設定
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplySlow(slowAmount[currentLevel - 1], 1f); // 1秒間の低下率を適用
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplySlow(slowAmount[currentLevel - 1], 1f); // 効果範囲にいる間ずっと低下率を適用
                if (!slowedEnemies.Contains(enemy))
                {
                    slowedEnemies.Add(enemy);
                    PlaySlowEffectSound();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && slowedEnemies.Contains(enemy))
            {
                slowedEnemies.Remove(enemy); // 効果範囲から出た敵をリストから削除
            }
        }
    }

    void PlaySlowEffectSound()
    {
        if (slowEffectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(slowEffectSound); // 接触時に一度だけ効果音を再生
        }
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound); // アップグレード時の効果音を再生
        }
    }

    // レベルアップ処理
    public bool UpgradeUnit()
    {
        if (currentLevel < 5) // 最大レベルに達していない場合
        {
            currentLevel++;
            UpdateEffectRange();
            PlayUpgradeSound();

            // UIManagerでアップグレード時の立ち絵とセリフを設定
            if (uiManager != null)
            {
                uiManager.SetSlowUnit2Upgraded();
            }

            Debug.Log("SlowUnitがレベルアップしました: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("SlowUnitは既に最大レベルです。");
            return false;
        }
    }

    void UpdateEffectRange()
    {
        if (triggerCollider != null)
        {
            triggerCollider.radius = slowEffectRange[currentLevel - 1];
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, slowEffectRange[currentLevel - 1]);
    }
}