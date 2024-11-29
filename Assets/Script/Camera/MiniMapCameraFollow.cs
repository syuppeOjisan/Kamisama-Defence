using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public Vector3 offset = new Vector3(0f, 50f, 0f); // �v���C���[�ɑ΂���J�����̃I�t�Z�b�g
    public bool followRotation = false; // �v���C���[�̉�]�ɒǏ]�����邩

    void LateUpdate()
    {
        if (player != null)
        {
            // �v���C���[�̏���ɃJ������z�u
            Vector3 newPosition = player.position + offset;
            transform.position = newPosition;

            // �v���C���[�̉�]�ɉe������Ȃ��悤�ɁA�J�����̉�]���Œ�
            if (!followRotation)
            {
                transform.rotation = Quaternion.Euler(90f, 0f, 0f); // �J������^���Ɍ�����
            }
            else
            {
                // �v���C���[�ɒǏ]���ĉ�]���������ꍇ�̏���
                transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
            }
        }
    }
}