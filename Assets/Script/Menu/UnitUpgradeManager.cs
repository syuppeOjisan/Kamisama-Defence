using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitUpgradeManager : MonoBehaviour
{
    public GameObject[] UpgradeMenuUnits;

    private string currentPressedButtonName;
    private string prevPressedButtonName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // なにかボタンが押されたらそれに対応した強化画面を表示
        if(GetLastPressedButton() != null)
        {
            // 押されたユニットのボタン情報を取得し、表示する強化画面の名前に変える
            prevPressedButtonName = currentPressedButtonName;
            currentPressedButtonName = GetLastPressedButton().name;
            string buttonName = "";
            Debug.Log("currentPressedButtonNameは[" + currentPressedButtonName + "]です");
            Debug.Log("prevPressedButtonName[" + prevPressedButtonName + "]です");

            // 押されたボタンの名前に"Button"が含まれていたらそれにあった強化画面を表示する
            if(currentPressedButtonName.Contains("Button"))
            {
                Debug.Log("ユニット選択ボタン押されてます");

                buttonName = currentPressedButtonName.Split("_")[0];
                buttonName += "_upgrade";
                Debug.Log("buttonName" + buttonName + "に変更されました");

                GameObject upgradeMenu = FindUpgradeMenu(buttonName);
                if (upgradeMenu)
                {
                    upgradeMenu.SetActive(true);
                }
            }

            // 強化ボタンが押されたらステータス強化
            if(currentPressedButtonName.Contains("upgrade_button") && prevPressedButtonName.Contains("Button"))
            {
                Debug.Log("強化ボタン押されてます");

                buttonName = prevPressedButtonName.Split("_")[0];
                buttonName += "_upgrade";
                Debug.Log("buttonName" + buttonName + "に変更されました");

                Debug.Log(buttonName + "で検索します");
                GameObject upgradeMenu = FindUpgradeMenu(buttonName);
                Debug.Log(upgradeMenu.name + "の情報取れてます");
                UpgradeUnitStore upgradeUnit;
                if(upgradeMenu.TryGetComponent<UpgradeUnitStore>(out upgradeUnit))
                {
                    DefenseUnit_Base defenUnit;
                    if(upgradeUnit.unit.TryGetComponent<DefenseUnit_Base>(out defenUnit))
                    {
                        // フラグによって強化するステータスを変更
                        if(upgradeUnit.UpgradeAtkPower)
                        {
                            defenUnit.AddAttackPower(upgradeUnit.UpgradeAmount);
                        }
                        else if(upgradeUnit.UpgradeAtkRange)
                        {
                            defenUnit.AddAttackRange(upgradeUnit.UpgradeAmount);
                        }
                        else if(upgradeUnit.UpgradeLifeTime)
                        {
                            defenUnit.AddLifeTime(upgradeUnit.UpgradeAmount);
                        }
                    }
                    else
                    {
                        Debug.LogError(upgradeUnit.unit.name + "にDefenceUnit_Baseが継承されていません");
                    }
                }
                else
                {
                    Debug.LogError("UpgradePanel->"+ buttonName +"->UpgradeUnitStoreにユニットがありません");
                }

            }


        }
    }

    // 最後に押されたボタンを取得
    GameObject GetLastPressedButton()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        GameObject lastClick = null;

        if(Input.GetMouseButtonUp(0))
        {
            // Raycastの結果を格納するリスト
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // リストが空でない場合、最後にクリックされたGameObjectを取得し、Buttonコンポーネントを取得
            if (results.Count > 0)
            {
                lastClick = results[results.Count - 1].gameObject;
            }
        }


        return lastClick;
    }

    // 引数で指定された名前の強化画面がリスト内にあるかどうかを判定し、リスト内にあればそれを返す
    GameObject FindUpgradeMenu(string unitName)
    {
        foreach(GameObject upgradeMenu in UpgradeMenuUnits)
        {
            if(upgradeMenu.name == unitName)
            {
                return upgradeMenu;
            }
        }

        return null;  
    }
}

