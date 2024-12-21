using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // �C�x���g�p���O���
using UnityEngine.UI;

public class ButtonSoundHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("���ʉ��ݒ�")]
    public AudioClip hoverSound; // �J�[�\�������������̌��ʉ�
    public AudioClip clickSound; // �N���b�N���̌��ʉ�

    private AudioSource audioSource;

    void Start()
    {
        // AudioSource��ǉ��i���݂��Ȃ��ꍇ�j
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // �J�[�\�����{�^����ɍ��������̏���
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // �{�^�����N���b�N���ꂽ���̏���
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}