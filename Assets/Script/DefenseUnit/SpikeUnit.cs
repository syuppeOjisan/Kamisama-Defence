using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeUnit : MonoBehaviour
{
    public float[] damage = new float[5];        // �e���x���̃_���[�W��
    public float[] effectiveRange = new float[5]; // �e���x���̌��ʔ͈�
    public float[] upgradeCosts = new float[5];  // �e���x���̃A�b�v�O���[�h�R�X�g
    public AudioClip hitSound;                   // �G���͈͂ɐG�ꂽ�Ƃ��̌��ʉ�
    public AudioClip upgradeSound;               // �A�b�v�O���[�h���̌��ʉ�

    private HashSet<Enemy> enemiesInRange = new HashSet<Enemy>(); // �_���[�W��^�����G���Ǘ�
    private int currentLevel = 1; // �������x����1�ɐݒ�
    private AudioSource audioSource; // ���ʉ��Đ��p
    private SphereCollider triggerCollider; // ���ʔ͈͂̂��߂̃R���C�_�[
    private Animator animator; // �A�j���[�V��������p
    private UIManager uiManager; // UI�Ǘ��p

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // ���ʔ͈͂̂��߂̃g���K�[��ݒ�
        triggerCollider = gameObject.AddComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        UpdateEffectRange();

        // Animator�R���|�[�l���g�̎擾
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("SpikeIdle"); // �A�j���[�V��������K�X�ύX
        }

        // UIManager�̎擾
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetSpikeUnitPlacement(); // �ݒu���̗����G�ƃZ���t��ݒ�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                // �_���[�W��^���A���ʉ����Đ�
                enemy.TakeDamage(damage[currentLevel - 1]);
                PlayHitSound();
                enemiesInRange.Add(enemy); // �_���[�W���󂯂��G�����X�g�ɒǉ�
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy); // �͈͂���o���G�����Z�b�g
            }
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }

    // ���x���A�b�v����
    public bool UpgradeUnit()
    {
        if (currentLevel < 5) // �ő僌�x����5�ɌŒ�
        {
            currentLevel++;
            UpdateEffectRange(); // ���ʔ͈͂��X�V
            PlayUpgradeSound();

            // UIManager�ŃA�b�v�O���[�h���̗����G�ƃZ���t��ݒ�
            if (uiManager != null)
            {
                uiManager.SetSpikeUnitUpgraded();
            }

            Debug.Log("SpikeUnit�����x���A�b�v���܂���: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("SpikeUnit�͊��ɍő僌�x���ł��B");
            return false;
        }
    }

    void UpdateEffectRange()
    {
        if (triggerCollider != null)
        {
            triggerCollider.radius = effectiveRange[currentLevel - 1];
        }
    }

    // �A�b�v�O���[�h�R�X�g���擾
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
        Gizmos.DrawWireSphere(transform.position, effectiveRange[currentLevel - 1]);
    }

    public void AddAttackBuff(float buffAmount)
    {
        for (int i = 0; i < damage.Length; i++)
        {
            damage[i] += buffAmount;
        }
    }

    public void RemoveAttackBuff(float buffAmount)
    {
        for (int i = 0; i < damage.Length; i++)
        {
            damage[i] -= buffAmount;
        }
    }
}