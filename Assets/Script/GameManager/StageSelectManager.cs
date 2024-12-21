using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StageSelectManager : MonoBehaviour
{
    public Button[] stageButtons; // �S�ẴX�e�[�W�{�^��
    public Button returnToTitleButton; // �^�C�g���ɖ߂�{�^��
    public Button pointAllocationButton; // �i�������V�[���ւ̃{�^��
    public GameObject equipPromptPanel; // ���j�b�g�������i���b�Z�[�W�̃p�l��
    public TextMeshProUGUI equipPromptText; // ���b�Z�[�W�p�e�L�X�g
    public Image equipPromptImage; // �w�i�p�̉摜
    public ScrollRect stageScrollView; // �X�N���[���r���[��ScrollRect

    private FadeController fadeController;

    void Start()
    {
        // FadeController���V�[��������T��
        fadeController = FindObjectOfType<FadeController>();

        // ���j�b�g������Ԃ��m�F
        CheckUnitEquipStatus();

        // Fade�C������
        if (fadeController != null)
        {
            SetButtonsInteractable(false); // �t�F�[�h���̓{�^���𖳌���
            StartCoroutine(fadeController.FadeIn(() => SetButtonsInteractable(true)));
        }

        // �{�^���̃N���b�N�C�x���g��ݒ�
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageNumber = i + 1;
            stageButtons[i].onClick.AddListener(() => SelectStage(stageNumber));
        }
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
        SetButtonsInteractable(false);

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
        SetButtonsInteractable(false);

        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("PointAllocationScene"));
        }
        else
        {
            SceneManager.LoadScene("PointAllocationScene");
        }
    }

    // ���j�b�g����������Ă��邩���m�F����Scroll View�ƃ{�^���𖳌���/�L����
    private void CheckUnitEquipStatus()
    {
        bool isUnitEquipped = UnitEquipManager.equipUnits != null && UnitEquipManager.equipUnits.Count > 0;

        if (isUnitEquipped)
        {
            SetStageButtonsInteractable(true);
            stageScrollView.enabled = true; // Scroll View�̑����L����
            equipPromptPanel.SetActive(false); // ���b�Z�[�W��\��
        }
        else
        {
            SetStageButtonsInteractable(false);
            stageScrollView.enabled = false; // Scroll View�̑���𖳌���
            equipPromptPanel.SetActive(true); // ���b�Z�[�W�\��
            equipPromptText.text = "�܂��̓��j�b�g�𑕔����悤�I";
        }
    }

    // �X�e�[�W�{�^���̗L��/������ݒ�
    private void SetStageButtonsInteractable(bool interactable)
    {
        foreach (Button button in stageButtons)
        {
            button.interactable = interactable;
        }
    }

    // �{�^���̗L��/������ݒ�i�t�F�[�h���j
    private void SetButtonsInteractable(bool interactable)
    {
        returnToTitleButton.interactable = interactable;
        pointAllocationButton.interactable = interactable;
        SetStageButtonsInteractable(interactable);
    }
}