using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaltropUnit : MonoBehaviour
{
    public float[] damagePerLevel = new float[5]; // 各レベルのダメージ量
    public float[] effectRadiusPerLevel = new float[5]; // 各レベルの効果範囲の半径
    public float[] slowAmountPerLevel = new float[5]; // 各レベルの移動速度低下率
    public float[] upgradeCosts = new float[5]; // 各レベルのアップグレードコスト
    public float damageInterval = 0.5f; // ダメージを与える間隔
    public AudioClip upgradeSound; // アップグレード時の効果音

    private List<Enemy> enemiesInRange = new List<Enemy>(); // 効果範囲内の敵のリスト
    private int currentLevel = 0; // 初期レベルは0 (1Lv)
    private AudioSource audioSource; // 効果音再生用のAudioSource
    private UIManager uiManager; // UI管理用

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        StartCoroutine(ApplyEffects());

        // UIManagerの取得
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetCaltropUnitPlacement(); // 設置時の立ち絵とセリフを設定
        }
    }

    void Update()
    {
        // 効果範囲内の敵を検出
        Collider[] hits = Physics.OverlapSphere(transform.position, effectRadiusPerLevel[currentLevel]);
        enemiesInRange.Clear(); // リストをクリアして再取得

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("ShootingEnemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemiesInRange.Add(enemy);
                }
            }
        }
    }

    IEnumerator ApplyEffects()
    {
        while (true)
        {
            foreach (Enemy enemy in enemiesInRange)
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(damagePerLevel[currentLevel]);
                    enemy.ApplySlow(slowAmountPerLevel[currentLevel], damageInterval); // スローを繰り返し適用
                }
            }
            yield return new WaitForSeconds(damageInterval);
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
        if (currentLevel < 4) // 最大レベルに達していない場合
        {
            currentLevel++;
            PlayUpgradeSound(); // レベルアップ時に効果音を再生

            // UIManagerでアップグレード時の立ち絵とセリフを設定
            if (uiManager != null)
            {
                uiManager.SetCaltropUnitUpgraded();
            }

            Debug.Log("CaltropUnitがレベルアップしました: Lv" + (currentLevel + 1));
            return true;
        }
        else
        {
            Debug.Log("CaltropUnitは既に最大レベルです。");
            return false; // これ以上のレベルアップは不可
        }
    }

    public float GetUpgradeCost()
    {
        if (currentLevel <= 4)
        {
            return upgradeCosts[currentLevel]; // 次のレベルのコストを返す
        }
        return 0f; // 最大レベルの場合、アップグレードコストはゼロ
    }

    void OnDrawGizmosSelected()
    {
        // 効果範囲を視覚的に表示
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, effectRadiusPerLevel[currentLevel]);
    }
}