using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���j�b�g�̊��N���X
public class DefenseUnit_Base : MonoBehaviour
{
    public float AttackPower;   // �U����
    public float AttackRange;   // �U���͈�
    public float LifeTime;      // ��������


    private string PrefabPath = null;   // �v���n�u�̃p�X

    // Start is called before the first frame update
    void Start()
    {
        // �ϐ�������
        AttackPower = 0.0f;
        AttackRange = 0.0f;
        LifeTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //============= �Q�b�^�[ ==================
    // �U���͂��擾
    public float GetAttackPower()
    {
        return AttackPower;
    }

    // �U���͈͂��擾
    public float GetAttackRange()
    {
        return AttackRange;
    }

    // �������Ԃ��擾
    public float GetLifeTime()
    {
        return LifeTime;
    }

    //============= �Z�b�^�[ ==================
    // �U���͂�����
    public float AddAttackPower(float addAmount)
    {
        AttackPower += addAmount;

        return AttackPower;
    }

    // �U���͈͂�����
    public float AddAttackRange(float addAmount)
    {
        AttackRange += addAmount;

        return AttackRange;
    }

    // �������Ԃ�����
    public float AddLifeTime(float addAmount)
    {
        LifeTime += addAmount;

        return LifeTime;
    }

    // �v���n�u�̃p�X���Z�b�g
    public void SetPrefabPath(string path)
    {
        PrefabPath = path;
    }
}
