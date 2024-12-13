using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithItem : MonoBehaviour
{
    public int faithPoints = 10; // このアイテムで獲得できる信仰ポイント
    public AudioClip pickupSound; // アイテム取得時の効果音

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StageManager stageManager = FindObjectOfType<StageManager>();
            if (stageManager != null)
            {
                stageManager.AddFaithPoints(faithPoints);
            }

            // 効果音再生
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject); // アイテムを消す
        }
    }
}