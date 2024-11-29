using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStationUnit : MonoBehaviour
{
    public float[] speedIncreaseAmounts = new float[5]; // ���x�����Ƃ̈ړ����x�㏸��
    public float[] effectiveRanges = new float[5]; // ���x�����Ƃ̌��ʔ͈�
    public float[] upgradeCosts = new float[5]; // ���x�����Ƃ̃A�b�v�O���[�h�R�X�g
    public AudioClip upgradeSound; // �A�b�v�O���[�h���̌��ʉ�

    private int currentLevel = 1; // �������x����1
    private AudioSource audioSource;
    private UIManager uiManager; // UI�Ǘ��p

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateEffectRange();

        // UIManager�̎擾
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetWaterStationUnitPlacement(); // �ݒu���̗����G�ƃZ���t��ݒ�
        }
    }

    void UpdateEffectRange()
    {
        SphereCollider rangeCollider = GetComponent<SphereCollider>();
        if (rangeCollider == null)
        {
            rangeCollider = gameObject.AddComponent<SphereCollider>();
        }
        rangeCollider.isTrigger = true;
        rangeCollider.radius = effectiveRanges[currentLevel - 1];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worshipper"))
        {
            Worshipper worshipper = other.GetComponent<Worshipper>();
            if (worshipper != null)
            {
                worshipper.IncreaseSpeed(speedIncreaseAmounts[currentLevel - 1]);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Worshipper"))
        {
            Worshipper worshipper = other.GetComponent<Worshipper>();
            if (worshipper != null)
            {
                worshipper.ResetSpeed();
            }
        }
    }

    public bool UpgradeUnit()
    {
        if (currentLevel < 5)
        {
            currentLevel++;
            PlayUpgradeSound();
            UpdateEffectRange();

            // UIManager�ŃA�b�v�O���[�h���̗����G�ƃZ���t��ݒ�
            if (uiManager != null)
            {
                uiManager.SetWaterStationUnitUpgraded();
            }

            return true;
        }
        else
        {
            Debug.Log("WaterStationUnit�͊��ɍő僌�x���ł��B");
            return false;
        }
    }

    public float GetUpgradeCost()
    {
        if (currentLevel < 5)
        {
            return upgradeCosts[currentLevel - 1];
        }
        return 0f;
    }

    private void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }
}