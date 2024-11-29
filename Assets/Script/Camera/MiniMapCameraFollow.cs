using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public Vector3 offset = new Vector3(0f, 50f, 0f); // プレイヤーに対するカメラのオフセット
    public bool followRotation = false; // プレイヤーの回転に追従させるか

    void LateUpdate()
    {
        if (player != null)
        {
            // プレイヤーの上方にカメラを配置
            Vector3 newPosition = player.position + offset;
            transform.position = newPosition;

            // プレイヤーの回転に影響されないように、カメラの回転を固定
            if (!followRotation)
            {
                transform.rotation = Quaternion.Euler(90f, 0f, 0f); // カメラを真下に向ける
            }
            else
            {
                // プレイヤーに追従して回転させたい場合の処理
                transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
            }
        }
    }
}