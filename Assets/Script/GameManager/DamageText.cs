using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProを使用

public class DamageText : MonoBehaviour
{
    public float moveSpeed = 1f; // ダメージ表示が上に移動する速度
    public float fadeDuration = 1f; // ダメージ表示のフェードアウト時間
    private TMP_Text damageText; // TextMeshProコンポーネント

    private Color textColor;

    void Start()
    {
        damageText = GetComponent<TMP_Text>();
        textColor = damageText.color;
    }

    void Update()
    {
        // ダメージ表示を上に移動
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // 時間経過に伴ってフェードアウト
        textColor.a -= Time.deltaTime / fadeDuration;
        damageText.color = textColor;

        // 完全に透明になったらオブジェクトを削除
        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    // ダメージを設定
    public void SetDamageText(float damage)
    {
        damageText.text = damage.ToString();
    }
}