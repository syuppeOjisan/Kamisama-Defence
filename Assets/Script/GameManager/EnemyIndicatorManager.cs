using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyIndicatorManager : MonoBehaviour
{
    public GameObject indicatorPrefab;      // 矢印インジケーターのプレハブ
    public Camera mainCamera;               // メインカメラへの参照
    public RectTransform canvasRect;        // CanvasのRectTransform（UI範囲を指定するため）

    private GameObject[] currentEnemies = { };
    private Dictionary<GameObject, GameObject> activeIndicators = new Dictionary<GameObject, GameObject>(); // 敵とインジケーターのマッピング

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 敵の数が前回フレームよりも減っていたらリストをチェック(負荷かも)
        if (currentEnemies.Length != enemies.Length)
        {
            // 配列を比較して削除された敵オブジェクトを保存
            GameObject[] destroyedEnemies = currentEnemies.Except(enemies).ToArray();
            foreach (GameObject nullEnemies in destroyedEnemies)
            {
                RemoveIndicatorForEnemy(nullEnemies);
            }
        }

        // 敵の状態を保存
        currentEnemies = enemies;

        foreach (GameObject enemy in enemies)
        {
            if (!IsEnemyOnScreen(enemy))
            {
                if (!activeIndicators.ContainsKey(enemy))
                {
                    CreateIndicatorForEnemy(enemy); // 敵が画面外にいればインジケーターを作成
                }

                UpdateIndicatorPosition(enemy);
            }
            else if (activeIndicators.ContainsKey(enemy))
            {
                RemoveIndicatorForEnemy(enemy); // 敵が画面内に戻ればインジケーターを削除
            }
        }
    }

    // 敵が画面内にいるかどうかを判定
    private bool IsEnemyOnScreen(GameObject enemy)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(enemy.transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0;
    }

    // 敵にインジケーターを作成
    private void CreateIndicatorForEnemy(GameObject enemy)
    {
        GameObject indicator = Instantiate(indicatorPrefab, canvasRect); // Canvasの子オブジェクトとしてインジケーターを作成
        activeIndicators[enemy] = indicator;
    }

    // 敵に対応するインジケーターを削除
    private void RemoveIndicatorForEnemy(GameObject enemy)
    {
        if (activeIndicators.ContainsKey(enemy))
        {
            Destroy(activeIndicators[enemy]);
            activeIndicators.Remove(enemy);
        }
    }

    // インジケーターの位置を更新
    private void UpdateIndicatorPosition(GameObject enemy)
    {
        if (!activeIndicators.ContainsKey(enemy)) return;

        GameObject indicator = activeIndicators[enemy];
        Vector3 enemyScreenPosition = mainCamera.WorldToScreenPoint(enemy.transform.position);


        // カメラに対して敵の位置を計算し、画面外の場合に矢印を表示
        Vector3 clampedPosition = enemyScreenPosition;

        // カメラの裏側にいる場合は値を補正
        if (clampedPosition.z < 0)
        {
            clampedPosition.x = -clampedPosition.x;
            clampedPosition.y = -clampedPosition.y;
        }

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, 50, Screen.width - 50); // 画面端に沿って移動
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 50, Screen.height - 50); // 画面端に沿って移動


        // インジケーターの位置を設定
        RectTransform indicatorRect = indicator.GetComponent<RectTransform>();
        indicatorRect.position = clampedPosition;

        // 矢印の回転を設定（敵の方向を指す）
        Vector3 dir = enemy.transform.position - mainCamera.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        indicatorRect.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}