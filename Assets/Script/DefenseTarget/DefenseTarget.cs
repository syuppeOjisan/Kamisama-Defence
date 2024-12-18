using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI�R���|�[�l���g���g�����߂ɕK�v
using UnityEngine.SceneManagement; // �V�[���J�ڂ̂��߂ɕK�v

public class DefenseTarget : MonoBehaviour
{
    public float maxHP = 100f; // �_�Ђ̍ő�HP
    private float currentHP = 0f; // ���݂�HP

    public Image hpBarFill; // HP�o�[�́uFill�vImage
    private StageManager stageManager; // StageManager�ւ̎Q��

    private PrevSceneTracker tracker = new PrevSceneTracker();

    void Start()
    {
        // StageManager�ւ̎Q�Ƃ��擾
        stageManager = FindObjectOfType<StageManager>();
        UpdateHPBar(); // �Q�[���J�n����HP�o�[���X�V
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(enemy.damage); // 'damageToTarget' �� 'damage' �ɕύX
        }
    }

    // �_�Ђ��_���[�W���󂯂��Ƃ��ɌĂяo����郁�\�b�h
    public void TakeDamage(float damageAmount)
    {
        currentHP += damageAmount; // �_���[�W�����Z
        if (currentHP >= maxHP)
        {
            Destroy(gameObject); // HP���ő�ɒB����Ɛ_�Ђ��j�󂳂��
            GameOver(); // �Q�[���I�[�o�[�������Ăяo��
        }
        UpdateHPBar(); // HP�o�[���X�V
    }

    // HP�o�[���X�V���郁�\�b�h
    void UpdateHPBar()
    {
        if (hpBarFill != null)
        {
            float fillAmount = currentHP / maxHP; // HP�̊������v�Z
            hpBarFill.fillAmount = fillAmount; // HP�o�[���X�V
        }
    }

    // �Q�[���I�[�o�[���̏���
    void GameOver()
    {
        Debug.Log("�Q�[���I�[�o�[�I");
        tracker.SetPrevSceneName(SceneManager.GetActiveScene().name); // ���݂̃V�[������ۑ�
        SceneManager.LoadScene("GameOverScene"); // �Q�[���I�[�o�[�V�[���ɑJ��
    }
}