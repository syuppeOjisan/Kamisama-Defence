using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrevSceneTracker : MonoBehaviour
{
    // シーン名
    public static string PreviousSceneName = null;

    // 現在のシーン名を保存
    public void SetPrevSceneName(string nowSceneName)
    {
        PreviousSceneName = nowSceneName;
    }

    // 保存したシーン名を取得
    public string GetPrevSceneName()
    {
        return PreviousSceneName;
    }

    public void BackToPrevScene()
    {
        if(PreviousSceneName != null)
        {
            SceneManager.LoadScene(PreviousSceneName);
        }
        else
        {
            Debug.LogError("シーン名が保存されていません");
        }
    }
}
