using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUnitStore : MonoBehaviour
{
    [Header("強化したいユニット")]
    public GameObject unit;
    [Header("強化したいステータスを選ぶ")]
    public bool UpgradeAtkPower = false;    // 攻撃力を強化したい場合
    public bool UpgradeAtkRange = false;    // 攻撃範囲を強化したい場合
    public bool UpgradeLifeTime = false;    // 生存時間を強化したい場合
    [Header("強化したい数値を入力")]
    public float UpgradeAmount;             // 強化値

    private DefenseUnit_Base baseUnit;  // 強化したいユニットのステータス取得
    private TMP_Text statusNum; // 現在のステータスと強化後のステータス

    public void Start()
    {
    }

    public void Update()
    {
        if (!unit.TryGetComponent<DefenseUnit_Base>(out baseUnit))
        {
            Debug.LogError("Baseが取得できませんでした");
        }

        // 強化するステータスに合わせて数値を取得
        float nowStatus = 0.0f;
        if(UpgradeAtkPower)
        {
            nowStatus = baseUnit.GetAttackPower();
        }
        else if(UpgradeAtkRange)
        {
            nowStatus = baseUnit.GetAttackRange();
        }
        else if(UpgradeLifeTime)
        {
            nowStatus = baseUnit.GetLifeTime();
        }

        // 表示を変更
        statusNum = GetComponentInChildren<TMP_Text>();
        if(statusNum)
        {
            statusNum.text = nowStatus.ToString() + "→" + (nowStatus + UpgradeAmount).ToString();
        }
    }
}
