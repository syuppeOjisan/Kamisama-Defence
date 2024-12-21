using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // ScrollRectを使用するための名前空間

public class ButtonScrollHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ScrollRect parentScrollRect;

    void Start()
    {
        // 親階層で ScrollRect を検索
        parentScrollRect = GetComponentInParent<ScrollRect>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ScrollRect の操作を許可
        if (parentScrollRect != null)
        {
            parentScrollRect.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 何もしない：常に ScrollRect を操作可能にする
    }
}