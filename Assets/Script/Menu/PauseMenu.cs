using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // ���݂̃V�[�����ēǂݍ��݁i���X�^�[�g�j
    public void RestartThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �X�e�[�W�Z���N�g�ɖ߂�
    public void GoBackStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
