using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // ���g���C�{�^���������ꂽ�Ƃ��ɌĂяo�����\�b�h
    public void RetryStage()
    {
        // �Ō�Ƀv���C���Ă����X�e�[�W�ԍ����擾
        int lastPlayedStage = PlayerPrefs.GetInt("SelectedStage", 1); // �f�t�H���g�̓X�e�[�W1

        // �X�e�[�W�f�[�^�����Z�b�g
        ResetStageData();

        // �X�e�[�W�ԍ��Ɋ�Â��Đ������V�[�������[�h
        string stageSceneName = $"Stage{lastPlayedStage}Scene";
        if (Application.CanStreamedLevelBeLoaded(stageSceneName)) // �V�[�������݂��邩�m�F
        {
            SceneManager.LoadScene(stageSceneName);
        }
        else
        {
            Debug.LogError($"�w�肳�ꂽ�X�e�[�W�V�[����������܂���: {stageSceneName}");
        }
    }

    // �X�e�[�W�f�[�^�����Z�b�g���郁�\�b�h
    private void ResetStageData()
    {
        // ���ΑK�|�C���g�̃��Z�b�g
        PlayerPrefs.SetFloat("OfferingPoints", 0);

        // ���̃��Z�b�g������ǉ��\
        // ��: ���Z�b�g���K�v�ȃQ�[����Ԃ�J�X�^���f�[�^
    }

    // �X�e�[�W�Z���N�g�V�[���ɖ߂�{�^��
    public void ReturnToStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}