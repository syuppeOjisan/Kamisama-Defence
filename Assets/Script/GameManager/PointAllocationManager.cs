using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PointAllocationManager : MonoBehaviour
{
    public Button returnToStageSelectButton; // �X�e�[�W�Z���N�g�ɖ߂�{�^��
    public UnitEquipManager equipManager;   // �����������j�b�g�̏��
    public TextMeshProUGUI errorMessage;    // �G���[���b�Z�[�W
    public TMP_Text totalFaithPointsText;   // �݌v�M�|�C���gUI

    void Start()
    {
        // ����������
        if (totalFaithPointsText != null)
        {
            UpdateFaithPointsUI();
        }

        // �{�^���̃N���b�N�C�x���g��ݒ�
        returnToStageSelectButton.onClick.AddListener(ReturnToStageSelect);

        if (equipManager == null)
        {
            Debug.LogError("�����}�l�[�W���[���A�^�b�`����Ă��܂���");
        }

        if (errorMessage == null)
        {
            Debug.LogError("�G���[���b�Z�[�W���Z�b�g����Ă��܂���");
        }
        else
        {
            errorMessage.alpha = 0;
            errorMessage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (errorMessage != null && errorMessage.alpha > 0)
        {
            errorMessage.alpha -= 0.001f;
        }
        else if (errorMessage.alpha <= 0)
        {
            errorMessage.gameObject.SetActive(false);
        }
    }

    // �M�|�C���gUI���X�V����
    private void UpdateFaithPointsUI()
    {
        if (totalFaithPointsText != null)
        {
            totalFaithPointsText.text = $"�����M�|�C���g: {FaithPointManager.Instance.GetTotalFaithPoints()}";
        }
    }

    // �X�e�[�W�Z���N�g�V�[���ɖ߂�
    public void ReturnToStageSelect()
    {
        equipManager.SetEquipUnits();   // �����������j�b�g�̃��X�g���쐬

        if (UnitEquipManager.isEquipSelected)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
        else
        {
            if (errorMessage != null)
            {
                errorMessage.gameObject.SetActive(true);
                errorMessage.alpha = 1.5f;
            }
        }
    }
}