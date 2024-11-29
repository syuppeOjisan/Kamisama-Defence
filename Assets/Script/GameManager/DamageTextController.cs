using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    public Animator animator; // Animatorをアサイン

    // ダメージを表示する際に呼び出すメソッド
    public void ShowDamage()
    {
        if (animator != null)
        {
            animator.SetTrigger("ShowDamage"); // Triggerを設定してアニメーションを再生
        }
    }
}