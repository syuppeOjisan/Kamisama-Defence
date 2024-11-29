using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseUnit : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float[] fireRates = new float[5];
    public float[] projectileSpeeds = new float[5];
    public float[] damages = new float[5];
    public float[] ranges = new float[5];
    public float[] upgradeCosts = new float[5];

    public AudioClip shootSound;
    public AudioClip upgradeSound;

    private float fireCooldown = 0f;
    private AudioSource audioSource;
    private int currentLevel = 1;

    private UIManager uiManager;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager が見つかりません！");
        }

        // ユニット設置時に立ち絵とセリフを変更
        if (uiManager != null)
        {
            uiManager.SetDefenseUnitPlacement();
        }
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            FindAndShootEnemy();
            fireCooldown = 1f / fireRates[currentLevel - 1];
        }
    }

    void FindAndShootEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, ranges[currentLevel - 1]);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("ShootingEnemy"))
            {
                Shoot(hit.transform);
                break;
            }
        }
    }

    void Shoot(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.Initialize(target, projectileSpeeds[currentLevel - 1], damages[currentLevel - 1]);
        }

        PlayShootSound();
    }

    void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    void PlayUpgradeSound()
    {
        if (upgradeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(upgradeSound);
        }
    }

    public bool UpgradeUnit()
    {
        if (currentLevel < 5)
        {
            currentLevel++;
            PlayUpgradeSound();

            if (uiManager != null)
            {
                uiManager.SetDefenseUnitUpgraded();
            }

            return true;
        }
        else
        {
            Debug.Log("ユニットは最大レベルです。");
            return false;
        }
    }

    public float GetUpgradeCost()
    {
        if (currentLevel <= 5)
        {
            return upgradeCosts[currentLevel - 1];
        }
        return 0f;
    }
}