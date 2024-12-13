using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithItem : MonoBehaviour
{
    public int faithPoints = 10; // ���̃A�C�e���Ŋl���ł���M�|�C���g
    public AudioClip pickupSound; // �A�C�e���擾���̌��ʉ�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StageManager stageManager = FindObjectOfType<StageManager>();
            if (stageManager != null)
            {
                stageManager.AddFaithPoints(faithPoints);
            }

            // ���ʉ��Đ�
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject); // �A�C�e��������
        }
    }
}