using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // �G�f�B�^���s�������삷��
public class BoundaryController : MonoBehaviour
{
    public Vector3 minBounds; // �X�e�[�W���̍ŏ��ʒu
    public Vector3 maxBounds; // �X�e�[�W���̍ő�ʒu
    public float gridSpacing = 1f; // �O���b�h�̊Ԋu
    public Color gridColor = Color.green; // �O���b�h�̐F

    // �v���C���[�̈ʒu�����E���ɐ������郁�\�b�h
    public Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3(
            Mathf.Clamp(position.x, minBounds.x, maxBounds.x),
            position.y, // Y���͐������Ȃ�
            Mathf.Clamp(position.z, minBounds.z, maxBounds.z)
        );
    }

    // �f�o�b�O�\��: ���E�ƃO���b�h��`��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // ���E�{�b�N�X��`��
        Vector3 size = maxBounds - minBounds;
        Gizmos.DrawWireCube(minBounds + size / 2, size);

        // �O���b�h�̕`��
        DrawGrid();
    }

    private void DrawGrid()
    {
        Gizmos.color = gridColor;

        // XZ���ʏ�̃O���b�h��`��
        for (float x = minBounds.x; x <= maxBounds.x; x += gridSpacing)
        {
            Gizmos.DrawLine(new Vector3(x, 0, minBounds.z), new Vector3(x, 0, maxBounds.z));
        }

        for (float z = minBounds.z; z <= maxBounds.z; z += gridSpacing)
        {
            Gizmos.DrawLine(new Vector3(minBounds.x, 0, z), new Vector3(maxBounds.x, 0, z));
        }
    }
}