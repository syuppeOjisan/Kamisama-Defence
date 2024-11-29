using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;  // 移動速度
    private NavMeshAgent agent;   // NavMeshAgentの参照
    private Transform target;     // 移動目標（神社）

    void Start()
    {
        // NavMeshAgentの取得
        agent = GetComponent<NavMeshAgent>();

        // 神社（目標オブジェクト）をターゲットに設定
        target = GameObject.FindWithTag("DefenseTarget").transform;

        // NavMeshAgentの移動速度を設定
        agent.speed = moveSpeed;

        // NavMeshAgentに目標位置を設定
        agent.SetDestination(target.position);
    }

    void Update()
    {
        // 現在の目標位置に向かって移動する
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            OnReachTarget(); // 目標に到達したら処理
        }
    }

    // 神社に触れたときの処理
    private void OnReachTarget()
    {
        Destroy(gameObject); // 敵を削除
    }

    // オブジェクトに「Enemy」タグを付ける
    void Awake()
    {
        gameObject.tag = "Enemy";
    }
}