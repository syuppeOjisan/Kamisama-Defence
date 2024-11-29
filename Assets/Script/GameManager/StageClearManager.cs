using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearManager : MonoBehaviour
{
    // ステージセレクトシーンに遷移するメソッド
    public void ReturnToStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}