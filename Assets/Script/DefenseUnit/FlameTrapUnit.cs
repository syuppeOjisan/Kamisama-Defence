using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrapUnit : MonoBehaviour
{
    public float[] effectRange = new float[5];          // 各レベルの効果範囲
    public float[] initialDamage = new float[5];        // 各レベルの初期ダメージ量
    public float[] sustainedDamage = new float[5];      // 各レベルの持続ダメージ量
    public float[] damageDuration = new float[5];       // 各レベルの持続時間
    public float[] upgradeCosts = new float[5];         // 各レベルのアップグレードコスト

    public AudioClip contactSound;                      // 接触時の効果音
    public AudioClip upgradeSound;                      // アップグレード時の効果音

    private int currentLevel = 1;                       // 初期レベルを1に設定
    private AudioSource audioSource;                    // 効果音再生用のAudioSource
    private SphereCollider triggerCollider;             // 効果範囲用のコライダー
    private HashSet<Enemy> affectedEnemies = new HashSet<Enemy>(); // 持続ダメージを追跡
    private UIManager uiManager;                        // UI管理用

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
            uiManager.SetFlameTrapUnitPlacement(); // 設置時の立ち絵とセリフを設定
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy")))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !affectedEnemies.Contains(enemy))
            {
                // 初回のみ初期ダメージと効果音
                enemy.TakeDamage(initialDamage[currentLevel - 1]);
                PlayContactSound();

                // 持続ダメージの開始
                affectedEnemies.Add(enemy);
                StartCoroutine(ApplySustainedDamage(enemy));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy")))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && affectedEnemies.Contains(enemy))
            {
                affectedEnemies.Remove(enemy); // 範囲から出た敵を追跡リストから削除
            }
        }
    }

    IEnumerator ApplySustainedDamage(Enemy enemy)
    {
        float elapsedTime = 0f;
        while (elapsedTime < damageDuration[currentLevel - 1] && affectedEnemies.Contains(enemy))
        {
            enemy.TakeDamage(sustainedDamage[currentLevel - 1]);
            yield return new WaitForSeconds(0.3f); // 持続ダメージ間隔を0.3秒ごとに設定
            elapsedTime += 0.3f;
        }

        affectedEnemies.Remove(enemy); // ダメージ完了後に削除
    }

    void PlayContactSound()
    {
        if (contactSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(contactSound); // 接触時の効果音再生
        }
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound); // アップグレード時の効果音再生
        }
    }

    // ユニットのレベルアップ処理
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
                uiManager.SetFlameTrapUnitUpgraded();
            }

            Debug.Log("FlameTrapUnitがレベルアップしました: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("FlameTrapUnitは既に最大レベルです。");
            return false;
        }
    }

    void UpdateEffectRange()
    {
        if (triggerCollider != null)
        {
            triggerCollider.radius = effectRange[currentLevel - 1];
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
        Gizmos.DrawWireSphere(transform.position, effectRange[currentLevel - 1]);
    }
}