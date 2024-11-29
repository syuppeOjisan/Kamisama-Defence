using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;  // �ړ����x
    private NavMeshAgent agent;   // NavMeshAgent�̎Q��
    private Transform target;     // �ړ��ڕW�i�_�Ёj

    void Start()
    {
        // NavMeshAgent�̎擾
        agent = GetComponent<NavMeshAgent>();

        // �_�Ёi�ڕW�I�u�W�F�N�g�j���^�[�Q�b�g�ɐݒ�
        target = GameObject.FindWithTag("DefenseTarget").transform;

        // NavMeshAgent�̈ړ����x��ݒ�
        agent.speed = moveSpeed;

        // NavMeshAgent�ɖڕW�ʒu��ݒ�
        agent.SetDestination(target.position);
    }

    void Update()
    {
        // ���݂̖ڕW�ʒu�Ɍ������Ĉړ�����
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            OnReachTarget(); // �ڕW�ɓ��B�����珈��
        }
    }

    // �_�ЂɐG�ꂽ�Ƃ��̏���
    private void OnReachTarget()
    {
        Destroy(gameObject); // �G���폜
    }

    // �I�u�W�F�N�g�ɁuEnemy�v�^�O��t����
    void Awake()
    {
        gameObject.tag = "Enemy";
    }
}