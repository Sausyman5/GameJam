using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int health = 3; // Starting health for the enemy
    public float destructionDelay = 1f; // Delay before the enemy is destroyed after death (optional)

    // This method will be called to deal damage to the enemy
    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce health by the damage amount
        Debug.Log(gameObject.name + " took damage! Remaining health: " + health);

        // Check if health is less than or equal to zero
        if (health <= 0)
        {
            Die(); // Call the Die method if the enemy's health is zero or less
        }
    }

    // This method handles the enemy's death
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!"); // Log the death of the enemy
        // You can add death animations or effects here if needed
        
        // Destroy the enemy GameObject after a delay
        Destroy(gameObject, destructionDelay);
    }
}