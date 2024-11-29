using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapUnit : MonoBehaviour
{
    public float[] damage = new float[5];        // �e���x���̃_���[�W��
    public float[] stunDuration = new float[5];  // �e���x���̃X�^������
    public float[] cooldownTime = new float[5];  // �e���x���̃N�[���_�E������
    public float[] effectiveRange = new float[5]; // �e���x���̌��ʔ͈�
    public float[] upgradeCosts = new float[5];  // �e���x���̃A�b�v�O���[�h�R�X�g

    public AudioClip triggerSound;               // �g���K�[���������鎞�̌��ʉ�
    public AudioClip upgradeSound;               // �A�b�v�O���[�h���̌��ʉ�

    private int currentLevel = 1; // �������x����1�ɐݒ�
    private bool isOnCooldown = false;           // �N�[���_�E�������ǂ���
    private AudioSource audioSource;
    private SphereCollider triggerCollider;
    private UIManager uiManager; // UI�Ǘ��p

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // ���ʔ͈͗p�̃g���K�[��ݒ�
        triggerCollider = gameObject.AddComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        UpdateEffectRange();

        // UIManager�̎擾
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetBearTrapUnitPlacement(); // �ݒu���̗����G�ƃZ���t��ݒ�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOnCooldown && (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy")))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                ActivateTrap(enemy);
            }
        }
    }

    private void ActivateTrap(Enemy enemy)
    {
        // �_���[�W�ƃX�^�����ʂ�K�p
        enemy.TakeDamage(damage[currentLevel - 1]);
        enemy.ApplyStun(stunDuration[currentLevel - 1]);

        // �g���K�[�T�E���h�Đ�
        PlayTriggerSound();

        // �N�[���_�E���J�n
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime[currentLevel - 1]);
        isOnCooldown = false;
    }

    void PlayTriggerSound()
    {
        if (triggerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(triggerSound);
        }
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }

    public bool UpgradeUnit()
    {
        if (currentLevel < 5)
        {
            currentLevel++;
            UpdateEffectRange();
            PlayUpgradeSound();

            // UIManager�ŃA�b�v�O���[�h���̗����G�ƃZ���t��ݒ�
            if (uiManager != null)
            {
                uiManager.SetBearTrapUnitUpgraded();
            }

            Debug.Log("BearTrapUnit�����x���A�b�v���܂���: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("BearTrapUnit�͊��ɍő僌�x���ł��B");
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
}