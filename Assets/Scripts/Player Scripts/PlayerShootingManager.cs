using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShootingManager : MonoBehaviour
{
    [SerializeField]
    private float shootingTimerLimit = 0.2f;
    private float shootingTimer;

    [SerializeField]
    private Transform bulletSpawnPos;

    private PlayerWeaponManager playerWeaponManager;

    private bool canMove = true;

    private void Awake()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }

    private void Update()
    {
        if (canMove)
        {
            HandleShooting();
        }
    }

    void HandleShooting()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            if(Time.time > shootingTimer)
            {
                shootingTimer = Time.time + shootingTimerLimit;
                //animate muzzle flash

                playerWeaponManager.Shoot(bulletSpawnPos.position);
            }
        }
    }

    void CreateBullet()
    {
        playerWeaponManager.Shoot(bulletSpawnPos.position);
    }

    // метод для остановки движения игрока
    public void DisableMovement()
    {
        canMove = false;
    }

    // метод для восстановления движения игрока
    public void EnableMovement()
    {
        canMove = true;
    }

}
