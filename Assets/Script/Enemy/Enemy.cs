using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    public float damage = 10f;
    public float detectionRange = 15f; // 検知範囲
    public float attackRange = 5f; // 攻撃範囲
    public float attackDelay = 1.5f; // 攻撃の遅延時間
    private float lastAttackTime;

    public AudioClip deathSound;

    public Image hpBarFill;
    public Image hpBarBackground;

    private NavMeshAgent agent;
    private AudioSource audioSource;
    private Transform shrineTarget;
    private StageManager stageManager;

    private float originalMoveSpeed;
    private bool isSlowed = false;
    private float slowDuration = 0f;

    private bool isStunned = false;
    private float stunEndTime = 0f;

    private Transform currentTarget; // 現在の追跡対象

    void Start()
    {
        currentHP = 0f;
        agent = GetComponent<NavMeshAgent>();

        shrineTarget = GameObject.FindWithTag("DefenseTarget").transform;
        currentTarget = shrineTarget;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        originalMoveSpeed = agent.speed;

        if (hpBarFill != null && hpBarBackground != null)
        {
            hpBarFill.enabled = false;
            hpBarBackground.enabled = false;
        }

        agent.SetDestination(currentTarget.position);

        stageManager = FindObjectOfType<StageManager>();
        if (stageManager == null)
        {
            Debug.LogError("StageManagerがシーン内に存在しません。");
        }
    }

    void Update()
    {
        if (isStunned)
        {
            if (Time.time >= stunEndTime)
            {
                Unstun();
            }
            return;
        }

        if (isSlowed)
        {
            slowDuration -= Time.deltaTime;
            if (slowDuration <= 0f)
            {
                ResetSpeed();
            }
        }

        DetectClosestTarget();

        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);

            // 攻撃範囲に入っているか、攻撃ディレイが経過しているかチェック
            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange && Time.time >= lastAttackTime + attackDelay)
            {
                AttackTarget();
                lastAttackTime = Time.time; // 攻撃タイマーをリセット
            }
        }
    }

    // 最も近い対象を検出して追跡対象を更新
    void DetectClosestTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        Transform closestTarget = shrineTarget;
        float closestDistance = Vector3.Distance(transform.position, shrineTarget.position);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Worshipper"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestTarget = hit.transform;
                    closestDistance = distance;
                }
            }
        }

        currentTarget = closestTarget;
    }

    void AttackTarget()
    {
        if (currentTarget.CompareTag("Worshipper"))
        {
            Worshipper worshipper = currentTarget.GetComponent<Worshipper>();
            if (worshipper != null)
            {
                worshipper.TakeDamage(damage);
            }
        }
        else if (currentTarget.CompareTag("DefenseTarget"))
        {
            DefenseTarget defenseTarget = currentTarget.GetComponent<DefenseTarget>();
            if (defenseTarget != null)
            {
                defenseTarget.TakeDamage(damage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.GetDamage());
                Destroy(other.gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP += damage;

        if (!hpBarFill.enabled && !hpBarBackground.enabled)
        {
            hpBarFill.enabled = true;
            hpBarBackground.enabled = true;
        }

        UpdateHPBar();

        if (currentHP >= maxHP)
        {
            Die();
        }
    }

    void UpdateHPBar()
    {
        if (hpBarFill != null)
        {
            float fillAmount = 1 - (currentHP / maxHP);
            hpBarFill.fillAmount = fillAmount;
        }
    }

    void Die()
    {
        PlayDeathSound();

        if (stageManager != null)
        {
            stageManager.EnemyDefeated();
        }

        Destroy(gameObject, 0.1f);
    }

    void PlayDeathSound()
    {
        if (deathSound != null)
        {
            GameObject soundObject = new GameObject("TemporaryAudio");
            AudioSource tempAudioSource = soundObject.AddComponent<AudioSource>();
            tempAudioSource.clip = deathSound;
            tempAudioSource.Play();

            Destroy(soundObject, deathSound.length);
        }
    }

    public void ApplySlow(float slowAmount, float duration)
    {
        agent.speed = originalMoveSpeed * (1f - slowAmount);
        slowDuration = Mathf.Max(slowDuration, duration); // 効果時間を延長
        isSlowed = true;
    }

    public void ApplyStun(float duration)
    {
        if (!isStunned)
        {
            isStunned = true;
            agent.isStopped = true;
            stunEndTime = Time.time + duration;
        }
    }

    private void Unstun()
    {
        isStunned = false;
        agent.isStopped = false;
    }

    private void ResetSpeed()
    {
        agent.speed = originalMoveSpeed;
        isSlowed = false;
    }
}