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

    private Transform shrineTarget; // �_�Ђւ̃^�[�Q�b�g
    private Transform spawnPoint; // �����X�|�[���n�_
    private bool isReturning = false; // �_�Г��B��̖߂�t���O
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

        // �����͐_�ЂɌ������Đi��
        MoveTowardsShrine();
    }

    void Update()
    {
        CheckForEnemies();

        if (!isStopped && navAgent.remainingDistance < 0.1f)
        {
            if (isReturning)
            {
                // �X�|�[���n�_�ɓ��B���������
                Destroy(gameObject);
            }
            else
            {
                // �_�Ђɓ��B������|�C���g��ǉ����A�X�|�[���|�C���g�֌�����
                GiveOffering();
                PlayOfferingSound();
                MoveTowardsSpawnPoint();
            }
        }
    }

    // �_�ЂɌ������Ĉړ�
    void MoveTowardsShrine()
    {
        if (shrineTarget != null)
        {
            navAgent.SetDestination(shrineTarget.position);
            isReturning = false;
        }
    }

    // �X�|�[���|�C���g�Ɍ������Ĉړ�
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

    // ���ΑK�|�C���g���X�e�[�W�ɒǉ�
    void GiveOffering()
    {
        if (stageManager != null)
        {
            stageManager.AddOfferingPoints(offeringPoints);
        }
        else
        {
            Debug.LogError("StageManager��������܂���B���ΑK�|�C���g��ǉ��ł��܂���B");
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

    // �ړ����x�𑝉������郁�\�b�h
    public void IncreaseSpeed(float speedMultiplier)
    {
        navAgent.speed = originalMoveSpeed * (1f + speedMultiplier);
    }

    // �ړ����x�����ɖ߂����\�b�h
    public void ResetSpeed()
    {
        navAgent.speed = originalMoveSpeed;
    }

    public void SetSpawnPoint(Transform spawnPointTransform)
    {
        spawnPoint = spawnPointTransform;
    }
}