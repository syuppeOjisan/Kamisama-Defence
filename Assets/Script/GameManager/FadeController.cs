using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; // フェードエフェクト用のImage
    public float fadeDuration = 1f; // フェードにかかる時間

    private void Awake()
    {
        // フェード用のImageが設定されていない場合は自動で探す
        if (fadeImage == null)
        {
            fadeImage = GetComponentInChildren<Image>();
        }

        // 初期状態でフェードイン
        StartCoroutine(FadeIn());
    }

    // フェードイン処理（画面を明るくする）
    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 1f; // フェードインの初期値は完全に暗い状態
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - (elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f; // 完全に明るい状態に設定
        fadeImage.color = color;
    }

    // フェードアウト処理（画面を暗くする）
    public IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f; // フェードアウトの初期値は完全に明るい状態
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = elapsedTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f; // 完全に暗い状態に設定
        fadeImage.color = color;

        // シーンをロード
        SceneManager.LoadScene(sceneName);
    }
}