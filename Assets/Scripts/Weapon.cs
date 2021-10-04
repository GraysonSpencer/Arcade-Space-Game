using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    /// <summary>
    /// If the player is able to shoot.
    /// </summary>
    private bool canShoot = true;
    
    /// <summary>
    /// Keeps track of the time since the last bullet was fired.
    /// </summary>
    private float currentTime = 0;
    
    /// <summary>
    /// How much times between player shots.
    /// </summary>
    public float fireRate;

    /// <summary>
    /// Fire point is transform where laser will be shot from.
    /// </summary>
    public Transform firePoint;

    /// <summary>
    /// Type of object to be shot.
    /// </summary>    
    public GameObject laserPrefab;


    void Update()
    {
       
        AllowedToShoot();
        
        // If the player can't shoot increment the time until they are allowed to shoot again.
        if (canShoot == false)
        {
            currentTime += Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (canShoot)
            {
                Shoot();
                // Disable the player from shooting again.
                canShoot = false;
                currentTime = 0;
            }
        }
    }

    /// <summary>
    /// Fires the given prefab at from the firepoint location.
    /// </summary>
   private void Shoot()
    {
        GameObject bullet;  
        if (ObjectPooler.sharedInstance.GetPooledObject("PlayerLaser", out bullet))
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
        }

    }

    /// <summary>
    /// Determines if the player is allowed to shoot.
    /// </summary>
    private void AllowedToShoot()
    {
        if(currentTime >= fireRate)
        {
            canShoot = true;
        }
    }
}
