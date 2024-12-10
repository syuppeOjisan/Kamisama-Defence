using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowUnit2 : MonoBehaviour
{
    public float[] slowEffectRange = new float[5];  // �e���x���̌��ʔ͈�
    public float[] slowAmount = new float[5];       // �e���x���̈ړ����x�ቺ��
    public float[] upgradeCosts = new float[5];     // �e���x���̃A�b�v�O���[�h�R�X�g
    public AudioClip slowEffectSound;               // �G�����ʔ͈͂ɓ������Ƃ��̌��ʉ�
    public AudioClip upgradeSound;                  // �A�b�v�O���[�h���̌��ʉ�

    private int currentLevel = 1; // �������x����1�ɐݒ�
    private HashSet<Enemy> slowedEnemies = new HashSet<Enemy>(); // ��x���ʂ�^�����G��ǐ�
    private AudioSource audioSource; // ���ʉ��Đ��p��AudioSource
    private SphereCollider triggerCollider; // ���ʔ͈͗p�̃R���C�_�[
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
            uiManager.SetSlowUnit2Placement(); // �ݒu���̗����G�ƃZ���t��ݒ�
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplySlow(slowAmount[currentLevel - 1], 1f); // 1�b�Ԃ̒ቺ����K�p
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplySlow(slowAmount[currentLevel - 1], 1f); // ���ʔ͈͂ɂ���Ԃ����ƒቺ����K�p
                if (!slowedEnemies.Contains(enemy))
                {
                    slowedEnemies.Add(enemy);
                    PlaySlowEffectSound();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && slowedEnemies.Contains(enemy))
            {
                slowedEnemies.Remove(enemy); // ���ʔ͈͂���o���G�����X�g����폜
            }
        }
    }

    void PlaySlowEffectSound()
    {
        if (slowEffectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(slowEffectSound); // �ڐG���Ɉ�x�������ʉ����Đ�
        }
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound); // �A�b�v�O���[�h���̌��ʉ����Đ�
        }
    }

    // ���x���A�b�v����
    public bool UpgradeUnit()
    {
        if (currentLevel < 5) // �ő僌�x���ɒB���Ă��Ȃ��ꍇ
        {
            currentLevel++;
            UpdateEffectRange();
            PlayUpgradeSound();

            // UIManager�ŃA�b�v�O���[�h���̗����G�ƃZ���t��ݒ�
            if (uiManager != null)
            {
                uiManager.SetSlowUnit2Upgraded();
            }

            Debug.Log("SlowUnit�����x���A�b�v���܂���: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("SlowUnit�͊��ɍő僌�x���ł��B");
            return false;
        }
    }

    void UpdateEffectRange()
    {
        if (triggerCollider != null)
        {
            triggerCollider.radius = slowEffectRange[currentLevel - 1];
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, slowEffectRange[currentLevel - 1]);
    }
}