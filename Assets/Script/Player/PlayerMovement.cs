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
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            // �ړ�����
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? dashSpeed : moveSpeed;
            Vector3 movement = new Vector3(moveX, 0, moveZ) * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + movement);
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
            RotateTowardsMouse();
        }
    }

    void RotateTowardsMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitDist = 0.0f;
        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
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