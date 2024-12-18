using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleUnit : MonoBehaviour
{
    [Header("�C���X�y�N�^�[�ݒ荀��")]
    public float[] effectRange = new float[5];      // �e���x���̌��ʔ͈�
    public float[] cooldownTimes = new float[5];    // �e���x���̃C���^�[�o������
    public float[] upgradeCosts = new float[5];     // �e���x���̃A�b�v�O���[�h�R�X�g
    public AudioClip warpSound;                     // ���[�v���̌��ʉ�
    public AudioClip upgradeSound;                  // �A�b�v�O���[�h���̌��ʉ�

    private int currentLevel = 1;                   // �������x��
    private AudioSource audioSource;
    private SphereCollider triggerCollider;         // ���ʔ͈͗p�̃R���C�_�[
    private HashSet<Enemy> affectedEnemies = new HashSet<Enemy>(); // ���[�v�Ώۂ̓G��ǐ�
    private UIManager uiManager;                    // UI�Ǘ��p

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
            uiManager.SetMagicCircleUnitPlacement(); // �ݒu���̗����G�ƃZ���t��ݒ�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ShootingEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !affectedEnemies.Contains(enemy))
            {
                StartCoroutine(WarpEnemy(enemy));
            }
        }
    }

    private IEnumerator WarpEnemy(Enemy enemy)
    {
        // �G�����[�v�����A�C���^�[�o�����͍ēx���ʂ�K�p���Ȃ�
        affectedEnemies.Add(enemy);

        // ���[�v����擾
        Transform warpPoint = GetRandomWarpPoint();
        if (warpPoint != null)
        {
            enemy.transform.position = warpPoint.position;
            PlayWarpSound();
        }

        // �C���^�[�o���ҋ@
        yield return new WaitForSeconds(cooldownTimes[currentLevel - 1]);

        affectedEnemies.Remove(enemy); // �ēx���[�v�\�ɂ���
    }

    private Transform GetRandomWarpPoint()
    {
        GameObject[] warpPoints = GameObject.FindGameObjectsWithTag("Warp");
        if (warpPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, warpPoints.Length);
            return warpPoints[randomIndex].transform;
        }

        Debug.LogWarning("Warp�^�O�����I�u�W�F�N�g��������܂���I");
        return null;
    }

    private void PlayWarpSound()
    {
        if (warpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(warpSound);
        }
    }

    private void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }

    public bool UpgradeUnit()
    {
        if (currentLevel < 5) // �ő僌�x����5
        {
            currentLevel++;
            UpdateEffectRange();
            PlayUpgradeSound();

            // UIManager�ŃA�b�v�O���[�h���̗����G�ƃZ���t��ݒ�
            if (uiManager != null)
            {
                uiManager.SetMagicCircleUnitUpgraded();
            }

            Debug.Log("MagicCircleUnit�����x���A�b�v���܂���: Lv" + currentLevel);
            return true;
        }
        else
        {
            Debug.Log("MagicCircleUnit�͊��ɍő僌�x���ł��B");
            return false;
        }
    }

    private void UpdateEffectRange()
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, effectRange[currentLevel - 1]);
    }
}