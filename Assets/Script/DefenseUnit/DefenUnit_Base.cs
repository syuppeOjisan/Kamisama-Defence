using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ユニットの基底クラス
public class DefenseUnit_Base : MonoBehaviour
{
    public float AttackPower;   // 攻撃力
    public float AttackRange;   // 攻撃範囲
    public float LifeTime;      // 持続時間


    private string PrefabPath = null;   // プレハブのパス

    // Start is called before the first frame update
    void Start()
    {
        // 変数初期化
        AttackPower = 0.0f;
        AttackRange = 0.0f;
        LifeTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //============= ゲッター ==================
    // 攻撃力を取得
    public float GetAttackPower()
    {
        return AttackPower;
    }

    // 攻撃範囲を取得
    public float GetAttackRange()
    {
        return AttackRange;
    }

    // 持続時間を取得
    public float GetLifeTime()
    {
        return LifeTime;
    }

    //============= セッター ==================
    // 攻撃力を強化
    public float AddAttackPower(float addAmount)
    {
        AttackPower += addAmount;

        return AttackPower;
    }

    // 攻撃範囲を強化
    public float AddAttackRange(float addAmount)
    {
        AttackRange += addAmount;

        return AttackRange;
    }

    // 持続時間を強化
    public float AddLifeTime(float addAmount)
    {
        LifeTime += addAmount;

        return LifeTime;
    }

    // プレハブのパスをセット
    public void SetPrefabPath(string path)
    {
        PrefabPath = path;
    }
}
