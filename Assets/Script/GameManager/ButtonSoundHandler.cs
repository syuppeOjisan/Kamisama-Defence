using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // イベント用名前空間
using UnityEngine.UI;

public class ButtonSoundHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("効果音設定")]
    public AudioClip hoverSound; // カーソルが合った時の効果音
    public AudioClip clickSound; // クリック時の効果音

    private AudioSource audioSource;

    void Start()
    {
        // AudioSourceを追加（存在しない場合）
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // カーソルがボタン上に合った時の処理
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // ボタンがクリックされた時の処理
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}