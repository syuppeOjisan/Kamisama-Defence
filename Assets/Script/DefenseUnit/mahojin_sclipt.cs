using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mahojin : MonoBehaviour
{
    public float[] upgradeCosts = new float[5]; // �e���x���̃A�b�v�O���[�h�R�X�g
 //   public float[] lifetimes = new float[5];   // �e���x���̎�������
    public AudioClip upgradeSound;            // �A�b�v�O���[�h���̌��ʉ�

    public int currentLevel = 1;             // �������x����1�ɐݒ�
    private bool canWarp = true;              // ���[�v�\���������t���O
    public float warpCooldown = 2f;           // �N�[���^�C���̕b��
    public float lifetime = 20;                 // ���@�w�̎�������10
    private AudioSource audioSource;          // ���ʉ��Đ��p

    void Start()
    {
        // AudioSource�R���|�[�l���g��ǉ�
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        StartCoroutine(Jikai());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && canWarp)
        {
            Debug.Log("�G��");
           

            // "start"�^�O�̂����I�u�W�F�N�g��T��
            GameObject startObject = GameObject.FindWithTag("start");

            if (startObject != null)
            {
                Debug.Log("���[�v����");
                // "start"�^�O�̃I�u�W�F�N�g�̍��W���擾
                Vector3 startPosition = startObject.transform.position;

                // Y���W��+20��������
                Vector3 warpPosition = new Vector3(startPosition.x, startPosition.y + 20.0f, startPosition.z);

                // �G�����[�v������
                other.transform.position = warpPosition;

                // �N�[���^�C�����J�n
                StartCoroutine(WarpCooldown());
            }
            else
            {
                Debug.LogError("'start'�^�O�̃I�u�W�F�N�g��������܂���ł���");
            }
        }
    }

    // �N�[���^�C������
    private IEnumerator WarpCooldown()
    {
        canWarp = false;
        yield return new WaitForSeconds(warpCooldown);
        canWarp = true;
    }

    private IEnumerator Jikai()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }

    // �A�b�v�O���[�h�R�X�g���擾
    public float GetUpgradeCost()
    {
        if (currentLevel - 1 < upgradeCosts.Length)
        {
            return upgradeCosts[currentLevel - 1];
        }
        else
        {
            Debug.LogError("�A�b�v�O���[�h�R�X�g�f�[�^���s�����Ă��܂��B");
            return 0f;
        }
    }

    // ���j�b�g�̃A�b�v�O���[�h
    public bool UpgradeUnit()
    {
        if (currentLevel < upgradeCosts.Length)
        {
            currentLevel++;  // ���x���A�b�v

            // �V�������x���̎������Ԃ�ݒ�

            lifetime += 10;
            // �A�b�v�O���[�h�T�E���h���Đ�
            PlayUpgradeSound();

            Debug.Log($"Mahojin�����x���A�b�v���܂���: Lv{currentLevel}");
            return true;
        }
        else
        {
            Debug.Log("Mahojin�͊��ɍő僌�x���ł��B");
            return false;
        }
    }

    // �A�b�v�O���[�h���̌��ʉ��Đ�
    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }
}
