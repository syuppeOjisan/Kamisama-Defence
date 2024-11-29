using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public Transform target; // �~�j�}�b�v�ŒǐՂ���I�u�W�F�N�g
    public Vector3 offset; // �~�j�}�b�v��ł̃I�t�Z�b�g�i�K�v�ɉ����Ē����j

    void LateUpdate()
    {
        if (target != null)
        {
            // �ǐՑΏۂ�X��Z�̈ʒu�ɃA�C�R�������킹��iY���͌Œ�j
            Vector3 newPosition = target.position + offset;
            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
        }
    }
}