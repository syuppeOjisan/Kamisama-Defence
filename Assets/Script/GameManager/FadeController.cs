using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; // �t�F�[�h�G�t�F�N�g�p��Image
    public float fadeDuration = 1f; // �t�F�[�h�ɂ����鎞��

    private void Awake()
    {
        // �t�F�[�h�p��Image���ݒ肳��Ă��Ȃ��ꍇ�͎����ŒT��
        if (fadeImage == null)
        {
            fadeImage = GetComponentInChildren<Image>();
        }

        // ������ԂŃt�F�[�h�C��
        StartCoroutine(FadeIn());
    }

    // �t�F�[�h�C�������i��ʂ𖾂邭����j
    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 1f; // �t�F�[�h�C���̏����l�͊��S�ɈÂ����
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - (elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f; // ���S�ɖ��邢��Ԃɐݒ�
        fadeImage.color = color;
    }

    // �t�F�[�h�A�E�g�����i��ʂ��Â�����j
    public IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f; // �t�F�[�h�A�E�g�̏����l�͊��S�ɖ��邢���
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = elapsedTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f; // ���S�ɈÂ���Ԃɐݒ�
        fadeImage.color = color;

        // �V�[�������[�h
        SceneManager.LoadScene(sceneName);
    }
}