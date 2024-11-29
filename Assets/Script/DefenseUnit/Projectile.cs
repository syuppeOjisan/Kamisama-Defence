using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float damage;

    public void Initialize(Transform target, float speed, float damage)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                HitTarget();
            }
        }
        else
        {
            Destroy(gameObject); // �^�[�Q�b�g���Ȃ��ꍇ�͒e��j��
        }
    }

    void HitTarget()
    {
        DefenseTarget defenseTarget = target.GetComponent<DefenseTarget>();

        if (defenseTarget != null)
        {
            defenseTarget.TakeDamage(damage); // �_�ЂɃ_���[�W��^����
        }

        Destroy(gameObject); // �e��j��
    }

    public float GetDamage()
    {
        return damage;
    }
}