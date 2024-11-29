using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // プレイヤーのTransformを指定
    public Vector3 offset; // カメラのオフセット

    void Update()
    {
        if (playerTransform != null)
        {
            // カメラをプレイヤーの位置に追従させる
            transform.position = playerTransform.position + offset;
        }
    }
}