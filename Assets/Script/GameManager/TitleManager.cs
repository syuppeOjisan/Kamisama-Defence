using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private FadeController fadeController;
    private AudioSource audioSource;
    public AudioClip buttonClickSound; // �{�^�����N���b�N�����Ƃ��̌��ʉ�

    void Start()
    {
        // FadeController���V�[��������T��
        fadeController = FindObjectOfType<FadeController>();

        // AudioSource�R���|�[�l���g��ǉ�
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // �X�e�[�W�Z���N�g�V�[���ɑJ�ڂ��郁�\�b�h
    public void GoToStageSelect()
    {
        // �{�^���̌��ʉ����Đ�
        PlayButtonClickSound();

        // �t�F�[�h�A�E�g���Ă���V�[���J�ڂ��s��
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("StageSelectScene"));
        }
        else
        {
            // FadeController��������Ȃ��ꍇ�͒��ڃV�[���J��
            SceneManager.LoadScene("StageSelectScene");
        }
    }

    // �Q�[�����I�����郁�\�b�h
    public void QuitGame()
    {
        // �{�^���̌��ʉ����Đ�
        PlayButtonClickSound();

        Debug.Log("�Q�[�����I�����܂����B");
        Application.Quit(); // �G�f�B�^�ł͋@�\���܂��񂪁A�r���h��ɂ͓��삵�܂�
    }

    // �{�^�����N���b�N�����Ƃ��̌��ʉ����Đ����郁�\�b�h
    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}