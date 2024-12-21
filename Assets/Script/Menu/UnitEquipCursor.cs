using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEquipCursor : MonoBehaviour
{
    public RectTransform cursor; // カーソルのRectTransform
    public Canvas canvas; // Canvasを指定
    public float cursorSpeed = 300f; // カーソルの移動速度

    private Vector2 cursorPosition; // カーソルの現在位置
    private GameObject lastHoveredObject = null; // 最後にホバーされたオブジェクト

    void Start()
    {
        cursorPosition = cursor.anchoredPosition; // 初期位置を取得
    }

    void Update()
    {
        HandleCursorMovement();
        HandleHoverEvents();
        HandleSelection();
    }

    // カーソル移動処理
    void HandleCursorMovement()
    {
        float moveX = Input.GetAxis("CursorHorizontal");
        float moveY = Input.GetAxis("CursorVertical");

        Vector2 movement = new Vector2(moveX, moveY) * cursorSpeed * Time.deltaTime;
        cursorPosition += movement;

        // カーソルの範囲制限
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 minPosition = Vector2.zero;
        Vector2 maxPosition = canvasRect.sizeDelta;

        cursorPosition.x = Mathf.Clamp(cursorPosition.x, minPosition.x, maxPosition.x);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, minPosition.y, maxPosition.y);

        cursor.anchoredPosition = cursorPosition;

    }

    // カーソルホバー処理
    void HandleHoverEvents()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = cursor.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        GameObject hoveredObject = null;

        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<Toggle>() || result.gameObject.GetComponent<Button>())
            {
                hoveredObject = result.gameObject;

                if (lastHoveredObject != hoveredObject)
                {
                    ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerEnterHandler);
                }
                break;
            }
        }

        if (lastHoveredObject != null && lastHoveredObject != hoveredObject)
        {
            ExecuteEvents.Execute(lastHoveredObject, pointerData, ExecuteEvents.pointerExitHandler);
        }

        lastHoveredObject = hoveredObject;
    }

    // カーソル選択処理
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
                Toggle toggle = result.gameObject.GetComponent<Toggle>();
                Button button = result.gameObject.GetComponent<Button>();

                Debug.Log("押されたボタン:" + result.gameObject.name);

                if (toggle != null && toggle.interactable)
                {
                    toggle.isOn = !toggle.isOn;
                    ExecuteEvents.Execute(toggle.gameObject, pointerData, ExecuteEvents.submitHandler);
                    return;
                }

                if (button != null && button.interactable)
                {
                    button.onClick.Invoke();
                    return;
                }
            }
        }
    }
}