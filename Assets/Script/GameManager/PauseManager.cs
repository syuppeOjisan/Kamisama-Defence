using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // �|�[�Y���UI

    private bool isPaused = false;

    void Update()
    {
        // Escape�L�[�Ń|�[�Y�؂�ւ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // �Q�[�����ꎞ��~
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // �Q�[���̎��Ԃ��~
        pauseMenuUI.SetActive(true); // �|�[�Y���j���[��\��
    }

    // �Q�[�����ĊJ
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // �Q�[���̎��Ԃ��ĊJ
        pauseMenuUI.SetActive(false); // �|�[�Y���j���[���\��
    }

    // ���g���C�{�^���������ꂽ�Ƃ�
    public void RetryStage()
    {
        Time.timeScale = 1; // ���Ԃ��ĊJ���Ă���V�[�����ă��[�h
        int lastPlayedStage = PlayerPrefs.GetInt("SelectedStage", 1);
        SceneManager.LoadScene($"Stage{lastPlayedStage}Scene");
    }

    // �X�e�[�W�Z���N�g�ɖ߂�{�^���������ꂽ�Ƃ�
    public void ReturnToStageSelect()
    {
        Time.timeScale = 1; // ���Ԃ��ĊJ���Ă���X�e�[�W�Z���N�g�ɖ߂�
        SceneManager.LoadScene("StageSelectScene");
    }
}