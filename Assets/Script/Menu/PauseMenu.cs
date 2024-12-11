using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // ���݂̃V�[�����ēǂݍ��݁i���X�^�[�g�j
    public void RestartThisScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �X�e�[�W�Z���N�g�ɖ߂�
    public void GoBackStageSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("StageSelectScene");
    }
}
