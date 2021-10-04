using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logic for the alien laser objects.
/// Requires a Rigidbody2D to be applied to the game object.
/// </summary>
public class AlienLaser : MonoBehaviour
{
    /// <summary>
    /// Speed of the projectile.
    /// </summary>
    public float speed = 4f;
    
    /// <summary>
    /// Damage done to player on collision.
    /// </summary>
    public int damage = 50;

    /// <summary>
    /// Rigidbody of the alienship.
    /// handles the connection instead of the prefab.
    /// </summary>
    private Rigidbody2D rb;

    // Called everytime the object is enabled in the pool.
    void OnEnable()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }
    
    /// <summary>
    /// On a collision enter checks if the collision was with a player.
    /// Applies damage to the player if applicable.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerShip player = collision.GetComponent<PlayerShip>();
        if (player != null)
        {
            player.TakeDamage(damage);
            this.gameObject.SetActive(false);
        }
    }
}
