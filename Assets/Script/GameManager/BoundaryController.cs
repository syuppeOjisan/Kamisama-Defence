using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // エディタ実行中も動作する
public class BoundaryController : MonoBehaviour
{
    public Vector3 minBounds; // ステージ内の最小位置
    public Vector3 maxBounds; // ステージ内の最大位置
    public float gridSpacing = 1f; // グリッドの間隔
    public Color gridColor = Color.green; // グリッドの色

    // プレイヤーの位置を境界内に制限するメソッド
    public Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3(
            Mathf.Clamp(position.x, minBounds.x, maxBounds.x),
            position.y, // Y軸は制限しない
            Mathf.Clamp(position.z, minBounds.z, maxBounds.z)
        );
    }

    // デバッグ表示: 境界とグリッドを描画
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // 境界ボックスを描画
        Vector3 size = maxBounds - minBounds;
        Gizmos.DrawWireCube(minBounds + size / 2, size);

        // グリッドの描画
        DrawGrid();
    }

    private void DrawGrid()
    {
        Gizmos.color = gridColor;

        // XZ平面上のグリッドを描画
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