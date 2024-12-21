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
    public Sprite character3DefaultPortrait;
    public Sprite character3StunnedPortrait;
    public Sprite character4DefaultPortrait;
    public Sprite character4StunnedPortrait;
    public List<string> character1StunnedSpeeches;
    public List<string> character2StunnedSpeeches;
    public List<string> character3StunnedSpeeches;
    public List<string> character4StunnedSpeeches;

    // DefenseUnit 用
    public Sprite defenseUnitDefaultPortraitCharacter1;
    public Sprite defenseUnitUpgradedPortraitCharacter1;
    public List<string> defenseUnitPlacementSpeechesCharacter1;
    public List<string> defenseUnitUpgradedSpeechesCharacter1;

    public Sprite defenseUnitDefaultPortraitCharacter2;
    public Sprite defenseUnitUpgradedPortraitCharacter2;
    public List<string> defenseUnitPlacementSpeechesCharacter2;
    public List<string> defenseUnitUpgradedSpeechesCharacter2;

    public Sprite defenseUnitDefaultPortraitCharacter3;
    public Sprite defenseUnitUpgradedPortraitCharacter3;
    public List<string> defenseUnitPlacementSpeechesCharacter3;
    public List<string> defenseUnitUpgradedSpeechesCharacter3;

    public Sprite defenseUnitDefaultPortraitCharacter4;
    public Sprite defenseUnitUpgradedPortraitCharacter4;
    public List<string> defenseUnitPlacementSpeechesCharacter4;
    public List<string> defenseUnitUpgradedSpeechesCharacter4;

    // FenceUnit 用
    public Sprite fenceUnitDefaultPortraitCharacter1;
    public Sprite fenceUnitUpgradedPortraitCharacter1;
    public List<string> fenceUnitPlacementSpeechesCharacter1;
    public List<string> fenceUnitUpgradedSpeechesCharacter1;

    public Sprite fenceUnitDefaultPortraitCharacter2;
    public Sprite fenceUnitUpgradedPortraitCharacter2;
    public List<string> fenceUnitPlacementSpeechesCharacter2;
    public List<string> fenceUnitUpgradedSpeechesCharacter2;

    public Sprite fenceUnitDefaultPortraitCharacter3;
    public Sprite fenceUnitUpgradedPortraitCharacter3;
    public List<string> fenceUnitPlacementSpeechesCharacter3;
    public List<string> fenceUnitUpgradedSpeechesCharacter3;

    public Sprite fenceUnitDefaultPortraitCharacter4;
    public Sprite fenceUnitUpgradedPortraitCharacter4;
    public List<string> fenceUnitPlacementSpeechesCharacter4;
    public List<string> fenceUnitUpgradedSpeechesCharacter4;

    // SpikeUnit 用
    public Sprite spikeUnitDefaultPortraitCharacter1;
    public Sprite spikeUnitUpgradedPortraitCharacter1;
    public List<string> spikeUnitPlacementSpeechesCharacter1;
    public List<string> spikeUnitUpgradedSpeechesCharacter1;

    public Sprite spikeUnitDefaultPortraitCharacter2;
    public Sprite spikeUnitUpgradedPortraitCharacter2;
    public List<string> spikeUnitPlacementSpeechesCharacter2;
    public List<string> spikeUnitUpgradedSpeechesCharacter2;

    public Sprite spikeUnitDefaultPortraitCharacter3;
    public Sprite spikeUnitUpgradedPortraitCharacter3;
    public List<string> spikeUnitPlacementSpeechesCharacter3;
    public List<string> spikeUnitUpgradedSpeechesCharacter3;

    public Sprite spikeUnitDefaultPortraitCharacter4;
    public Sprite spikeUnitUpgradedPortraitCharacter4;
    public List<string> spikeUnitPlacementSpeechesCharacter4;
    public List<string> spikeUnitUpgradedSpeechesCharacter4;

    // CaltropUnit 用
    public Sprite caltropUnitDefaultPortraitCharacter1;
    public Sprite caltropUnitUpgradedPortraitCharacter1;
    public List<string> caltropUnitPlacementSpeechesCharacter1;
    public List<string> caltropUnitUpgradedSpeechesCharacter1;

    public Sprite caltropUnitDefaultPortraitCharacter2;
    public Sprite caltropUnitUpgradedPortraitCharacter2;
    public List<string> caltropUnitPlacementSpeechesCharacter2;
    public List<string> caltropUnitUpgradedSpeechesCharacter2;

    public Sprite caltropUnitDefaultPortraitCharacter3;
    public Sprite caltropUnitUpgradedPortraitCharacter3;
    public List<string> caltropUnitPlacementSpeechesCharacter3;
    public List<string> caltropUnitUpgradedSpeechesCharacter3;

    public Sprite caltropUnitDefaultPortraitCharacter4;
    public Sprite caltropUnitUpgradedPortraitCharacter4;
    public List<string> caltropUnitPlacementSpeechesCharacter4;
    public List<string> caltropUnitUpgradedSpeechesCharacter4;

    // FlameTrapUnit 用
    public Sprite flameTrapUnitDefaultPortraitCharacter1;
    public Sprite flameTrapUnitUpgradedPortraitCharacter1;
    public List<string> flameTrapUnitPlacementSpeechesCharacter1;
    public List<string> flameTrapUnitUpgradedSpeechesCharacter1;

    public Sprite flameTrapUnitDefaultPortraitCharacter2;
    public Sprite flameTrapUnitUpgradedPortraitCharacter2;
    public List<string> flameTrapUnitPlacementSpeechesCharacter2;
    public List<string> flameTrapUnitUpgradedSpeechesCharacter2;

    public Sprite flameTrapUnitDefaultPortraitCharacter3;
    public Sprite flameTrapUnitUpgradedPortraitCharacter3;
    public List<string> flameTrapUnitPlacementSpeechesCharacter3;
    public List<string> flameTrapUnitUpgradedSpeechesCharacter3;

    public Sprite flameTrapUnitDefaultPortraitCharacter4;
    public Sprite flameTrapUnitUpgradedPortraitCharacter4;
    public List<string> flameTrapUnitPlacementSpeechesCharacter4;
    public List<string> flameTrapUnitUpgradedSpeechesCharacter4;

    // ShrineUnit 用
    public Sprite shrineUnitDefaultPortraitCharacter1;
    public Sprite shrineUnitUpgradedPortraitCharacter1;
    public List<string> shrineUnitPlacementSpeechesCharacter1;
    public List<string> shrineUnitUpgradedSpeechesCharacter1;

    public Sprite shrineUnitDefaultPortraitCharacter2;
    public Sprite shrineUnitUpgradedPortraitCharacter2;
    public List<string> shrineUnitPlacementSpeechesCharacter2;
    public List<string> shrineUnitUpgradedSpeechesCharacter2;

    public Sprite shrineUnitDefaultPortraitCharacter3;
    public Sprite shrineUnitUpgradedPortraitCharacter3;
    public List<string> shrineUnitPlacementSpeechesCharacter3;
    public List<string> shrineUnitUpgradedSpeechesCharacter3;

    public Sprite shrineUnitDefaultPortraitCharacter4;
    public Sprite shrineUnitUpgradedPortraitCharacter4;
    public List<string> shrineUnitPlacementSpeechesCharacter4;
    public List<string> shrineUnitUpgradedSpeechesCharacter4;

    // BearTrapUnit 用
    public Sprite bearTrapUnitDefaultPortraitCharacter1;
    public Sprite bearTrapUnitUpgradedPortraitCharacter1;
    public List<string> bearTrapUnitPlacementSpeechesCharacter1;
    public List<string> bearTrapUnitUpgradedSpeechesCharacter1;

    public Sprite bearTrapUnitDefaultPortraitCharacter2;
    public Sprite bearTrapUnitUpgradedPortraitCharacter2;
    public List<string> bearTrapUnitPlacementSpeechesCharacter2;
    public List<string> bearTrapUnitUpgradedSpeechesCharacter2;

    public Sprite bearTrapUnitDefaultPortraitCharacter3;
    public Sprite bearTrapUnitUpgradedPortraitCharacter3;
    public List<string> bearTrapUnitPlacementSpeechesCharacter3;
    public List<string> bearTrapUnitUpgradedSpeechesCharacter3;

    public Sprite bearTrapUnitDefaultPortraitCharacter4;
    public Sprite bearTrapUnitUpgradedPortraitCharacter4;
    public List<string> bearTrapUnitPlacementSpeechesCharacter4;
    public List<string> bearTrapUnitUpgradedSpeechesCharacter4;

    // WaterStationUnit 用
    public Sprite waterStationUnitDefaultPortraitCharacter1;
    public Sprite waterStationUnitUpgradedPortraitCharacter1;
    public List<string> waterStationUnitPlacementSpeechesCharacter1;
    public List<string> waterStationUnitUpgradedSpeechesCharacter1;

    public Sprite waterStationUnitDefaultPortraitCharacter2;
    public Sprite waterStationUnitUpgradedPortraitCharacter2;
    public List<string> waterStationUnitPlacementSpeechesCharacter2;
    public List<string> waterStationUnitUpgradedSpeechesCharacter2;

    public Sprite waterStationUnitDefaultPortraitCharacter3;
    public Sprite waterStationUnitUpgradedPortraitCharacter3;
    public List<string> waterStationUnitPlacementSpeechesCharacter3;
    public List<string> waterStationUnitUpgradedSpeechesCharacter3;

    public Sprite waterStationUnitDefaultPortraitCharacter4;
    public Sprite waterStationUnitUpgradedPortraitCharacter4;
    public List<string> waterStationUnitPlacementSpeechesCharacter4;
    public List<string> waterStationUnitUpgradedSpeechesCharacter4;

    // SlowUnit1 用
    public Sprite slowUnit1DefaultPortraitCharacter1;
    public Sprite slowUnit1UpgradedPortraitCharacter1;
    public List<string> slowUnit1PlacementSpeechesCharacter1;
    public List<string> slowUnit1UpgradedSpeechesCharacter1;

    public Sprite slowUnit1DefaultPortraitCharacter2;
    public Sprite slowUnit1UpgradedPortraitCharacter2;
    public List<string> slowUnit1PlacementSpeechesCharacter2;
    public List<string> slowUnit1UpgradedSpeechesCharacter2;

    public Sprite slowUnit1DefaultPortraitCharacter3;
    public Sprite slowUnit1UpgradedPortraitCharacter3;
    public List<string> slowUnit1PlacementSpeechesCharacter3;
    public List<string> slowUnit1UpgradedSpeechesCharacter3;

    public Sprite slowUnit1DefaultPortraitCharacter4;
    public Sprite slowUnit1UpgradedPortraitCharacter4;
    public List<string> slowUnit1PlacementSpeechesCharacter4;
    public List<string> slowUnit1UpgradedSpeechesCharacter4;

    // SlowUnit2 用
    public Sprite slowUnit2DefaultPortraitCharacter1;
    public Sprite slowUnit2UpgradedPortraitCharacter1;
    public List<string> slowUnit2PlacementSpeechesCharacter1;
    public List<string> slowUnit2UpgradedSpeechesCharacter1;

    public Sprite slowUnit2DefaultPortraitCharacter2;
    public Sprite slowUnit2UpgradedPortraitCharacter2;
    public List<string> slowUnit2PlacementSpeechesCharacter2;
    public List<string> slowUnit2UpgradedSpeechesCharacter2;

    public Sprite slowUnit2DefaultPortraitCharacter3;
    public Sprite slowUnit2UpgradedPortraitCharacter3;
    public List<string> slowUnit2PlacementSpeechesCharacter3;
    public List<string> slowUnit2UpgradedSpeechesCharacter3;

    public Sprite slowUnit2DefaultPortraitCharacter4;
    public Sprite slowUnit2UpgradedPortraitCharacter4;
    public List<string> slowUnit2PlacementSpeechesCharacter4;
    public List<string> slowUnit2UpgradedSpeechesCharacter4;

    // SlowUnit3 用
    public Sprite slowUnit3DefaultPortraitCharacter1;
    public Sprite slowUnit3UpgradedPortraitCharacter1;
    public List<string> slowUnit3PlacementSpeechesCharacter1;
    public List<string> slowUnit3UpgradedSpeechesCharacter1;

    public Sprite slowUnit3DefaultPortraitCharacter2;
    public Sprite slowUnit3UpgradedPortraitCharacter2;
    public List<string> slowUnit3PlacementSpeechesCharacter2;
    public List<string> slowUnit3UpgradedSpeechesCharacter2;

    public Sprite slowUnit3DefaultPortraitCharacter3;
    public Sprite slowUnit3UpgradedPortraitCharacter3;
    public List<string> slowUnit3PlacementSpeechesCharacter3;
    public List<string> slowUnit3UpgradedSpeechesCharacter3;

    public Sprite slowUnit3DefaultPortraitCharacter4;
    public Sprite slowUnit3UpgradedPortraitCharacter4;
    public List<string> slowUnit3PlacementSpeechesCharacter4;
    public List<string> slowUnit3UpgradedSpeechesCharacter4;

    // MagicCircleUnit 用
    public Sprite MagicCircleUnitDefaultPortraitCharacter1;
    public Sprite MagicCircleUnitUpgradedPortraitCharacter1;
    public List<string> MagicCircleUnitPlacementSpeechesCharacter1;
    public List<string> MagicCircleUnitUpgradedSpeechesCharacter1;

    public Sprite MagicCircleUnitDefaultPortraitCharacter2;
    public Sprite MagicCircleUnitUpgradedPortraitCharacter2;
    public List<string> MagicCircleUnitPlacementSpeechesCharacter2;
    public List<string> MagicCircleUnitUpgradedSpeechesCharacter2;

    public Sprite MagicCircleUnitDefaultPortraitCharacter3;
    public Sprite MagicCircleUnitUpgradedPortraitCharacter3;
    public List<string> MagicCircleUnitPlacementSpeechesCharacter3;
    public List<string> MagicCircleUnitUpgradedSpeechesCharacter3;

    public Sprite MagicCircleUnitDefaultPortraitCharacter4;
    public Sprite MagicCircleUnitUpgradedPortraitCharacter4;
    public List<string> MagicCircleUnitPlacementSpeechesCharacter4;
    public List<string> MagicCircleUnitUpgradedSpeechesCharacter4;

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
        else if (selectedCharacter == 3)
        {
            defaultPortrait = character3DefaultPortrait;
            stunnedPortrait = character3StunnedPortrait;
            stunnedSpeeches = character3StunnedSpeeches;
        }
        else if (selectedCharacter == 4)
        {
            defaultPortrait = character4DefaultPortrait;
            stunnedPortrait = character4StunnedPortrait;
            stunnedSpeeches = character4StunnedSpeeches;
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = fenceUnitDefaultPortraitCharacter3;
            SetRandomSpeech(fenceUnitPlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = fenceUnitDefaultPortraitCharacter4;
            SetRandomSpeech(fenceUnitPlacementSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = fenceUnitUpgradedPortraitCharacter3;
            SetRandomSpeech(fenceUnitUpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = fenceUnitUpgradedPortraitCharacter4;
            SetRandomSpeech(fenceUnitUpgradedSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = defenseUnitDefaultPortraitCharacter3;
            SetRandomSpeech(defenseUnitPlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = defenseUnitDefaultPortraitCharacter4;
            SetRandomSpeech(defenseUnitPlacementSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = defenseUnitUpgradedPortraitCharacter3;
            SetRandomSpeech(defenseUnitUpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = defenseUnitUpgradedPortraitCharacter4;
            SetRandomSpeech(defenseUnitUpgradedSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = spikeUnitDefaultPortraitCharacter3;
            SetRandomSpeech(spikeUnitPlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = spikeUnitDefaultPortraitCharacter4;
            SetRandomSpeech(spikeUnitPlacementSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = spikeUnitUpgradedPortraitCharacter3;
            SetRandomSpeech(spikeUnitUpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = spikeUnitUpgradedPortraitCharacter4;
            SetRandomSpeech(spikeUnitUpgradedSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = caltropUnitDefaultPortraitCharacter3;
            SetRandomSpeech(caltropUnitPlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = caltropUnitDefaultPortraitCharacter4;
            SetRandomSpeech(caltropUnitPlacementSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = caltropUnitUpgradedPortraitCharacter3;
            SetRandomSpeech(caltropUnitUpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = caltropUnitUpgradedPortraitCharacter4;
            SetRandomSpeech(caltropUnitUpgradedSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = flameTrapUnitDefaultPortraitCharacter3;
            SetRandomSpeech(flameTrapUnitPlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = flameTrapUnitDefaultPortraitCharacter4;
            SetRandomSpeech(flameTrapUnitPlacementSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = flameTrapUnitUpgradedPortraitCharacter3;
            SetRandomSpeech(flameTrapUnitUpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = flameTrapUnitUpgradedPortraitCharacter4;
            SetRandomSpeech(flameTrapUnitUpgradedSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = shrineUnitDefaultPortraitCharacter3;
            SetRandomSpeech(shrineUnitPlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = shrineUnitDefaultPortraitCharacter4;
            SetRandomSpeech(shrineUnitPlacementSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = shrineUnitUpgradedPortraitCharacter3;
            SetRandomSpeech(shrineUnitUpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = shrineUnitUpgradedPortraitCharacter4;
            SetRandomSpeech(shrineUnitUpgradedSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = bearTrapUnitDefaultPortraitCharacter3;
            SetRandomSpeech(bearTrapUnitPlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = bearTrapUnitDefaultPortraitCharacter4;
            SetRandomSpeech(bearTrapUnitPlacementSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = bearTrapUnitUpgradedPortraitCharacter3;
            SetRandomSpeech(bearTrapUnitUpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = bearTrapUnitUpgradedPortraitCharacter4;
            SetRandomSpeech(bearTrapUnitUpgradedSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = waterStationUnitDefaultPortraitCharacter3;
            SetRandomSpeech(waterStationUnitPlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = waterStationUnitDefaultPortraitCharacter4;
            SetRandomSpeech(waterStationUnitPlacementSpeechesCharacter4);
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
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = waterStationUnitUpgradedPortraitCharacter3;
            SetRandomSpeech(waterStationUnitUpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = waterStationUnitUpgradedPortraitCharacter4;
            SetRandomSpeech(waterStationUnitUpgradedSpeechesCharacter4);
        }
        ResetDefaultTimer();
    }

    public void SetSlowUnit1Placement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = slowUnit1DefaultPortraitCharacter1;
            SetRandomSpeech(slowUnit1PlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = slowUnit1DefaultPortraitCharacter2;
            SetRandomSpeech(slowUnit1PlacementSpeechesCharacter2);
        }
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = slowUnit1DefaultPortraitCharacter3;
            SetRandomSpeech(slowUnit1PlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = slowUnit1DefaultPortraitCharacter4;
            SetRandomSpeech(slowUnit1PlacementSpeechesCharacter4);
        }
        ResetDefaultTimer();
    }

    public void SetSlowUnit1Upgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = slowUnit1UpgradedPortraitCharacter1;
            SetRandomSpeech(slowUnit1UpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = slowUnit1UpgradedPortraitCharacter2;
            SetRandomSpeech(slowUnit1UpgradedSpeechesCharacter2);
        }
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = slowUnit1UpgradedPortraitCharacter3;
            SetRandomSpeech(slowUnit1UpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = slowUnit1UpgradedPortraitCharacter4;
            SetRandomSpeech(slowUnit1UpgradedSpeechesCharacter4);
        }
        ResetDefaultTimer();
    }

    public void SetSlowUnit2Placement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = slowUnit2DefaultPortraitCharacter1;
            SetRandomSpeech(slowUnit2PlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = slowUnit2DefaultPortraitCharacter2;
            SetRandomSpeech(slowUnit2PlacementSpeechesCharacter2);
        }
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = slowUnit2DefaultPortraitCharacter3;
            SetRandomSpeech(slowUnit2PlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = slowUnit2DefaultPortraitCharacter4;
            SetRandomSpeech(slowUnit2PlacementSpeechesCharacter4);
        }
        ResetDefaultTimer();
    }

    public void SetSlowUnit2Upgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = slowUnit2UpgradedPortraitCharacter1;
            SetRandomSpeech(slowUnit2UpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = slowUnit2UpgradedPortraitCharacter2;
            SetRandomSpeech(slowUnit2UpgradedSpeechesCharacter2);
        }
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = slowUnit2UpgradedPortraitCharacter3;
            SetRandomSpeech(slowUnit2UpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = slowUnit2UpgradedPortraitCharacter4;
            SetRandomSpeech(slowUnit2UpgradedSpeechesCharacter4);
        }
        ResetDefaultTimer();
    }

    public void SetSlowUnit3Placement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = slowUnit3DefaultPortraitCharacter1;
            SetRandomSpeech(slowUnit3PlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = slowUnit3DefaultPortraitCharacter2;
            SetRandomSpeech(slowUnit3PlacementSpeechesCharacter2);
        }
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = slowUnit3DefaultPortraitCharacter3;
            SetRandomSpeech(slowUnit3PlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = slowUnit3DefaultPortraitCharacter4;
            SetRandomSpeech(slowUnit3PlacementSpeechesCharacter4);
        }
        ResetDefaultTimer();
    }

    public void SetSlowUnit3Upgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = slowUnit3UpgradedPortraitCharacter1;
            SetRandomSpeech(slowUnit3UpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = slowUnit3UpgradedPortraitCharacter2;
            SetRandomSpeech(slowUnit3UpgradedSpeechesCharacter2);
        }
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = slowUnit3UpgradedPortraitCharacter3;
            SetRandomSpeech(slowUnit3UpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = slowUnit3UpgradedPortraitCharacter4;
            SetRandomSpeech(slowUnit3UpgradedSpeechesCharacter4);
        }
        ResetDefaultTimer();
    }

    public void SetMagicCircleUnitPlacement()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = MagicCircleUnitDefaultPortraitCharacter1;
            SetRandomSpeech(MagicCircleUnitPlacementSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = MagicCircleUnitDefaultPortraitCharacter2;
            SetRandomSpeech(MagicCircleUnitPlacementSpeechesCharacter2);
        }
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = MagicCircleUnitDefaultPortraitCharacter3;
            SetRandomSpeech(MagicCircleUnitPlacementSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = MagicCircleUnitDefaultPortraitCharacter4;
            SetRandomSpeech(MagicCircleUnitPlacementSpeechesCharacter4);
        }
        ResetDefaultTimer();
    }

    public void SetMagicCircleUnitUpgraded()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        if (selectedCharacter == 1)
        {
            characterPortrait.sprite = MagicCircleUnitUpgradedPortraitCharacter1;
            SetRandomSpeech(MagicCircleUnitUpgradedSpeechesCharacter1);
        }
        else if (selectedCharacter == 2)
        {
            characterPortrait.sprite = MagicCircleUnitUpgradedPortraitCharacter2;
            SetRandomSpeech(MagicCircleUnitUpgradedSpeechesCharacter2);
        }
        else if (selectedCharacter == 3)
        {
            characterPortrait.sprite = MagicCircleUnitUpgradedPortraitCharacter3;
            SetRandomSpeech(MagicCircleUnitUpgradedSpeechesCharacter3);
        }
        else if (selectedCharacter == 4)
        {
            characterPortrait.sprite = MagicCircleUnitUpgradedPortraitCharacter4;
            SetRandomSpeech(MagicCircleUnitUpgradedSpeechesCharacter4);
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