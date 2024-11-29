using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    // �L�����N�^�[1�̑I��
    public void SelectCharacter1()
    {
        PlayerPrefs.SetInt("SelectedCharacter", 1); // �L�����N�^�[1��ۑ�
        LoadSelectedStage(); // �I�������X�e�[�W�����[�h
    }

    // �L�����N�^�[2�̑I��
    public void SelectCharacter2()
    {
        PlayerPrefs.SetInt("SelectedCharacter", 2); // �L�����N�^�[2��ۑ�
        LoadSelectedStage(); // �I�������X�e�[�W�����[�h
    }

    // �I�������X�e�[�W�����[�h����
    private void LoadSelectedStage()
    {
        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1); // �f�t�H���g�̓X�e�[�W1

        // �X�e�[�W�ԍ��ɉ����ăV�[�������[�h
        if (selectedStage == 1)
        {
            SceneManager.LoadScene("Stage1Scene");
        }

        if (selectedStage == 2)
        {
            SceneManager.LoadScene("Stage2Scene");
        }

        if (selectedStage == 3)
        {
            SceneManager.LoadScene("Stage3Scene");
        }
        // ���̃X�e�[�W��ǉ�����ꍇ�͂����ɒǉ�
    }

    // �X�e�[�W�Z���N�g�V�[���ɖ߂郁�\�b�h
    public void ReturnToStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}