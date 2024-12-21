using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectCursor : MonoBehaviour
{
    public RectTransform cursor; // カーソルのRectTransform
    public Canvas canvas; // Canvasを指定
    public float cursorSpeed = 300f; // カーソルの移動速度
    public ScrollRect scrollRect; // スクロールビューのScrollRect
    public RectTransform scrollViewArea; // Scroll Viewエリアの範囲
    public AudioClip hoverSound; // ボタンに乗った時の音
    public AudioClip clickSound; // ボタンを選択した時の音
    public AudioSource audioSource; // 効果音再生用

    private Vector2 cursorPosition; // カーソルの現在位置
    private bool isCursorOverScrollView = false; // カーソルがScroll View上にあるかどうか

    private GameObject lastHoveredButton = null; // 最後にホバーされたボタン

    void Start()
    {
        cursorPosition = cursor.anchoredPosition; // 初期位置を取得
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        HandleCursorMovement();
        HandleSelection();
        HandleScroll();
        HandleButtonHover();
    }

    // カーソル移動処理
    void HandleCursorMovement()
    {
        float moveX = Input.GetAxis("CursorHorizontal");
        float moveY = Input.GetAxis("CursorVertical");

        Vector2 movement = new Vector2(moveX, moveY) * cursorSpeed * Time.deltaTime;
        cursorPosition += movement;

        // カーソルの範囲制限
        Vector2 minPosition = Vector2.zero;
        Vector2 maxPosition = canvas.GetComponent<RectTransform>().sizeDelta;

        cursorPosition.x = Mathf.Clamp(cursorPosition.x, minPosition.x, maxPosition.x);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, minPosition.y, maxPosition.y);

        cursor.anchoredPosition = cursorPosition;
    }

    // Aボタンで決定処理
    void HandleSelection()
    {
        if (Input.GetButtonDown("Submit"))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = cursor.position
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                Button button = result.gameObject.GetComponent<Button>();
                if (button != null && button.interactable) // ボタンが有効な場合のみ処理
                {
                    button.onClick.Invoke();
                    if (clickSound != null)
                    {
                        audioSource.PlayOneShot(clickSound);
                    }
                    return;
                }
            }
        }
    }

    // 右スティックでスクロール処理
    void HandleScroll()
    {
        if (IsCursorOverScrollView())
        {
            float scrollInput = -Input.GetAxis("ScrollVertical");
            if (scrollRect != null)
            {
                scrollRect.verticalNormalizedPosition += scrollInput * Time.deltaTime;
            }
        }
    }

    // カーソルがScroll View上にあるかどうかを判定
    bool IsCursorOverScrollView()
    {
        Vector2 localCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            scrollViewArea, cursor.position, canvas.worldCamera, out localCursor);

        isCursorOverScrollView = scrollViewArea.rect.Contains(localCursor);
        return isCursorOverScrollView;
    }

    // ボタン上にカーソルが乗った時の処理
    void HandleButtonHover()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = cursor.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        GameObject hoveredButton = null;

        foreach (var result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null && button.interactable)
            {
                hoveredButton = button.gameObject;

                // 初めてこのボタンにホバーした場合
                if (lastHoveredButton != hoveredButton)
                {
                    ExecuteEvents.Execute(button.gameObject, pointerData, ExecuteEvents.pointerEnterHandler);
                }
                break;
            }
        }

        // 前回ホバーしていたボタンから離れた場合
        if (lastHoveredButton != null && lastHoveredButton != hoveredButton)
        {
            ExecuteEvents.Execute(lastHoveredButton, pointerData, ExecuteEvents.pointerExitHandler);
        }

        lastHoveredButton = hoveredButton;
    }
}