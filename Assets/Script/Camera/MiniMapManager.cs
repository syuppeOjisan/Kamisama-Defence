using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    public Camera miniMapCamera; // ミニマップ用のカメラ
    public RenderTexture miniMapTexture; // ミニマップ用のRenderTexture
    public RawImage miniMapUI; // ミニマップ表示用のUI (RawImage)
    public Material pathMaterial; // 道に適用するマテリアル

    void Start()
    {
        // ミニマップカメラの設定
        if (miniMapCamera != null && miniMapTexture != null)
        {
            miniMapCamera.targetTexture = miniMapTexture;
        }
        else
        {
            Debug.LogError("MiniMapCamera または MiniMapTexture が設定されていません！");
        }

        // ミニマップUIの設定
        if (miniMapUI != null && miniMapTexture != null)
        {
            miniMapUI.texture = miniMapTexture;
        }
        else
        {
            Debug.LogError("MiniMapUI または MiniMapTexture が設定されていません！");
        }

        // 道のマテリアルを適用
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