using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaltropUnit : MonoBehaviour
{
    public float[] damagePerLevel = new float[5]; // �e���x���̃_���[�W��
    public float[] effectRadiusPerLevel = new float[5]; // �e���x���̌��ʔ͈͂̔��a
    public float[] slowAmountPerLevel = new float[5]; // �e���x���̈ړ����x�ቺ��
    public float[] upgradeCosts = new float[5]; // �e���x���̃A�b�v�O���[�h�R�X�g
    public float damageInterval = 0.5f; // �_���[�W��^����Ԋu
    public AudioClip upgradeSound; // �A�b�v�O���[�h���̌��ʉ�

    private List<Enemy> enemiesInRange = new List<Enemy>(); // ���ʔ͈͓��̓G�̃��X�g
    private int currentLevel = 0; // �������x����0 (1Lv)
    private AudioSource audioSource; // ���ʉ��Đ��p��AudioSource
    private UIManager uiManager; // UI�Ǘ��p

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        StartCoroutine(ApplyEffects());

        // UIManager�̎擾
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetCaltropUnitPlacement(); // �ݒu���̗����G�ƃZ���t��ݒ�
        }
    }

    void Update()
    {
        // ���ʔ͈͓��̓G�����o
        Collider[] hits = Physics.OverlapSphere(transform.position, effectRadiusPerLevel[currentLevel]);
        enemiesInRange.Clear(); // ���X�g���N���A���čĎ擾

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("ShootingEnemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemiesInRange.Add(enemy);
                }
            }
        }
    }

    IEnumerator ApplyEffects()
    {
        while (true)
        {
            foreach (Enemy enemy in enemiesInRange)
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(damagePerLevel[currentLevel]);
                    enemy.ApplySlow(slowAmountPerLevel[currentLevel], damageInterval); // �X���[���J��Ԃ��K�p
                }
            }
            yield return new WaitForSeconds(damageInterval);
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
        if (currentLevel < 4) // �ő僌�x���ɒB���Ă��Ȃ��ꍇ
        {
            currentLevel++;
            PlayUpgradeSound(); // ���x���A�b�v���Ɍ��ʉ����Đ�

            // UIManager�ŃA�b�v�O���[�h���̗����G�ƃZ���t��ݒ�
            if (uiManager != null)
            {
                uiManager.SetCaltropUnitUpgraded();
            }

            Debug.Log("CaltropUnit�����x���A�b�v���܂���: Lv" + (currentLevel + 1));
            return true;
        }
        else
        {
            Debug.Log("CaltropUnit�͊��ɍő僌�x���ł��B");
            return false; // ����ȏ�̃��x���A�b�v�͕s��
        }
    }

    public float GetUpgradeCost()
    {
        if (currentLevel <= 4)
        {
            return upgradeCosts[currentLevel]; // ���̃��x���̃R�X�g��Ԃ�
        }
        return 0f; // �ő僌�x���̏ꍇ�A�A�b�v�O���[�h�R�X�g�̓[��
    }

    void OnDrawGizmosSelected()
    {
        // ���ʔ͈͂����o�I�ɕ\��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, effectRadiusPerLevel[currentLevel]);
    }
}