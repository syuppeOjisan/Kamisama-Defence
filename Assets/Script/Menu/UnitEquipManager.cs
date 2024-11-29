using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitEquipManager : MonoBehaviour
{
    [Header("トグルボタン")]
    public Toggle[] toggles;    // 使用するボタンたち
    [Header("ボタンに対応したユニット")]
    [Tooltip("トグルボタンリスト0️番に入れたボタンに対応するユニットをこのリストの0番に入れてね")]
    public GameObject[] units;  // ユニットたち

    [HideInInspector]
    public  static List<GameObject> equipUnits; // 装備したユニットのリスト
    [System.NonSerialized]
    public  static bool isEquipSelected = false;// 装備を変更したかどうか

    [System.NonSerialized]
    private static bool isFirsttime = true;

    private bool isListUpdated = false; 

    // Start is called before the first frame update
    void Start()
    {
        // リストのエラーチェック
        if(toggles.Length != units.Length)
        {
            Debug.LogError("トグルボタンのリストとユニットのリストの数が一致しません。" +
                           "一致するように変更してください。");
        }

        // このシーンの実行が初回かどうか
        if(isFirsttime)
        {
            // リスト初期化
            equipUnits = new List<GameObject>();
            // 初回フラグを変更
            isFirsttime = false;
        }
        else
        {
            // ２回目以降の実行なら前回の選択内容をボタンに反映
            SetToggles();
        }

    }

    // ボタンで選択されたユニットをリストに反映
    public void SetEquipUnits()
    {
        if(!isListUpdated)
        {
            equipUnits.Clear(); // リストを更新するのですでにある情報は削除
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                {
                    equipUnits.Add(units[i]);
                    isListUpdated = true;
                    isEquipSelected = true;
                    Debug.Log("きてます");
                }
            }
        }

    }

    // すでに選択されているユニットがあれば、ボタンに反映
    public void SetToggles()
    {
        foreach(GameObject unit in equipUnits)
        {
            // 配列unitsにunitが入っているかをチェック
            int index = Array.IndexOf(units, unit);

            if (index >= 0)
            {
                // 入っていればボタンをTrueに
                toggles[index].isOn = true;
            }
            else
            {
                toggles[index].isOn = false;
            }

        }
    }
}
