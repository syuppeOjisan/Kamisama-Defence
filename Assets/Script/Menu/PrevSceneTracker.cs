using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrevSceneTracker : MonoBehaviour
{
    // �V�[����
    public static string PreviousSceneName = null;

    // ���݂̃V�[������ۑ�
    public void SetPrevSceneName(string nowSceneName)
    {
        PreviousSceneName = nowSceneName;
    }

    // �ۑ������V�[�������擾
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
            Debug.LogError("�V�[�������ۑ�����Ă��܂���");
        }
    }
}
