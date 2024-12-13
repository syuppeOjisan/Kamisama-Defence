using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    public Camera miniMapCamera; // �~�j�}�b�v�p�̃J����
    public RenderTexture miniMapTexture; // �~�j�}�b�v�p��RenderTexture
    public RawImage miniMapUI; // �~�j�}�b�v�\���p��UI (RawImage)
    public Material pathMaterial; // ���ɓK�p����}�e���A��

    void Start()
    {
        // �~�j�}�b�v�J�����̐ݒ�
        if (miniMapCamera != null && miniMapTexture != null)
        {
            miniMapCamera.targetTexture = miniMapTexture;
        }
        else
        {
            Debug.LogError("MiniMapCamera �܂��� MiniMapTexture ���ݒ肳��Ă��܂���I");
        }

        // �~�j�}�b�vUI�̐ݒ�
        if (miniMapUI != null && miniMapTexture != null)
        {
            miniMapUI.texture = miniMapTexture;
        }
        else
        {
            Debug.LogError("MiniMapUI �܂��� MiniMapTexture ���ݒ肳��Ă��܂���I");
        }

        // ���̃}�e���A����K�p
        ApplyPathMaterial();
    }

    void ApplyPathMaterial()
    {
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject path in paths)
        {
            Renderer renderer = path.GetComponent<Renderer>();
            if (renderer != null && pathMaterial != null)
            {
                renderer.material = pathMaterial;
            }
        }
    }
}