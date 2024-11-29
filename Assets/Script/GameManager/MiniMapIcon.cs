using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public Transform target; // ミニマップで追跡するオブジェクト
    public Vector3 offset; // ミニマップ上でのオフセット（必要に応じて調整）

    void LateUpdate()
    {
        if (target != null)
        {
            // 追跡対象のXとZの位置にアイコンを合わせる（Y軸は固定）
            Vector3 newPosition = target.position + offset;
            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
        }
    }
}