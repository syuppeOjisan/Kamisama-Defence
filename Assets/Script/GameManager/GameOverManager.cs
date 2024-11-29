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
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 1); // �f�t�H���g�̓L�����N�^�[1

        // �X�e�[�W�f�[�^�ƃQ�[���f�[�^�����Z�b�g
        ResetStageData();

        // �I�������L�����N�^�[�ƃX�e�[�W��ێ������܂܍ăX�^�[�g
        if (lastPlayedStage == 1)
        {
            SceneManager.LoadScene("Stage1Scene");
        }
        // ���̃X�e�[�W��ǉ�����ꍇ�͂����ɒǉ�
    }

    // �X�e�[�W�f�[�^�����Z�b�g���郁�\�b�h
    private void ResetStageData()
    {
        // ���ΑK�|�C���g�̃��Z�b�g
        PlayerPrefs.SetFloat("OfferingPoints", 0);

        // �����ő��̃��Z�b�g���K�v�ȃf�[�^�����Z�b�g����
        // �Ⴆ�΁A���j�b�g�z�u��G�E�Q�q�q�̏������Z�b�g����ꍇ�́A�V�[���ēǂݍ��ݎ��ɏ����������悤�ɂ���
    }

    // �X�e�[�W�Z���N�g�V�[���ɖ߂�{�^��
    public void ReturnToStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}