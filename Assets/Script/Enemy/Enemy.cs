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
    public float detectionRange = 15f;
    public float attackRange = 5f;
    public float attackDelay = 1.5f;
    private float lastAttackTime;

    public AudioClip deathSound;
    public GameObject itemDropPrefab;
    public float dropChance = 0.3f;

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

    private Transform currentTarget;
    private bool isDead = false; // “G‚ª€–S‚µ‚½‚©‚Ç‚¤‚©‚ğŠÇ—‚·‚éƒtƒ‰ƒO

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
            Debug.LogError("StageManager‚ªƒV[ƒ““à‚É‘¶İ‚µ‚Ü‚¹‚ñB");
        }
    }

    void Update()
    {
        if (isDead) return; // €–SŒã‚Ìˆ—‚ğ’â~

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

            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange && Time.time >= lastAttackTime + attackDelay)
            {
                AttackTarget();
                lastAttackTime = Time.time;
            }
        }
    }

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
        if (isDead) return; // €–SÏ‚İ‚Ìê‡‚Íˆ—‚µ‚È‚¢

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
        if (isDead) return; // €–SÏ‚İ‚Ìê‡‚Íˆ—‚µ‚È‚¢

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
        if (isDead) return; // Šù‚É€–S‚µ‚Ä‚¢‚éê‡‚Íˆ—‚µ‚È‚¢
        isDead = true; // €–Sƒtƒ‰ƒO‚ğƒZƒbƒg

        PlayDeathSound();

        if (stageManager != null)
        {
            stageManager.EnemyDefeated();
        }

        DropItem(); // ƒAƒCƒeƒ€‚ğƒhƒƒbƒv
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

    void DropItem()
    {
        if (itemDropPrefab != null && Random.value <= dropChance)
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        }
    }

    public void ApplySlow(float slowAmount, float duration)
    {
        if (isDead) return; // €–SÏ‚İ‚Ìê‡‚Íˆ—‚µ‚È‚¢

        agent.speed = originalMoveSpeed * (1f - slowAmount);
        slowDuration = Mathf.Max(slowDuration, duration);
        isSlowed = true;
    }

    public void ApplyStun(float duration)
    {
        if (isDead) return; // €–SÏ‚İ‚Ìê‡‚Íˆ—‚µ‚È‚¢

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