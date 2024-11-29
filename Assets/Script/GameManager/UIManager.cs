using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public Image characterPortrait; // キャラクターの立ち絵画像
    public TMP_Text speechText; // セリフ表示用のTextMeshProコンポーネント

    // プレイヤーキャラクター用
    public Sprite character1DefaultPortrait;
    public Sprite character1StunnedPortrait;
    public Sprite character2DefaultPortrait;
    public Sprite character2StunnedPortrait;
    public List<string> character1StunnedSpeeches;
    public List<string> character2StunnedSpeeches;

    // DefenseUnit 用
    public Sprite defenseUnitDefaultPortraitCharacter1;
    public Sprite defenseUnitUpgradedPortraitCharacter1;
    public List<string> defenseUnitPlacementSpeechesCharacter1;
    public List<string> defenseUnitUpgradedSpeechesCharacter1;

    public Sprite defenseUnitDefaultPortraitCharacter2;
    public Sprite defenseUnitUpgradedPortraitCharacter2;
    public List<string> defenseUnitPlacementSpeechesCharacter2;
    public List<string> defenseUnitUpgradedSpeechesCharacter2;

    // FenceUnit 用
    public Sprite fenceUnitDefaultPortraitCharacter1;
    public Sprite fenceUnitUpgradedPortraitCharacter1;
    public List<string> fenceUnitPlacementSpeechesCharacter1;
    public List<string> fenceUnitUpgradedSpeechesCharacter1;

    public Sprite fenceUnitDefaultPortraitCharacter2;
    public Sprite fenceUnitUpgradedPortraitCharacter2;
    public List<string> fenceUnitPlacementSpeechesCharacter2;
    public List<string> fenceUnitUpgradedSpeechesCharacter2;

    // SpikeUnit 用
    public Sprite spikeUnitDefaultPortraitCharacter1;
    public Sprite spikeUnitUpgradedPortraitCharacter1;
    public List<string> spikeUnitPlacementSpeechesCharacter1;
    public List<string> spikeUnitUpgradedSpeechesCharacter1;

    public Sprite spikeUnitDefaultPortraitCharacter2;
    public Sprite spikeUnitUpgradedPortraitCharacter2;
    public List<string> spikeUnitPlacementSpeechesCharacter2;
    public List<string> spikeUnitUpgradedSpeechesCharacter2;

    // CaltropUnit 用
    public Sprite caltropUnitDefaultPortraitCharacter1;
    public Sprite caltropUnitUpgradedPortraitCharacter1;
    public List<string> caltropUnitPlacementSpeechesCharacter1;
    public List<string> caltropUnitUpgradedSpeechesCharacter1;

    public Sprite caltropUnitDefaultPortraitCharacter2;
    public Sprite caltropUnitUpgradedPortraitCharacter2;
    public List<string> caltropUnitPlacementSpeechesCharacter2;
    public List<string> caltropUnitUpgradedSpeechesCharacter2;

    // FlameTrapUnit 用
    public Sprite flameTrapUnitDefaultPortraitCharacter1;
    public Sprite flameTrapUnitUpgradedPortraitCharacter1;
    public List<string> flameTrapUnitPlacementSpeechesCharacter1;
    public List<string> flameTrapUnitUpgradedSpeechesCharacter1;

    public Sprite flameTrapUnitDefaultPortraitCharacter2;
    public Sprite flameTrapUnitUpgradedPortraitCharacter2;
    public List<string> flameTrapUnitPlacementSpeechesCharacter2;
    public List<string> flameTrapUnitUpgradedSpeechesCharacter2;

    // ShrineUnit 用
    public Sprite shrineUnitDefaultPortraitCharacter1;
    public Sprite shrineUnitUpgradedPortraitCharacter1;
    public List<string> shrineUnitPlacementSpeechesCharacter1;
    public List<string> shrineUnitUpgradedSpeechesCharacter1;

    public Sprite shrineUnitDefaultPortraitCharacter2;
    public Sprite shrineUnitUpgradedPortraitCharacter2;
    public List<string> shrineUnitPlacementSpeechesCharacter2;
    public List<string> shrineUnitUpgradedSpeechesCharacter2;

    // BearTrapUnit 用
    public Sprite bearTrapUnitDefaultPortraitCharacter1;
    public Sprite bearTrapUnitUpgradedPortraitCharacter1;
    public List<string> bearTrapUnitPlacementSpeechesCharacter1;
    public List<string> bearTrapUnitUpgradedSpeechesCharacter1;

    public Sprite bearTrapUnitDefaultPortraitCharacter2;
    public Sprite bearTrapUnitUpgradedPortraitCharacter2;
    public List<string> bearTrapUnitPlacementSpeechesCharacter2;
    public List<string> bearTrapUnitUpgradedSpeechesCharacter2;

    // WaterStationUnit 用
    public Sprite waterStationUnitDefaultPortraitCharacter1;
    public Sprite waterStationUnitUpgradedPortraitCharacter1;
    public List<string> waterStationUnitPlacementSpeechesCharacter1;
    public List<string> waterStationUnitUpgradedSpeechesCharacter1;

    public Sprite waterStationUnitDefaultPortraitCharacter2;
    public Sprite waterStationUnitUpgradedPortraitCharacter2;
    public List<string> waterStationUnitPlacementSpeechesCharacter2;
    public List<string> waterStationUnitUpgradedSpeechesCharacter2;

    // SlowUnit 用
    public Sprite slowUnitDefaultPortraitCharacter1;
    public Sprite slowUnitUpgradedPortraitCharacter1;
    public List<string> slowUnitPlacementSpeechesCharacter1;
    public List<string> slowUnitUpgradedSpeechesCharacter1;

    public Sprite slowUnitDefaultPortraitCharacter2;
    public Sprite slowUnitUpgradedPortraitCharacter2;
    public List<string> slowUnitPlacementSpeechesCharacter2;
    public List<string> slowUnitUpgradedSpeechesCharacter2;

    private Sprite defaultPortrait;
    private Sprite stunnedPortrait;
    private List<string> stunnedSpeeches;

    private bool isStunned = false; // スタン中かどうかを示すフラグ
    private float resetTimer = 0f; // デフォルトに戻すためのタイマー
    private const float resetDelay = 3f; // デフォルトに戻すまでの待機時間

    public string defaultSpeech = ""; // デフォルト時のセリフ

    void Start()
    {
        SetPlayerCharacterAssets();
        SetDefaultSpeech();
    }

    private void Update()
    {
        // スタン中でない場合、タイマーを更新
        if (!isStunned && resetTimer > 0f)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0f)
            {
                SetDefaultPortraitAndSpeech();
            }
        }
    }

    private void SetPlayerCharacterAssets()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");

        if (selectedCharacter == 1)
        {
            defaultPortrait = character1DefaultPortrait;
            stunnedPortrait = character1StunnedPortrait;
            stunnedSpeeches = character1StunnedSpeeches;
        }
        else if (selectedCharacter == 2)
        {
            defaultPortrait = character2DefaultPortrait;
            stunnedPortrait = character2StunnedPortrait;
            stunnedSpeeches = character2StunnedSpeeches;
        }

        SetDefaultPortrait();
    }

    public void SetDefaultPortrait()
    {
        characterPortrait.sprite = defaultPortrait;
        SetDefaultSpeech();
    }

    public void SetDefaultPortraitAndSpeech()
    {
        if (!isStunned) // スタン中でない場合のみデフォルトに戻す
        {
            SetDefaultPortrait();
        }
    }

    public void SetFenceUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = fenceUnitDefaultPortraitCharacter1;
            SetRandomSpeech(fenceUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = fenceUnitDefaultPortraitCharacter2;
            SetRandomSpeech(fenceUnitPlacementSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetFenceUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = fenceUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(fenceUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = fenceUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(fenceUnitUpgradedSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetDefenseUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = defenseUnitDefaultPortraitCharacter1;
            SetRandomSpeech(defenseUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = defenseUnitDefaultPortraitCharacter2;
            SetRandomSpeech(defenseUnitPlacementSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetDefenseUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = defenseUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(defenseUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = defenseUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(defenseUnitUpgradedSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetSpikeUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = spikeUnitDefaultPortraitCharacter1;
            SetRandomSpeech(spikeUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = spikeUnitDefaultPortraitCharacter2;
            SetRandomSpeech(spikeUnitPlacementSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetSpikeUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = spikeUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(spikeUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = spikeUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(spikeUnitUpgradedSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetCaltropUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = caltropUnitDefaultPortraitCharacter1;
            SetRandomSpeech(caltropUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = caltropUnitDefaultPortraitCharacter2;
            SetRandomSpeech(caltropUnitPlacementSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetCaltropUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = caltropUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(caltropUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = caltropUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(caltropUnitUpgradedSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetFlameTrapUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = flameTrapUnitDefaultPortraitCharacter1;
            SetRandomSpeech(flameTrapUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = flameTrapUnitDefaultPortraitCharacter2;
            SetRandomSpeech(flameTrapUnitPlacementSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetFlameTrapUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = flameTrapUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(flameTrapUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = flameTrapUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(flameTrapUnitUpgradedSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetShrineUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = shrineUnitDefaultPortraitCharacter1;
            SetRandomSpeech(shrineUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = shrineUnitDefaultPortraitCharacter2;
            SetRandomSpeech(shrineUnitPlacementSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetShrineUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = shrineUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(shrineUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = shrineUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(shrineUnitUpgradedSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetBearTrapUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = bearTrapUnitDefaultPortraitCharacter1;
            SetRandomSpeech(bearTrapUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = bearTrapUnitDefaultPortraitCharacter2;
            SetRandomSpeech(bearTrapUnitPlacementSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetBearTrapUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = bearTrapUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(bearTrapUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = bearTrapUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(bearTrapUnitUpgradedSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetWaterStationUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = waterStationUnitDefaultPortraitCharacter1;
            SetRandomSpeech(waterStationUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = waterStationUnitDefaultPortraitCharacter2;
            SetRandomSpeech(waterStationUnitPlacementSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetWaterStationUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = waterStationUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(waterStationUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = waterStationUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(waterStationUnitUpgradedSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetSlowUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = slowUnitDefaultPortraitCharacter1;
            SetRandomSpeech(slowUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = slowUnitDefaultPortraitCharacter2;
            SetRandomSpeech(slowUnitPlacementSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    public void SetSlowUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = slowUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(slowUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = slowUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(slowUnitUpgradedSpeechesCharacter2);
        }
        ResetDefaultTimer();
    }

    //スタン時
    public void SetStunnedPortrait()
    {
        characterPortrait.sprite = stunnedPortrait;
        SetRandomSpeech(stunnedSpeeches);
        isStunned = true; // スタン中のフラグを有効化
    }

    public void ClearStunnedState()
    {
        isStunned = false; // スタン状態を解除
        SetDefaultPortraitAndSpeech();
    }

    public void SetSpeech(string message)
    {
        if (speechText != null)
        {
            speechText.text = message;
        }
    }

    public void SetDefaultSpeech()
    {
        SetSpeech(defaultSpeech);
    }

    public void SetRandomSpeech(List<string> speeches)
    {
        if (speeches != null && speeches.Count > 0)
        {
            int randomIndex = Random.Range(0, speeches.Count);
            SetSpeech(speeches[randomIndex]);
        }
        else
        {
            Debug.LogWarning("セリフリストが空です。");
        }
    }

    private void ResetDefaultTimer()
    {
        if (!isStunned) // スタン中でない場合のみタイマーをリセット
        {
            resetTimer = resetDelay;
        }
    }
}