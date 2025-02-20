using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Do nothing if the projectile hits the player
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                // Assume that the enemy has an Enemy script with a TakeDamage method
                EnemyDamage enemy = collision.gameObject.GetComponent<EnemyDamage>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1); // Reduce the enemy's health by 1
                }
                
                Debug.Log("Projectile hit an enemy: " + collision.gameObject.name);
                Destroy(gameObject); // Destroy the projectile on collision
            }
            else
            {
                Debug.Log("Projectile hit: " + collision.gameObject.name);
                Destroy(gameObject); // Destroy the projectile on other collisions
            }
        }
    }
}