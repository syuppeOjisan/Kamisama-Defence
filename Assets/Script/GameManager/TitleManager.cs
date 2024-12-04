using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Button�̂��߂ɒǉ�

public class TitleManager : MonoBehaviour
{
    private FadeController fadeController;
    private AudioSource audioSource;
    public AudioClip buttonClickSound; // �{�^�����N���b�N�����Ƃ��̌��ʉ�

    private Button[] allButtons; // �V�[�����̂��ׂẴ{�^��

    void Start()
    {
        // FadeController���V�[��������T��
        fadeController = FindObjectOfType<FadeController>();

        // AudioSource�R���|�[�l���g��ǉ�
        audioSource = gameObject.AddComponent<AudioSource>();

        // �V�[�����̂��ׂẴ{�^�����擾
        allButtons = FindObjectsOfType<Button>();

        // �V�[���J�n���Ƀt�F�[�h�C�������s
        if (fadeController != null)
        {
            SetButtonsInteractable(false); // �t�F�[�h���̓{�^���𖳌���
            StartCoroutine(fadeController.FadeIn(() =>
            {
                SetButtonsInteractable(true); // �t�F�[�h�C��������Ƀ{�^����L����
            }));
        }
    }

    // �X�e�[�W�Z���N�g�V�[���ɑJ�ڂ��郁�\�b�h
    public void GoToStageSelect()
    {
        // �{�^���̌��ʉ����Đ�
        PlayButtonClickSound();

        // �t�F�[�h�A�E�g���Ă���V�[���J�ڂ��s��
        if (fadeController != null)
        {
            SetButtonsInteractable(false); // �t�F�[�h�A�E�g�J�n���Ƀ{�^���𖳌���
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

        SetButtonsInteractable(false); // �t�F�[�h�A�E�g���Ƀ{�^��������
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

    // �{�^���̗L��/������ݒ�
    private void SetButtonsInteractable(bool interactable)
    {
        foreach (Button button in allButtons)
        {
            button.interactable = interactable;
        }
    }
}