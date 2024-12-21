using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // ScrollRect���g�p���邽�߂̖��O���

public class ButtonScrollHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ScrollRect parentScrollRect;

    void Start()
    {
        // �e�K�w�� ScrollRect ������
        parentScrollRect = GetComponentInParent<ScrollRect>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ScrollRect �̑��������
        if (parentScrollRect != null)
        {
            parentScrollRect.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �������Ȃ��F��� ScrollRect �𑀍�\�ɂ���
    }
}