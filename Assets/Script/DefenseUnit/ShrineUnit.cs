using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineUnit : MonoBehaviour
{
    public float[] pointAdditionIntervals = new float[5]; // �e���x���̃|�C���g�ǉ��p�x
    public float[] pointsToAdd = new float[5]; // �e���x���Œǉ�����|�C���g��
    public float[] upgradeCosts = new float[5]; // �e���x���̃A�b�v�O���[�h�R�X�g
    private int currentLevel = 0; // �������x����0 (1Lv)

    private StageManager stageManager;
    private float timer; // �|�C���g�ǉ��̂��߂̃^�C�}�[
    private UIManager uiManager; // UI�Ǘ��p

    void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
        if (stageManager == null)
        {
            Debug.LogError("StageManager��������܂���B���ΑK�|�C���g��ǉ��ł��܂���B");
        }
        timer = pointAdditionIntervals[currentLevel]; // �����^�C�}�[�ݒ�

        // UIManager�̎擾
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetShrineUnitPlacement(); // �ݒu���̗����G�ƃZ���t��ݒ�
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            AddOfferingPoints();
            timer = pointAdditionIntervals[currentLevel]; // �^�C�}�[�����Z�b�g
        }
    }

    // �������ΑK�|�C���g�Ƀ|�C���g��ǉ�
    void AddOfferingPoints()
    {
        if (stageManager != null)
        {
            stageManager.AddOfferingPoints(pointsToAdd[currentLevel]);
            Debug.Log("ShrineUnit���|�C���g��ǉ����܂���: " + pointsToAdd[currentLevel]);
        }
    }

    // ���j�b�g�̃A�b�v�O���[�h���\�b�h
    public bool UpgradeUnit()
    {
        if (currentLevel < 4) // �ő僌�x���ɒB���Ă��Ȃ��ꍇ
        {
            currentLevel++;
            if (uiManager != null)
            {
                uiManager.SetShrineUnitUpgraded(); // �A�b�v�O���[�h���̗����G�ƃZ���t��ݒ�
            }
            Debug.Log("ShrineUnit�����x���A�b�v���܂���: Lv" + (currentLevel + 1));
            return true;
        }
        else
        {
            Debug.Log("ShrineUnit�͊��ɍő僌�x���ł��B");
            return false;
        }
    }

    // ���݂̃��j�b�g���x���ɉ������A�b�v�O���[�h�R�X�g���擾
    public float GetUpgradeCost()
    {
        if (currentLevel <= 4)
        {
            return upgradeCosts[currentLevel];
        }
        return 0f; // �ő僌�x���̏ꍇ�A�A�b�v�O���[�h�R�X�g�̓[��
    }
}