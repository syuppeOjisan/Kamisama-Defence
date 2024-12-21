using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEquipCursor : MonoBehaviour
{
    public RectTransform cursor; // �J�[�\����RectTransform
    public Canvas canvas; // Canvas���w��
    public float cursorSpeed = 300f; // �J�[�\���̈ړ����x

    private Vector2 cursorPosition; // �J�[�\���̌��݈ʒu
    private GameObject lastHoveredObject = null; // �Ō�Ƀz�o�[���ꂽ�I�u�W�F�N�g

    void Start()
    {
        cursorPosition = cursor.anchoredPosition; // �����ʒu���擾
    }

    void Update()
    {
        HandleCursorMovement();
        HandleHoverEvents();
        HandleSelection();
    }

    // �J�[�\���ړ�����
    void HandleCursorMovement()
    {
        float moveX = Input.GetAxis("CursorHorizontal");
        float moveY = Input.GetAxis("CursorVertical");

        Vector2 movement = new Vector2(moveX, moveY) * cursorSpeed * Time.deltaTime;
        cursorPosition += movement;

        // �J�[�\���͈̔͐���
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 minPosition = Vector2.zero;
        Vector2 maxPosition = canvasRect.sizeDelta;

        cursorPosition.x = Mathf.Clamp(cursorPosition.x, minPosition.x, maxPosition.x);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, minPosition.y, maxPosition.y);

        cursor.anchoredPosition = cursorPosition;

    }

    // �J�[�\���z�o�[����
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

    // �J�[�\���I������
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

                Debug.Log("�����ꂽ�{�^��:" + result.gameObject.name);

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