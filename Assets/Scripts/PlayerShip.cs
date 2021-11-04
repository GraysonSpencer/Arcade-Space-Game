using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;

/// <summary>
/// Represents the ship that the player controls.
/// </summary>
public class PlayerShip : MonoBehaviour
{
    /// <summary>
    /// Pointer to the controller of the game.
    /// </summary>
    public GameController GC;

    /// <summary>
    /// Lives of Player.
    /// </summary>
    public int lives;

    /// <summary>
    /// Health of Player Ship when spawned.
    /// </summary>
    public int Health;

    /// <summary>
    /// Speed the player will move at.
    /// </summary>
    public float moveSpeed = 3f;

    /// <summary>
    /// Used for starting Position.
    /// </summary>
    public Vector2 startLocation;

    /// <summary>
    /// X value in range is lower bound and Y value is upper bound.
    /// </summary>
    public Vector2 YRange;

    /// <summary>
    /// Range that the player can move in.
    /// X value in range is left bound and Y value is right bound.
    /// </summary>
    public Vector2 Xrange;

    /// <summary>
    /// Used to store the direction of the player input.
    /// </summary>
    private Vector2 playerInputDirection;

    private Rigidbody2D rb;

    /// <summary>
    /// Initializes the health of the current player object to the healh from the prefab.
    /// </summary>
    private int actualHealth;

    /// <summary>
    /// Called everytime the object is enabled in the pool.
    /// </summary>
    void OnEnable()
    {
        // Sets the player at the start position defined by the public variables.
        SpawnPlayer();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Spawns player in the designated start position..
    /// Health is initialized to the given value from prefab.
    /// </summary>
    private void SpawnPlayer()
    {
        this.transform.position = new Vector3(startLocation.x, startLocation.y, 0);
        actualHealth = Health;
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        playerInputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }


    private void FixedUpdate()
    {
        moveCharacter(playerInputDirection); 
    }

    /// <summary>
    /// Moves the character in the given direction.
    /// If the proposed movement exits the range given by the prefab, the movement does not occur.
    /// </summary>
    /// <param name="direction">Direction the player wants to move.</param>
    void moveCharacter (Vector2 direction)
    {
        // Current position.
        Vector2 tempPosition = (Vector2)transform.position;

        Vector2 proposedMovement = (direction.normalized * moveSpeed * Time.deltaTime);
        
        tempPosition += proposedMovement;
        
        // Check that the player is still within the provided range.
        if (tempPosition.x >= Xrange.x && tempPosition.x <= Xrange.y)
        {
            if (tempPosition.y >= YRange.x && tempPosition.y <= YRange.y)
            {
                rb.MovePosition(tempPosition);
            }
        }
        
    }

    /// <summary>
    /// Reduces the player's ship health by the given amount.
    /// </summary>
    /// <param name="damage">Amount of damage to deal to the player.</param>
    public void TakeDamage(int damage)
    {
        actualHealth -= damage;
        if (actualHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// If the health goes below zero a life is removed or the player ship is disabled.
    /// </summary>
    void Die()
    {
        lives--;
        this.gameObject.SetActive(false);

        if (lives > 0)
        {
            SpawnPlayer();
        }
        else
        {   
            // Requires the game to be started from the main menu scene to function properly.
            MenuManager.instance.LoadMainMenu();
        }
    }
}
