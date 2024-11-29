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
    public float range = 10f; // �˒�����
    public float fireRate = 1f; // �e�̔��ˑ��x
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float damage = 20f; // �e�̃_���[�W
    public AudioClip deathSound;

    public Image hpBarFill; // �΂�HP�o�[
    public Image hpBarBackground; // �Ԃ�HP�o�[

    private Transform target; // �_�Ђ��^�[�Q�b�g�ɐݒ�
    private NavMeshAgent agent; // NavMeshAgent�̎Q��
    private float fireCooldown = 0f; // �ˌ��N�[���_�E���^�C�}�[
    private AudioSource audioSource;

    private float originalMoveSpeed; // ���̈ړ����x
    private bool isSlowed = false; // �ړ����x���ቺ���Ă��邩
    private float slowDuration = 0f; // �ړ����x�ቺ�̎c�莞��

    void Start()
    {
        currentHP = 0f; // HP�̏����l
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgent���擾
        target = GameObject.FindWithTag("DefenseTarget").transform; // �^�O�Ő_�Ђ��^�[�Q�b�g�Ƃ��Đݒ�
        agent.SetDestination(target.position); // �_�Ђ܂ł̃��[�g��ݒ�
        agent.speed = moveSpeed; // NavMeshAgent�̑��x��ݒ�

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        originalMoveSpeed = moveSpeed; // ���̈ړ����x��ێ�

        // HP�o�[��������ԂŔ�\����
        if (hpBarFill != null && hpBarBackground != null)
        {
            hpBarFill.enabled = false;
            hpBarBackground.enabled = false;
        }
    }

    void Update()
    {
        // �_�Ђ܂ł̋������v�Z
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= range)
        {
            // �˒����ɓ�������ړ����~���čU��
            agent.isStopped = true;
            AttackTarget();
        }
        else
        {
            // �˒��O�ɂ���ꍇ�͈ړ�
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }

        // �ړ����x�ቺ�̏���
        if (isSlowed)
        {
            slowDuration -= Time.deltaTime;
            if (slowDuration <= 0f)
            {
                ResetSpeed(); // ���̑��x�ɖ߂�
            }
        }
    }

    // �ړ����x�ቺ�̓K�p
    public void ApplySlow(float slowAmount, float duration)
    {
        if (!isSlowed)
        {
            agent.speed = originalMoveSpeed * (1f - slowAmount); // ���x��ቺ������
        }
        slowDuration = duration; // �ቺ���ʂ̎������Ԃ�ݒ�
        isSlowed = true;
    }

    // �ړ����x�����ɖ߂�
    private void ResetSpeed()
    {
        agent.speed = originalMoveSpeed;
        isSlowed = false;
    }

    // �_�Ђւ̍U��
    void AttackTarget()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate; // ���̎ˌ��܂ł̃N�[���_�E��
        }
    }

    // �e�𔭎�
    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();

        if (projScript != null)
        {
            projScript.Initialize(target, projectileSpeed, damage); // �^�[�Q�b�g�A�e���A�_���[�W��ݒ�
        }
    }

    // �e�̏Փ˂ɂ��_���[�W����
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.GetDamage());
                Destroy(other.gameObject); // �e��j��
            }
        }
    }

    // �_���[�W����
    public void TakeDamage(float damageAmount)
    {
        currentHP += damageAmount;

        // ���߂ă_���[�W���󂯂��Ƃ���HP�o�[��\��
        if (!hpBarFill.enabled && !hpBarBackground.enabled)
        {
            hpBarFill.enabled = true;
            hpBarBackground.enabled = true;
        }

        UpdateHPBar(); // HP�o�[���X�V

        if (currentHP >= maxHP)
        {
            Die();
        }
    }

    void UpdateHPBar()
    {
        if (hpBarFill != null)
        {
            float fillAmount = 1 - (currentHP / maxHP); // �_���[�W�ɉ����ăo�[���k�߂�
            hpBarFill.fillAmount = fillAmount;
        }
    }

    // �G���|���ꂽ�Ƃ��̏���
    void Die()
    {
        PlayDeathSound();
        Destroy(gameObject, 0.1f);
    }

    // ���S���̌��ʉ��Đ�
    void PlayDeathSound()
    {
        if (deathSound != null)
        {
            GameObject soundObject = new GameObject("TemporaryAudio");
            AudioSource tempAudioSource = soundObject.AddComponent<AudioSource>();
            tempAudioSource.clip = deathSound;
            tempAudioSource.Play();

            Destroy(soundObject, deathSound.length); // ���ʉ��Đ���ɔj��
        }
    }
}