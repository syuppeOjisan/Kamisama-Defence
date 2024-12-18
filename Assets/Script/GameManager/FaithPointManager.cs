using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithPointManager : MonoBehaviour
{
    private static FaithPointManager instance;
    public static FaithPointManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("FaithPointManager");
                instance = obj.AddComponent<FaithPointManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    private int totalFaithPoints = 0; // 累計信仰ポイント

    public int GetTotalFaithPoints()
    {
        return totalFaithPoints;
    }

    public void AddFaithPoints(int amount)
    {
        totalFaithPoints += amount;
        Debug.Log($"信仰ポイントを追加しました: {amount}. 合計: {totalFaithPoints}");
    }

    public void DeductFaithPoints(int amount)
    {
        if (totalFaithPoints >= amount)
        {
            totalFaithPoints -= amount;
            Debug.Log($"信仰ポイントを減少しました: {amount}. 合計: {totalFaithPoints}");
        }
        else
        {
            Debug.LogWarning("信仰ポイントが不足しています。");
        }
    }

    public void ResetFaithPoints()
    {
        totalFaithPoints = 0;
        Debug.Log("信仰ポイントをリセットしました。");
    }
}