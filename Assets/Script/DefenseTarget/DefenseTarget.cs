using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを使うために必要
using UnityEngine.SceneManagement; // シーン遷移のために必要

public class DefenseTarget : MonoBehaviour
{
    public float maxHP = 100f; // 神社の最大HP
    private float currentHP = 0f; // 現在のHP

    public Image hpBarFill; // HPバーの「Fill」Image
    private StageManager stageManager; // StageManagerへの参照

    private PrevSceneTracker tracker = new PrevSceneTracker();

    void Start()
    {
        // StageManagerへの参照を取得
        stageManager = FindObjectOfType<StageManager>();
        UpdateHPBar(); // ゲーム開始時にHPバーを更新
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(enemy.damage); // 'damageToTarget' を 'damage' に変更
        }
    }

    // 神社がダメージを受けたときに呼び出されるメソッド
    public void TakeDamage(float damageAmount)
    {
        currentHP += damageAmount; // ダメージを加算
        if (currentHP >= maxHP)
        {
            Destroy(gameObject); // HPが最大に達すると神社が破壊される
            GameOver(); // ゲームオーバー処理を呼び出し
        }
        UpdateHPBar(); // HPバーを更新
    }

    // HPバーを更新するメソッド
    void UpdateHPBar()
    {
        if (hpBarFill != null)
        {
            float fillAmount = currentHP / maxHP; // HPの割合を計算
            hpBarFill.fillAmount = fillAmount; // HPバーを更新
        }
    }

    // ゲームオーバー時の処理
    void GameOver()
    {
        Debug.Log("ゲームオーバー！");
        tracker.SetPrevSceneName(SceneManager.GetActiveScene().name); // 現在のシーン名を保存
        SceneManager.LoadScene("GameOverScene"); // ゲームオーバーシーンに遷移
    }
}