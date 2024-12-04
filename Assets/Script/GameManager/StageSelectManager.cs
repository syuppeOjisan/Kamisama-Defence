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
    private Button[] allButtons; // �V�[�����̂��ׂẴ{�^��

    void Start()
    {
        // FadeController���V�[��������T��
        fadeController = FindObjectOfType<FadeController>();

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
        SetButtonsInteractable(false); // �{�^���𖳌���
        PlayerPrefs.SetInt("SelectedStage", stageNumber);

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
        SetButtonsInteractable(false); // �{�^���𖳌���

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
        SetButtonsInteractable(false); // �{�^���𖳌���

        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("PointAllocationScene"));
        }
        else
        {
            SceneManager.LoadScene("PointAllocationScene");
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