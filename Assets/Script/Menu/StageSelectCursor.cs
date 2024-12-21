using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectCursor : MonoBehaviour
{
    public RectTransform cursor; // �J�[�\����RectTransform
    public Canvas canvas; // Canvas���w��
    public float cursorSpeed = 300f; // �J�[�\���̈ړ����x
    public ScrollRect scrollRect; // �X�N���[���r���[��ScrollRect
    public RectTransform scrollViewArea; // Scroll View�G���A�͈̔�
    public AudioClip hoverSound; // �{�^���ɏ�������̉�
    public AudioClip clickSound; // �{�^����I���������̉�
    public AudioSource audioSource; // ���ʉ��Đ��p

    private Vector2 cursorPosition; // �J�[�\���̌��݈ʒu
    private bool isCursorOverScrollView = false; // �J�[�\����Scroll View��ɂ��邩�ǂ���

    private GameObject lastHoveredButton = null; // �Ō�Ƀz�o�[���ꂽ�{�^��

    void Start()
    {
        cursorPosition = cursor.anchoredPosition; // �����ʒu���擾
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

    // �J�[�\���ړ�����
    void HandleCursorMovement()
    {
        float moveX = Input.GetAxis("CursorHorizontal");
        float moveY = Input.GetAxis("CursorVertical");

        Vector2 movement = new Vector2(moveX, moveY) * cursorSpeed * Time.deltaTime;
        cursorPosition += movement;

        // �J�[�\���͈̔͐���
        Vector2 minPosition = Vector2.zero;
        Vector2 maxPosition = canvas.GetComponent<RectTransform>().sizeDelta;

        cursorPosition.x = Mathf.Clamp(cursorPosition.x, minPosition.x, maxPosition.x);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, minPosition.y, maxPosition.y);

        cursor.anchoredPosition = cursorPosition;
    }

    // A�{�^���Ō��菈��
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
                if (button != null && button.interactable) // �{�^�����L���ȏꍇ�̂ݏ���
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

    // �E�X�e�B�b�N�ŃX�N���[������
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

    // �J�[�\����Scroll View��ɂ��邩�ǂ����𔻒�
    bool IsCursorOverScrollView()
    {
        Vector2 localCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            scrollViewArea, cursor.position, canvas.worldCamera, out localCursor);

        isCursorOverScrollView = scrollViewArea.rect.Contains(localCursor);
        return isCursorOverScrollView;
    }

    // �{�^����ɃJ�[�\������������̏���
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

                // ���߂Ă��̃{�^���Ƀz�o�[�����ꍇ
                if (lastHoveredButton != hoveredButton)
                {
                    ExecuteEvents.Execute(button.gameObject, pointerData, ExecuteEvents.pointerEnterHandler);
                }
                break;
            }
        }

        // �O��z�o�[���Ă����{�^�����痣�ꂽ�ꍇ
        if (lastHoveredButton != null && lastHoveredButton != hoveredButton)
        {
            ExecuteEvents.Execute(lastHoveredButton, pointerData, ExecuteEvents.pointerExitHandler);
        }

        lastHoveredButton = hoveredButton;
    }
}