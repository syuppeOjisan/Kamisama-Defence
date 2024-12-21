using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �ʏ�̈ړ����x
    public float dashSpeed = 10f; // �_�b�V�����̈ړ����x
    public float knockbackForce = 5f; // �m�b�N�o�b�N�̋���
    public float stunDuration = 2f; // �X�^���̎�������
    public AudioClip stunSound; // �X�^�����̌��ʉ�

    private Rigidbody rb;
    private bool isStunned = false;
    private bool isInvincible = false;
    private float stunEndTime;
    private float invincibleEndTime;
    private AudioSource audioSource;
    private UIManager uiManager; // UIManager�̎Q��
    private BoundaryController boundaryController; // ���E�Ǘ��̎Q��

    private Vector2 lastMousePosition;
    private bool isUsingMouseInput = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();

        // UIManager�̎Q�Ƃ��擾
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager���V�[���ɑ��݂��܂���");
        }

        // BoundaryController�̎Q�Ƃ��擾
        boundaryController = FindObjectOfType<BoundaryController>();
        if (boundaryController == null)
        {
            Debug.LogError("BoundaryController���V�[���ɑ��݂��܂���");
        }
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            // �ړ�����
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // �_�b�V������iX�{�^���܂���Shift�L�[�j
            float currentSpeed = (Input.GetButton("Dash") || Input.GetKey(KeyCode.LeftShift)) ? dashSpeed : moveSpeed;
            Vector3 movement = new Vector3(moveX, 0, moveZ) * currentSpeed * Time.fixedDeltaTime;

            Vector3 newPosition = transform.position + movement;

            // ���E���Ɉʒu�𐧌�
            if (boundaryController != null)
            {
                newPosition = boundaryController.ClampPosition(newPosition);
            }

            rb.MovePosition(newPosition);
        }
    }

    void Update()
    {
        // �X�^����ԂƖ��G���Ԃ̃`�F�b�N
        if (isStunned && Time.time >= stunEndTime)
        {
            EndStun();
        }
        if (isInvincible && Time.time >= invincibleEndTime)
        {
            isInvincible = false;
        }

        // �X�^�����Ă��Ȃ��ꍇ�̂ݕ����]���\
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
        // Y�����͂𔽓]
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

        // �X�^�����̗����G�ƃZ���t�ɐ؂�ւ�
        if (uiManager != null)
        {
            uiManager.SetStunnedPortrait();
        }
    }

    void EndStun()
    {
        isStunned = false;

        // �f�t�H���g�̗����G�ƃZ���t�ɖ߂�
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