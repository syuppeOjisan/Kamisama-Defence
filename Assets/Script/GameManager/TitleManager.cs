using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private FadeController fadeController;
    private AudioSource audioSource;
    public AudioClip buttonClickSound; // ボタンをクリックしたときの効果音

    void Start()
    {
        // FadeControllerをシーン内から探す
        fadeController = FindObjectOfType<FadeController>();

        // AudioSourceコンポーネントを追加
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // ステージセレクトシーンに遷移するメソッド
    public void GoToStageSelect()
    {
        // ボタンの効果音を再生
        PlayButtonClickSound();

        // フェードアウトしてからシーン遷移を行う
        if (fadeController != null)
        {
            StartCoroutine(fadeController.FadeOutAndLoadScene("StageSelectScene"));
        }
        else
        {
            // FadeControllerが見つからない場合は直接シーン遷移
            SceneManager.LoadScene("StageSelectScene");
        }
    }

    // ゲームを終了するメソッド
    public void QuitGame()
    {
        // ボタンの効果音を再生
        PlayButtonClickSound();

        Debug.Log("ゲームを終了しました。");
        Application.Quit(); // エディタでは機能しませんが、ビルド後には動作します
    }

    // ボタンをクリックしたときの効果音を再生するメソッド
    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}