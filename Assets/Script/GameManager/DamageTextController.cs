using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    public Animator animator; // Animator���A�T�C��

    // �_���[�W��\������ۂɌĂяo�����\�b�h
    public void ShowDamage()
    {
        if (animator != null)
        {
            animator.SetTrigger("ShowDamage"); // Trigger��ݒ肵�ăA�j���[�V�������Đ�
        }
    }
}