using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
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
            StartCoroutine(fadeController.FadeIn(() =>
            {
                SetButtonsInteractable(true); // �t�F�[�h�C��������Ƀ{�^����L����
            }));
        }
    }

    // �L�����N�^�[1��I��
    public void SelectCharacter1()
    {
        SetButtonsInteractable(false); // �{�^���𖳌���
        PlayerPrefs.SetInt("SelectedCharacter", 1); // �L�����N�^�[1��ۑ�

        // �I�������X�e�[�W�����[�h�i�t�F�[�h�A�E�g�t���j
        LoadSelectedStage();
    }

    // �L�����N�^�[2��I��
    public void SelectCharacter2()
    {
        SetButtonsInteractable(false); // �{�^���𖳌���
        PlayerPrefs.SetInt("SelectedCharacter", 2); // �L�����N�^�[2��ۑ�

        // �I�������X�e�[�W�����[�h�i�t�F�[�h�A�E�g�t���j
        LoadSelectedStage();
    }

    // �X�e�[�W�����[�h����
    private void LoadSelectedStage()
    {
        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1); // �f�t�H���g�̓X�e�[�W1
        string sceneName = $"Stage{selectedStage}Scene";

        // �t�F�[�h�A�E�g���ăV�[���J��
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene(sceneName));
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    // �X�e�[�W�Z���N�g�V�[���ɖ߂�
    public void ReturnToStageSelect()
    {
        SetButtonsInteractable(false); // �{�^���𖳌���

        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("StageSelectScene"));
        }
        else
        {
            SceneManager.LoadScene("StageSelectScene");
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