using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 通常の移動速度
    public float dashSpeed = 10f; // ダッシュ時の移動速度
    public float knockbackForce = 5f; // ノックバックの強さ
    public float stunDuration = 2f; // スタンの持続時間
    public AudioClip stunSound; // スタン時の効果音

    private Rigidbody rb;
    private bool isStunned = false;
    private bool isInvincible = false;
    private float stunEndTime;
    private float invincibleEndTime;
    private AudioSource audioSource;
    private UIManager uiManager; // UIManagerの参照
    private BoundaryController boundaryController; // 境界管理の参照

    private Vector2 lastMousePosition;
    private bool isUsingMouseInput = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();

        // UIManagerの参照を取得
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManagerがシーンに存在しません");
        }

        // BoundaryControllerの参照を取得
        boundaryController = FindObjectOfType<BoundaryController>();
        if (boundaryController == null)
        {
            Debug.LogError("BoundaryControllerがシーンに存在しません");
        }
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            // 移動処理
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // ダッシュ判定（XボタンまたはShiftキー）
            float currentSpeed = (Input.GetButton("Dash") || Input.GetKey(KeyCode.LeftShift)) ? dashSpeed : moveSpeed;
            Vector3 movement = new Vector3(moveX, 0, moveZ) * currentSpeed * Time.fixedDeltaTime;

            Vector3 newPosition = transform.position + movement;

            // 境界内に位置を制限
            if (boundaryController != null)
            {
                newPosition = boundaryController.ClampPosition(newPosition);
            }

            rb.MovePosition(newPosition);
        }
    }

    void Update()
    {
        // スタン状態と無敵時間のチェック
        if (isStunned && Time.time >= stunEndTime)
        {
            EndStun();
        }
        if (isInvincible && Time.time >= invincibleEndTime)
        {
            isInvincible = false;
        }

        // スタンしていない場合のみ方向転換可能
        if (!isStunned)
        {
            HandleRotation();
        }
    }

    void HandleRotation()
    {
        Vector2 rightStickInput = new Vector2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"));
        bool isRightStickActive = rightStickInput.sqrMagnitude > 0.01f;

        if (Input.mousePosition != (Vector3)lastMousePosition)
        {
            isUsingMouseInput = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (isRightStickActive)
        {
            isUsingMouseInput = false;
        }

        if (isUsingMouseInput)
        {
            RotateTowardsMouse();
        }
        else if (isRightStickActive)
        {
            RotateTowardsRightStick(rightStickInput);
        }
    }

    void RotateTowardsMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (playerPlane.Raycast(ray, out float hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime));
        }
    }

    void RotateTowardsRightStick(Vector2 rightStickInput)
    {
        // Y軸入力を反転
        Vector3 direction = new Vector3(rightStickInput.x, 0, -rightStickInput.y);
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime));
        }
    }

    public void ApplyKnockback(Vector3 direction)
    {
        if (!isInvincible)
        {
            rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
            StartStun();
        }
    }

    void StartStun()
    {
        isStunned = true;
        isInvincible = true;
        stunEndTime = Time.time + stunDuration;
        invincibleEndTime = Time.time + stunDuration + 3f;

        PlayStunSound();

        // スタン時の立ち絵とセリフに切り替え
        if (uiManager != null)
        {
            uiManager.SetStunnedPortrait();
        }
    }

    void EndStun()
    {
        isStunned = false;

        // デフォルトの立ち絵とセリフに戻す
        if (uiManager != null)
        {
            uiManager.SetDefaultPortrait();
        }
    }

    void PlayStunSound()
    {
        if (stunSound != null)
        {
            audioSource.PlayOneShot(stunSound);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;
            ApplyKnockback(knockbackDirection);
        }
    }
}