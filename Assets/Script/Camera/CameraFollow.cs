using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // �v���C���[��Transform���w��
    public Vector3 offset; // �J�����̃I�t�Z�b�g

    void Update()
    {
        if (playerTransform != null)
        {
            // �J�������v���C���[�̈ʒu�ɒǏ]������
            transform.position = playerTransform.position + offset;
        }
    }
}