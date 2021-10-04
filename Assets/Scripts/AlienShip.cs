using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the Enemy Alien ship.
/// </summary>
public class AlienShip : MonoBehaviour
{
    /// <summary>
    /// Pointer to the controller of the game.
    /// </summary>    
    public GameController GC;

    /// <summary>
    /// Health of enemy when spawned.
    /// </summary>
    public int Health;

    /// <summary>
    /// How fast the alien ship will move.
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Fastest the alien ship will be able to shoot.
    /// </summary>
    public float minShootTime;

    /// <summary>
    /// Slowest the alien ship will be able to shoot.
    /// </summary>
    public float maxShootTime;

    /// <summary>
    /// Range of X values the ship will spawn in. Order of values is irrelevant
    /// </summary>
    public Vector2 rangeOfX;

    /// <summary>
    /// Range of Y values the ship will spawn in. Order of values is irrelevant
    /// </summary>
    public Vector2 rangeOfY;

    /// <summary>
    /// Player ship location
    /// </summary>
    public Transform player;

    /// <summary>
    /// location where bullets are fired from. (Empty Object in Alien Ship Prefab).
    /// </summary>
    public Transform firePoint;

    /// <summary>
    /// Prefab for laser to be shot.
    /// </summary>    
    public GameObject laserPrefab;

    /// <summary>
    /// Next location for ship to move to.
    /// </summary>
    private Vector3 targetLocation;

    /// <summary>
    /// Time is random between the given min and max time to shoot.
    /// </summary>
    private float timeToShoot;

    /// <summary>
    /// Time since the last laser was fired.
    /// </summary>
    private float currentTime;

    /// <summary>
    /// Health of this alien ship. Initialized to the health of the prefab.
    /// </summary>
    private int localHealth;

    /// <summary>
    /// Called everytime the object is enabled in the pool.
    /// Initialize starting position and first position to be moved to.
    /// </summary>
   void OnEnable()
    {
        localHealth = Health;
        SetStartingPosition();
        targetLocation = NewDestination();
        currentTime = 0;
        WhenToShoot();
    }


    // Movement and Positions.

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        
        // Firing Laser.
        if (currentTime >= timeToShoot)
        {
            Shoot();
            WhenToShoot();
            currentTime = 0;
        }
        
        // Determine new position to move to.
        if (this.transform.position == targetLocation)
        {
            targetLocation = NewDestination();
        }

        MoveCharacter();
    }

    /// <summary>
    /// Generates a location to start the alien ship in.
    /// The generated location will be within the given range of x's and y's taken from the public range variables.
    /// </summary>
    private void SetStartingPosition()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(rangeOfX.x, rangeOfX.y), 
            UnityEngine.Random.Range(rangeOfY.x, rangeOfY.y), 0);
    }

    /// <summary>
    /// Generates a new location for the alien ship to travel to.
    /// The generated location will be within the given range of x's and y's taken from the public range variables.
    /// </summary>
    /// <returns>New location to travel to.</returns>
    private Vector3 NewDestination()
    {
        return new Vector3(UnityEngine.Random.Range(rangeOfX.x, rangeOfX.y), 
            UnityEngine.Random.Range(rangeOfY.x, rangeOfY.y), 0f);
    }

    /// <summary>
    /// Travel to the alien ships generated location.
    /// </summary>
    private void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, moveSpeed * Time.deltaTime);
    }

    
    /// <summary>
    /// Reduces the health of this ship by the given damage amount.
    /// </summary>
    /// <param name="damage">Amount of damage to deal.</param>
    public void TakeDamage(int damage)
    {
        localHealth -= damage;
        if (localHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Disables the alien ship if the health goes below 0.
    /// </summary>
    void Die()
    {
        this.gameObject.SetActive(false);
        GC.SpawnEnemy();
    }

    // Shooting Methods

    /// <summary>
    /// Fires an AlienLaser from the firePoint transfrom.
    /// </summary>
    void Shoot()
    {
        GameObject bullet;
        if (ObjectPooler.sharedInstance.GetPooledObject("AlienLaser", out bullet))
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
        }
    }

    /// <summary>
    /// Generate a random time before the ship will fire again.
    /// </summary>
    private void WhenToShoot()
    {
        timeToShoot = Random.Range(minShootTime, maxShootTime);
    }
}
