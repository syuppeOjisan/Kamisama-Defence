using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro���g�p

public class DamageText : MonoBehaviour
{
    public float moveSpeed = 1f; // �_���[�W�\������Ɉړ����鑬�x
    public float fadeDuration = 1f; // �_���[�W�\���̃t�F�[�h�A�E�g����
    private TMP_Text damageText; // TextMeshPro�R���|�[�l���g

    private Color textColor;

    void Start()
    {
        damageText = GetComponent<TMP_Text>();
        textColor = damageText.color;
    }

    void Update()
    {
        // �_���[�W�\������Ɉړ�
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // ���Ԍo�߂ɔ����ăt�F�[�h�A�E�g
        textColor.a -= Time.deltaTime / fadeDuration;
        damageText.color = textColor;

        // ���S�ɓ����ɂȂ�����I�u�W�F�N�g���폜
        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    // �_���[�W��ݒ�
    public void SetDamageText(float damage)
    {
        damageText.text = damage.ToString();
    }
}