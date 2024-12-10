using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Buttonのために追加

public class TitleManager : MonoBehaviour
{
    private FadeController fadeController;
    private AudioSource audioSource;
    public AudioClip buttonClickSound; // ボタンをクリックしたときの効果音

    private Button[] allButtons; // シーン内のすべてのボタン

    void Start()
    {
        // FadeControllerをシーン内から探す
        fadeController = FindObjectOfType<FadeController>();

        // AudioSourceコンポーネントを追加
        audioSource = gameObject.AddComponent<AudioSource>();

        // シーン内のすべてのボタンを取得
        allButtons = FindObjectsOfType<Button>();

        // シーン開始時にフェードインを実行
        if (fadeController != null)
        {
            SetButtonsInteractable(false); // フェード中はボタンを無効化
            StartCoroutine(fadeController.FadeIn(() =>
            {
                SetButtonsInteractable(true); // フェードイン完了後にボタンを有効化
            }));
        }
    }

    // ステージセレクトシーンに遷移するメソッド
    public void GoToStageSelect()
    {
        // ボタンの効果音を再生
        PlayButtonClickSound();

        // フェードアウトしてからシーン遷移を行う
        if (fadeController != null)
        {
            SetButtonsInteractable(false); // フェードアウト開始時にボタンを無効化
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

        SetButtonsInteractable(false); // フェードアウト中にボタン無効化
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

    // ボタンの有効/無効を設定
    private void SetButtonsInteractable(bool interactable)
    {
        foreach (Button button in allButtons)
        {
            button.interactable = interactable;
        }
    }
}