using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the lasers that the player fires.
/// Requires a rigidbody to be attached to the object.
/// </summary>
public class Laser : MonoBehaviour
{
    /// <summary>
    /// Speed the laser will travel at.
    /// </summary>
    public float speed = 4f;

    /// <summary>
    /// Damage dealt to enemy ships.
    /// </summary>
    public int damage = 50;

    private Rigidbody2D rb;

    /// <summary>
    /// Called everytime the object is enabled in the pool.
    /// </summary>
    void OnEnable()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    /// <summary>
    /// Determiens if the laser collides with an enemy ship.
    /// Damage is dealt to the enemy if applicable.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AlienShip enemy = collision.GetComponent<AlienShip>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            this.gameObject.SetActive(false);
        }
    }
}
