using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    public Button returnToTitleButton; // �^�C�g���ɖ߂�{�^��
    public Button pointAllocationButton; // �i�������V�[���ւ̃{�^��

    private FadeController fadeController;

    void Start()
    {
        // FadeController���V�[��������T��
        fadeController = FindObjectOfType<FadeController>();

        // �V�[���J�n���Ƀt�F�[�h�C�������s
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeIn());
        }

        // �������F�X�e�[�W�I�����̓L�����N�^�[�I�����l�����Ȃ�
        stage1Button.interactable = true;
        stage2Button.interactable = true;
        stage3Button.interactable = true;

        // �{�^���̃N���b�N�C�x���g��ݒ�
        stage1Button.onClick.AddListener(() => SelectStage(1));
        stage2Button.onClick.AddListener(() => SelectStage(2));
        stage3Button.onClick.AddListener(() => SelectStage(3));
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        pointAllocationButton.onClick.AddListener(GoToPointAllocation);
    }

    // �X�e�[�W�I�����ăL�����N�^�[�Z���N�g�Ɉڍs
    public void SelectStage(int stageNumber)
    {
        // �I�������X�e�[�W�ԍ���PlayerPrefs�ɕۑ�
        PlayerPrefs.SetInt("SelectedStage", stageNumber);

        // �L�����N�^�[�Z���N�g�V�[���ɑJ�ځi�t�F�[�h�A�E�g�t���j
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("CharacterSelectScene"));
        }
        else
        {
            SceneManager.LoadScene("CharacterSelectScene");
        }
    }

    // �^�C�g���V�[���ɖ߂�
    public void ReturnToTitle()
    {
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("TitleScene"));
        }
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    // �i�������V�[���ɑJ�ڂ���
    public void GoToPointAllocation()
    {
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("PointAllocationScene"));
        }
        else
        {
            SceneManager.LoadScene("PointAllocationScene");
        }
    }
}
