using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ShootingEnemy : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;
    public float moveSpeed = 2f;
    public float range = 10f; // 射程距離
    public float fireRate = 1f; // 弾の発射速度
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float damage = 20f; // 弾のダメージ
    public AudioClip deathSound;

    public Image hpBarFill; // 緑のHPバー
    public Image hpBarBackground; // 赤のHPバー

    private Transform target; // 神社をターゲットに設定
    private NavMeshAgent agent; // NavMeshAgentの参照
    private float fireCooldown = 0f; // 射撃クールダウンタイマー
    private AudioSource audioSource;

    private float originalMoveSpeed; // 元の移動速度
    private bool isSlowed = false; // 移動速度が低下しているか
    private float slowDuration = 0f; // 移動速度低下の残り時間

    void Start()
    {
        currentHP = 0f; // HPの初期値
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgentを取得
        target = GameObject.FindWithTag("DefenseTarget").transform; // タグで神社をターゲットとして設定
        agent.SetDestination(target.position); // 神社までのルートを設定
        agent.speed = moveSpeed; // NavMeshAgentの速度を設定

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        originalMoveSpeed = moveSpeed; // 元の移動速度を保持

        // HPバーを初期状態で非表示に
        if (hpBarFill != null && hpBarBackground != null)
        {
            hpBarFill.enabled = false;
            hpBarBackground.enabled = false;
        }
    }

    void Update()
    {
        // 神社までの距離を計算
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= range)
        {
            // 射程内に入ったら移動を停止して攻撃
            agent.isStopped = true;
            AttackTarget();
        }
        else
        {
            // 射程外にいる場合は移動
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }

        // 移動速度低下の処理
        if (isSlowed)
        {
            slowDuration -= Time.deltaTime;
            if (slowDuration <= 0f)
            {
                ResetSpeed(); // 元の速度に戻す
            }
        }
    }

    // 移動速度低下の適用
    public void ApplySlow(float slowAmount, float duration)
    {
        if (!isSlowed)
        {
            agent.speed = originalMoveSpeed * (1f - slowAmount); // 速度を低下させる
        }
        slowDuration = duration; // 低下効果の持続時間を設定
        isSlowed = true;
    }

    // 移動速度を元に戻す
    private void ResetSpeed()
    {
        agent.speed = originalMoveSpeed;
        isSlowed = false;
    }

    // 神社への攻撃
    void AttackTarget()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate; // 次の射撃までのクールダウン
        }
    }

    // 弾を発射
    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();

        if (projScript != null)
        {
            projScript.Initialize(target, projectileSpeed, damage); // ターゲット、弾速、ダメージを設定
        }
    }

    // 弾の衝突によるダメージ処理
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.GetDamage());
                Destroy(other.gameObject); // 弾を破壊
            }
        }
    }

    // ダメージ処理
    public void TakeDamage(float damageAmount)
    {
        currentHP += damageAmount;

        // 初めてダメージを受けたときにHPバーを表示
        if (!hpBarFill.enabled && !hpBarBackground.enabled)
        {
            hpBarFill.enabled = true;
            hpBarBackground.enabled = true;
        }

        UpdateHPBar(); // HPバーを更新

        if (currentHP >= maxHP)
        {
            Die();
        }
    }

    void UpdateHPBar()
    {
        if (hpBarFill != null)
        {
            float fillAmount = 1 - (currentHP / maxHP); // ダメージに応じてバーを縮める
            hpBarFill.fillAmount = fillAmount;
        }
    }

    // 敵が倒されたときの処理
    void Die()
    {
        PlayDeathSound();
        Destroy(gameObject, 0.1f);
    }

    // 死亡時の効果音再生
    void PlayDeathSound()
    {
        if (deathSound != null)
        {
            GameObject soundObject = new GameObject("TemporaryAudio");
            AudioSource tempAudioSource = soundObject.AddComponent<AudioSource>();
            tempAudioSource.clip = deathSound;
            tempAudioSource.Play();

            Destroy(soundObject, deathSound.length); // 効果音再生後に破棄
        }
    }
}