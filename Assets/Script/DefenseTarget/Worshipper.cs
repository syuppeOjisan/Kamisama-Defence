using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worshipper : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;
    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    private float originalMoveSpeed;
    public AudioClip deathSound;
    public AudioClip offeringSound;
    public float offeringPoints = 10f;
    public float soundRange = 10f;

    private Transform shrineTarget; // 神社へのターゲット
    private Transform spawnPoint; // 初期スポーン地点
    private bool isReturning = false; // 神社到達後の戻りフラグ
    private bool isStopped = false;
    private AudioSource audioSource;
    private DefenseTarget shrine;
    private StageManager stageManager;
    private Renderer[] renderers;
    private NavMeshAgent navAgent;

    void Start()
    {
        currentHP = 0f;
        shrineTarget = GameObject.FindWithTag("DefenseTarget").transform;
        shrine = shrineTarget.GetComponent<DefenseTarget>();
        stageManager = FindObjectOfType<StageManager>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f;
        audioSource.maxDistance = soundRange;
        renderers = GetComponentsInChildren<Renderer>();

        navAgent = GetComponent<NavMeshAgent>();
        originalMoveSpeed = navAgent.speed = moveSpeed;

        // 初期は神社に向かって進む
        MoveTowardsShrine();
    }

    void Update()
    {
        CheckForEnemies();

        if (!isStopped && navAgent.remainingDistance < 0.1f)
        {
            if (isReturning)
            {
                // スポーン地点に到達したら消滅
                Destroy(gameObject);
            }
            else
            {
                // 神社に到達したらポイントを追加し、スポーンポイントへ向かう
                GiveOffering();
                PlayOfferingSound();
                MoveTowardsSpawnPoint();
            }
        }
    }

    // 神社に向かって移動
    void MoveTowardsShrine()
    {
        if (shrineTarget != null)
        {
            navAgent.SetDestination(shrineTarget.position);
            isReturning = false;
        }
    }

    // スポーンポイントに向かって移動
    void MoveTowardsSpawnPoint()
    {
        if (spawnPoint != null)
        {
            navAgent.SetDestination(spawnPoint.position);
            isReturning = true;
        }
    }

    void CheckForEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("ShootingEnemy"))
            {
                isStopped = true;
                navAgent.isStopped = true;
                return;
            }
        }
        isStopped = false;
        navAgent.isStopped = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DefenseTarget") && !isReturning)
        {
            GiveOffering();
            PlayOfferingSound();
            MoveTowardsSpawnPoint();
        }
    }

    // お賽銭ポイントをステージに追加
    void GiveOffering()
    {
        if (stageManager != null)
        {
            stageManager.AddOfferingPoints(offeringPoints);
        }
        else
        {
            Debug.LogError("StageManagerが見つかりません。お賽銭ポイントを追加できません。");
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP += damage;

        if (currentHP >= maxHP)
        {
            Die();
        }
        else
        {
            if (shrine != null)
            {
                shrine.TakeDamage(damage);
            }
        }
    }

    void Die()
    {
        PlayDeathSound();
        HideWorshipper();
        Destroy(gameObject, deathSound.length);
    }

    void HideWorshipper()
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    void PlayDeathSound()
    {
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    void PlayOfferingSound()
    {
        if (offeringSound != null)
        {
            audioSource.PlayOneShot(offeringSound);
        }
    }

    // 移動速度を増加させるメソッド
    public void IncreaseSpeed(float speedMultiplier)
    {
        navAgent.speed = originalMoveSpeed * (1f + speedMultiplier);
    }

    // 移動速度を元に戻すメソッド
    public void ResetSpeed()
    {
        navAgent.speed = originalMoveSpeed;
    }

    public void SetSpawnPoint(Transform spawnPointTransform)
    {
        spawnPoint = spawnPointTransform;
    }
}