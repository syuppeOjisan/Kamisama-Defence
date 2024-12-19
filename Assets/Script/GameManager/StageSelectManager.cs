using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectManager : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    public Button stage4Button;
    public Button stage5Button;
    public Button stage6Button;
    public Button stage7Button;
    public Button stage8Button;
    public Button stage9Button;
    public Button stage10Button;
    public Button stage11Button;
    public Button stage12Button;
    public Button stage13Button;
    public Button stage14Button;
    public Button stage15Button;
    public Button stage16Button;
    public Button stage17Button;
    public Button stage18Button;
    public Button returnToTitleButton; // �^�C�g���ɖ߂�{�^��
    public Button pointAllocationButton; // �i�������V�[���ւ̃{�^��
    public GameObject scrollBar;    // �X�N���[���o�[�������Ă�I�u�W�F�N�g

    [Header("�{�^���̐�")]
    public float buttonAmount = 10;
    [Header("�X�N���[���r���[�̏�Ɖ�")]
    public GameObject scrollView_Top;
    public GameObject scrollView_Bottom;

    private FadeController fadeController;
    private Button[] allButtons; // �V�[�����̂��ׂẴ{�^��
    private ScrollRect scrollRect;  // �X�N���[���o�[�{��
    private float moveAmount;   // �X�N���[���o�[�̈ړ���

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
        stage4Button.onClick.AddListener(() => SelectStage(4));
        stage5Button.onClick.AddListener(() => SelectStage(5));
        stage6Button.onClick.AddListener(() => SelectStage(6));
        stage7Button.onClick.AddListener(() => SelectStage(7));
        stage8Button.onClick.AddListener(() => SelectStage(8));
        stage9Button.onClick.AddListener(() => SelectStage(9));
        stage10Button.onClick.AddListener(() => SelectStage(10));
        //stage11Button.onClick.AddListener(() => SelectStage(11));
        //stage12Button.onClick.AddListener(() => SelectStage(12));
        //stage13Button.onClick.AddListener(() => SelectStage(13));
        //stage14Button.onClick.AddListener(() => SelectStage(14));
        //stage15Button.onClick.AddListener(() => SelectStage(15));
        //stage16Button.onClick.AddListener(() => SelectStage(16));
        //stage17Button.onClick.AddListener(() => SelectStage(17));
        //stage18Button.onClick.AddListener(() => SelectStage(18));
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        pointAllocationButton.onClick.AddListener(GoToPointAllocation);

        // �X�N���[���o�[�̖{�̂��擾
        if(!scrollBar.TryGetComponent<ScrollRect>(out scrollRect))  
        {
            Debug.LogError("ScrollRect���擾�ł��܂���ł���");
        }

        // �X�N���[���o�[�̈ړ��ʂ��v�Z
        moveAmount = 1 / (buttonAmount - 6f);
    }

    void Update()
    {
        UpdateScroll();
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

    // �R���g���[���[�ŃX�N���[���o�[���ړ�
    private void UpdateScroll()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position.y >= scrollView_Top.transform.position.y)
            {
                scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition + moveAmount;
            }
            else if (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position.y <= scrollView_Bottom.transform.position.y)
            {
                scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition - moveAmount;
            }
        }
    }
}