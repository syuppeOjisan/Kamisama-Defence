using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceUnit : MonoBehaviour
{
    public float[] timerLevels = { 10f, 20f, 30f, 40f, 50f }; // ���x�����Ƃ̃^�C�}�[
    public float[] upgradeCosts = { 10f, 20f, 30f, 40f, 50f }; // ���x�����Ƃ̃A�b�v�O���[�h�R�X�g
    public AudioClip upgradeSound; // �A�b�v�O���[�h���̌��ʉ�
    public AudioClip destructionSound; // ���Ŏ��̌��ʉ�

    private int currentLevel = 1; // ���݂̃��x��
    private float remainingTime; // ���݂̎c�莞��
    private AudioSource audioSource; // ���ʉ��p�̃I�[�f�B�I�\�[�X
    private bool isDestroyed = false; // ���j�b�g�����ɔj�󂳂�Ă��邩�ǂ������Ǘ�
    private UIManager uiManager; // UI�Ǘ��p

    void Start()
    {
        // �����^�C�}�[��ݒ�
        remainingTime = timerLevels[currentLevel - 1];
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource��ǉ�

        // UIManager�̎擾
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetFenceUnitPlacement(); // �ݒu���̃Z���t�Ɨ����G��ݒ�
        }
    }

    void Update()
    {
        // �^�C�}�[���J�E���g�_�E��
        remainingTime -= Time.deltaTime;

        // �^�C�}�[��0�ɂȂ��������
        if (remainingTime <= 0 && !isDestroyed) // ���łɏ��ł��Ă��Ȃ��ꍇ�̂ݏ���
        {
            DestroyFence(); // ���ŏ��������s
        }
    }

    // ���j�b�g�̃A�b�v�O���[�h����
    public bool UpgradeUnit()
    {
        if (currentLevel < 5) // ���x��5�܂�
        {
            currentLevel++;
            remainingTime += timerLevels[currentLevel - 1]; // �^�C�}�[�ɒǉ��b�������Z
            PlayUpgradeSound(); // �A�b�v�O���[�h���ʉ����Đ�

            // �A�b�v�O���[�h���̗����G�ƃZ���t��ݒ�
            if (uiManager != null)
            {
                uiManager.SetFenceUnitUpgraded();
            }

            return true;
        }
        else
        {
            Debug.Log("FenceUnit�͊��ɍő僌�x���ł��B");
            return false;
        }
    }

    // �A�b�v�O���[�h�ɕK�v�ȃR�X�g���擾
    public float GetUpgradeCost()
    {
        if (currentLevel <= 5) // ���x��5�܂ł̃R�X�g
        {
            return upgradeCosts[currentLevel - 1];
        }
        return 0f;
    }

    // �򃆃j�b�g�����ł����鏈��
    private void DestroyFence()
    {
        if (isDestroyed) return;

        isDestroyed = true; // ��d���ł�h��

        if (destructionSound != null)
        {
            audioSource.PlayOneShot(destructionSound); // ���Ō��ʉ�
        }

        Destroy(gameObject, destructionSound != null ? destructionSound.length : 0f); // ���Ō��ʉ��Đ���ɔj��
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null)
        {
            audioSource.PlayOneShot(upgradeSound); // �A�b�v�O���[�h���̌��ʉ�
        }
    }
}