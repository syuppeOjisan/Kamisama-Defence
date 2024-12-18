using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleUnit : MonoBehaviour
{
    [Header("インスペクター設定項目")]
    public float[] effectRange = new float[5];      // 各レベルの効果範囲
    public float[] cooldownTimes = new float[5];    // 各レベルのインターバル時間
    public float[] upgradeCosts = new float[5];     // 各レベルのアップグレードコスト
    public AudioClip warpSound;                     // ワープ時の効果音
    public AudioClip upgradeSound;                  // アップグレード時の効果音

    private int currentLevel = 1;                   // 初期レベル
    private AudioSource audioSource;
    private SphereCollider triggerCollider;         // 効果範囲用のコライダー
    private HashSet<Enemy> affectedEnemies = new HashSet<Enemy>(); // ワープ対象の敵を追跡
    private UIManager uiManager;                    // UI管理用

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
            uiManager.SetMagicCircleUnitPlacement(); // 設置時の立ち絵とセリフを設定
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !affectedEnemies.Contains(enemy))
            {
                StartCoroutine(WarpEnemy(enemy));
            }
        }
    }

    private IEnumerator WarpEnemy(Enemy enemy)
    {
        // 敵をワープさせ、インターバル中は再度効果を適用しない
        affectedEnemies.Add(enemy);

        // ワープ先を取得
        Transform warpPoint = GetRandomWarpPoint();
        if (warpPoint != null)
        {
            enemy.transform.position = warpPoint.position;
            PlayWarpSound();
        }

        // インターバル待機
        yield return new WaitForSeconds(cooldownTimes[currentLevel - 1]);

        affectedEnemies.Remove(enemy); // 再度ワープ可能にする
    }

    private Transform GetRandomWarpPoint()
    {
        GameObject[] warpPoints = GameObject.FindGameObjectsWithTag("Warp");
        if (warpPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, warpPoints.Length);
            return warpPoints[randomIndex].transform;
        }

        Debug.LogWarning("Warpタグを持つオブジェクトが見つかりません！");
        return null;
    }

    private void PlayWarpSound()
    {
        if (warpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(warpSound);
        }
    }

    private void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }

    public bool UpgradeUnit()
    {
        if (currentLevel < 5) // 最大レベルは5
        {
            currentLevel++;
            UpdateEffectRange();
            PlayUpgradeSound();

            // UIManagerでアップグレード時の立ち絵とセリフを設定
            if (uiManager != null)
            {
                uiManager.SetMagicCircleUnitUpgraded();
            }

            Debug.Log("MagicCircleUnitがレベルアップしました: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("MagicCircleUnitは既に最大レベルです。");
            return false;
        }
    }

    private void UpdateEffectRange()
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, effectRange[currentLevel - 1]);
    }
}