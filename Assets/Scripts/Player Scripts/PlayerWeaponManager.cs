using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponManager[] playerWeapons;

    private int weaponIndex;

    [SerializeField]
    private GameObject[] weaponBullets;

    private Vector2 targetPos;

    private Vector2 direction;

    private Camera mainCam;

    private Vector2 bulletSpawnPosition;

    private Quaternion bulletRotation;

    [SerializeField]
    private AudioClip[] weaponFireSounds; // Массив звуков выстрелов

    private AudioSource audioSource; // Источник аудио для воспроизведения звуков

    private void Awake()
    {
        mainCam = Camera.main;
        LoadWeapon();

        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource
    }

    private void LoadWeapon()
    {
        weaponIndex = PlayerPrefs.GetInt("SelectedWeapon", 0);

        for (int i = 0; i < playerWeapons.Length; i++)
        {
            playerWeapons[i].gameObject.SetActive(i == weaponIndex);
        }
    }

    public void SaveWeapon()
    {
        PlayerPrefs.SetInt("SelectedWeapon", weaponIndex);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        ChangeWeapon();
    }

    public void ActivateGun(int gunIndex)
    {
        playerWeapons[weaponIndex].ActivateGun(gunIndex);
    }

    void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerWeapons[weaponIndex].gameObject.SetActive(false);
            weaponIndex++;

            if (weaponIndex == playerWeapons.Length)
                weaponIndex = 0;

            playerWeapons[weaponIndex].gameObject.SetActive(true);

            SaveWeapon(); // Сохраняем выбранное оружие
        }
    }

    public void Shoot(Vector3 spawnPos)
    {
        targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        bulletSpawnPosition = new Vector2(spawnPos.x, spawnPos.y);

        direction = (targetPos - bulletSpawnPosition).normalized;

        bulletRotation = Quaternion.Euler(0,0,
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        GameObject newBullet = Instantiate(weaponBullets[weaponIndex],
            spawnPos, bulletRotation);

        newBullet.GetComponent<Bullet>().MoveInDirection(direction);

        // Воспроизводим звук выстрела для текущего оружия
        if (weaponIndex < weaponFireSounds.Length && weaponFireSounds[weaponIndex] != null)
        {
            audioSource.PlayOneShot(weaponFireSounds[weaponIndex]);
        }
    }
}
