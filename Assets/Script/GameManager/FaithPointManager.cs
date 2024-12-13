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

    private int totalFaithPoints = 0; // �݌v�M�|�C���g

    public int GetTotalFaithPoints()
    {
        return totalFaithPoints;
    }

    public void AddFaithPoints(int amount)
    {
        totalFaithPoints += amount;
        Debug.Log($"�M�|�C���g��ǉ����܂���: {amount}. ���v: {totalFaithPoints}");
    }

    public void ResetFaithPoints()
    {
        totalFaithPoints = 0;
        Debug.Log("�M�|�C���g�����Z�b�g���܂����B");
    }
}