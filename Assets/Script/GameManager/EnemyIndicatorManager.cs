using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyIndicatorManager : MonoBehaviour
{
    public GameObject indicatorPrefab;      // ���C���W�P�[�^�[�̃v���n�u
    public Camera mainCamera;               // ���C���J�����ւ̎Q��
    public RectTransform canvasRect;        // Canvas��RectTransform�iUI�͈͂��w�肷�邽�߁j

    private GameObject[] currentEnemies = { };
    private Dictionary<GameObject, GameObject> activeIndicators = new Dictionary<GameObject, GameObject>(); // �G�ƃC���W�P�[�^�[�̃}�b�s���O

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // �G�̐����O��t���[�����������Ă����烊�X�g���`�F�b�N(���ׂ���)
        if (currentEnemies.Length != enemies.Length)
        {
            // �z����r���č폜���ꂽ�G�I�u�W�F�N�g��ۑ�
            GameObject[] destroyedEnemies = currentEnemies.Except(enemies).ToArray();
            foreach (GameObject nullEnemies in destroyedEnemies)
            {
                RemoveIndicatorForEnemy(nullEnemies);
            }
        }

        // �G�̏�Ԃ�ۑ�
        currentEnemies = enemies;

        foreach (GameObject enemy in enemies)
        {
            if (!IsEnemyOnScreen(enemy))
            {
                if (!activeIndicators.ContainsKey(enemy))
                {
                    CreateIndicatorForEnemy(enemy); // �G����ʊO�ɂ���΃C���W�P�[�^�[���쐬
                }

                UpdateIndicatorPosition(enemy);
            }
            else if (activeIndicators.ContainsKey(enemy))
            {
                RemoveIndicatorForEnemy(enemy); // �G����ʓ��ɖ߂�΃C���W�P�[�^�[���폜
            }
        }
    }

    // �G����ʓ��ɂ��邩�ǂ����𔻒�
    private bool IsEnemyOnScreen(GameObject enemy)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(enemy.transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0;
    }

    // �G�ɃC���W�P�[�^�[���쐬
    private void CreateIndicatorForEnemy(GameObject enemy)
    {
        GameObject indicator = Instantiate(indicatorPrefab, canvasRect); // Canvas�̎q�I�u�W�F�N�g�Ƃ��ăC���W�P�[�^�[���쐬
        activeIndicators[enemy] = indicator;
    }

    // �G�ɑΉ�����C���W�P�[�^�[���폜
    private void RemoveIndicatorForEnemy(GameObject enemy)
    {
        if (activeIndicators.ContainsKey(enemy))
        {
            Destroy(activeIndicators[enemy]);
            activeIndicators.Remove(enemy);
        }
    }

    // �C���W�P�[�^�[�̈ʒu���X�V
    private void UpdateIndicatorPosition(GameObject enemy)
    {
        if (!activeIndicators.ContainsKey(enemy)) return;

        GameObject indicator = activeIndicators[enemy];
        Vector3 enemyScreenPosition = mainCamera.WorldToScreenPoint(enemy.transform.position);


        // �J�����ɑ΂��ēG�̈ʒu���v�Z���A��ʊO�̏ꍇ�ɖ���\��
        Vector3 clampedPosition = enemyScreenPosition;

        // �J�����̗����ɂ���ꍇ�͒l��␳
        if (clampedPosition.z < 0)
        {
            clampedPosition.x = -clampedPosition.x;
            clampedPosition.y = -clampedPosition.y;
        }

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, 50, Screen.width - 50); // ��ʒ[�ɉ����Ĉړ�
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 50, Screen.height - 50); // ��ʒ[�ɉ����Ĉړ�


        // �C���W�P�[�^�[�̈ʒu��ݒ�
        RectTransform indicatorRect = indicator.GetComponent<RectTransform>();
        indicatorRect.position = clampedPosition;

        // ���̉�]��ݒ�i�G�̕������w���j
        Vector3 dir = enemy.transform.position - mainCamera.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        indicatorRect.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}