using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mahojin : MonoBehaviour
{
    public float[] upgradeCosts = new float[5]; // 各レベルのアップグレードコスト
 //   public float[] lifetimes = new float[5];   // 各レベルの持続時間
    public AudioClip upgradeSound;            // アップグレード時の効果音

    public int currentLevel = 1;             // 初期レベルを1に設定
    private bool canWarp = true;              // ワープ可能かを示すフラグ
    public float warpCooldown = 2f;           // クールタイムの秒数
    public float lifetime = 20;                 // 魔法陣の持続時間10
    private AudioSource audioSource;          // 効果音再生用

    void Start()
    {
        // AudioSourceコンポーネントを追加
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        StartCoroutine(Jikai());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && canWarp)
        {
            Debug.Log("敵だ");
           

            // "start"タグのついたオブジェクトを探す
            GameObject startObject = GameObject.FindWithTag("start");

            if (startObject != null)
            {
                Debug.Log("ワープした");
                // "start"タグのオブジェクトの座標を取得
                Vector3 startPosition = startObject.transform.position;

                // Y座標に+20を加える
                Vector3 warpPosition = new Vector3(startPosition.x, startPosition.y + 20.0f, startPosition.z);

                // 敵をワープさせる
                other.transform.position = warpPosition;

                // クールタイムを開始
                StartCoroutine(WarpCooldown());
            }
            else
            {
                Debug.LogError("'start'タグのオブジェクトが見つかりませんでした");
            }
        }
    }

    // クールタイム処理
    private IEnumerator WarpCooldown()
    {
        canWarp = false;
        yield return new WaitForSeconds(warpCooldown);
        canWarp = true;
    }

    private IEnumerator Jikai()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }

    // アップグレードコストを取得
    public float GetUpgradeCost()
    {
        if (currentLevel - 1 < upgradeCosts.Length)
        {
            return upgradeCosts[currentLevel - 1];
        }
        else
        {
            Debug.LogError("アップグレードコストデータが不足しています。");
            return 0f;
        }
    }

    // ユニットのアップグレード
    public bool UpgradeUnit()
    {
        if (currentLevel < upgradeCosts.Length)
        {
            currentLevel++;  // レベルアップ

            // 新しいレベルの持続時間を設定

            lifetime += 10;
            // アップグレードサウンドを再生
            PlayUpgradeSound();

            Debug.Log($"Mahojinがレベルアップしました: Lv{currentLevel}");
            return true;
        }
        else
        {
            Debug.Log("Mahojinは既に最大レベルです。");
            return false;
        }
    }

    // アップグレード時の効果音再生
    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }
}
