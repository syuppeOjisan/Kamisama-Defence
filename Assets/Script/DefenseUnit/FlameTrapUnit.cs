using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrapUnit : MonoBehaviour
{
    public float[] effectRange = new float[5];          // �e���x���̌��ʔ͈�
    public float[] initialDamage = new float[5];        // �e���x���̏����_���[�W��
    public float[] sustainedDamage = new float[5];      // �e���x���̎����_���[�W��
    public float[] damageDuration = new float[5];       // �e���x���̎�������
    public float[] upgradeCosts = new float[5];         // �e���x���̃A�b�v�O���[�h�R�X�g

    public AudioClip contactSound;                      // �ڐG���̌��ʉ�
    public AudioClip upgradeSound;                      // �A�b�v�O���[�h���̌��ʉ�

    private int currentLevel = 1;                       // �������x����1�ɐݒ�
    private AudioSource audioSource;                    // ���ʉ��Đ��p��AudioSource
    private SphereCollider triggerCollider;             // ���ʔ͈͗p�̃R���C�_�[
    private HashSet<Enemy> affectedEnemies = new HashSet<Enemy>(); // �����_���[�W��ǐ�
    private UIManager uiManager;                        // UI�Ǘ��p

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
            uiManager.SetFlameTrapUnitPlacement(); // �ݒu���̗����G�ƃZ���t��ݒ�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy")))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !affectedEnemies.Contains(enemy))
            {
                // ����̂ݏ����_���[�W�ƌ��ʉ�
                enemy.TakeDamage(initialDamage[currentLevel - 1]);
                PlayContactSound();

                // �����_���[�W�̊J�n
                affectedEnemies.Add(enemy);
                StartCoroutine(ApplySustainedDamage(enemy));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy")))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && affectedEnemies.Contains(enemy))
            {
                affectedEnemies.Remove(enemy); // �͈͂���o���G��ǐՃ��X�g����폜
            }
        }
    }

    IEnumerator ApplySustainedDamage(Enemy enemy)
    {
        float elapsedTime = 0f;
        while (elapsedTime < damageDuration[currentLevel - 1] && affectedEnemies.Contains(enemy))
        {
            enemy.TakeDamage(sustainedDamage[currentLevel - 1]);
            yield return new WaitForSeconds(0.3f); // �����_���[�W�Ԋu��0.3�b���Ƃɐݒ�
            elapsedTime += 0.3f;
        }

        affectedEnemies.Remove(enemy); // �_���[�W������ɍ폜
    }

    void PlayContactSound()
    {
        if (contactSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(contactSound); // �ڐG���̌��ʉ��Đ�
        }
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound); // �A�b�v�O���[�h���̌��ʉ��Đ�
        }
    }

    // ���j�b�g�̃��x���A�b�v����
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
                uiManager.SetFlameTrapUnitUpgraded();
            }

            Debug.Log("FlameTrapUnit�����x���A�b�v���܂���: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("FlameTrapUnit�͊��ɍő僌�x���ł��B");
            return false;
        }
    }

    void UpdateEffectRange()
    {
        if (triggerCollider != null)
        {
            triggerCollider.radius = effectRange[currentLevel - 1];
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
        Gizmos.DrawWireSphere(transform.position, effectRange[currentLevel - 1]);
    }
}