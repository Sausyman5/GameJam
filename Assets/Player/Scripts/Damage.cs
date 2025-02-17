using UnityEngine;

public class Death : MonoBehaviour
{
    public int maxHealth = 10;
    public int damage = 1;
    private int health;
    public float timeBetweenDamage = 0.5f; // Time in seconds
    private float timer = 0f;
    private bool isColliding = false; // Flag to track whether the enemy is currently colliding with the player.
    public GameObject player; // Reference to the player GameObject

    // Audio
    public AudioClip deathSound; // Assign in Inspector
    private AudioSource audioSource; // Added for playing the sound

    private void Start()
    {
        health = maxHealth;

        // Ensure AudioSource is available.  Add it if it doesn't exist.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // If the player is colliding with the enemy.
        if (isColliding)
        {
            // Increment the timer.
            timer += Time.deltaTime;

            // If enough time has passed, apply damage.
            if (timer >= timeBetweenDamage)
            {
                // Reset the timer.
                timer = 0f;
                // Apply the damage.
                TakeDamageFromPlayer(damage);
            }
        }
    }

    // This method handles the enemy's damage.
    public void TakeDamageFromPlayer(int damage)
    {
        // Reduce health by the damage amount.
        health -= damage;

        // If health is zero or less.
        if (health <= 0)
        {
            // Call the Die method to destroy the player.
            Die();
        }

        // Output the health.
        Debug.Log($"Ouch! Health: {health}");
    }

    // This method handles destroying the object (the player).
    private void Die()
    {
        // Play death sound
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        else
        {
            Debug.LogWarning("Death sound or AudioSource not assigned. Death sound not playing.");
        }

        // Destroy the player GameObject.  Make sure player is assigned in inspector.
        if (player != null) // Added a check to avoid null reference errors.
        {
            Destroy(player);
            Debug.Log("Player has died.");
        }
        else
        {
            Debug.LogError("Player GameObject not assigned in the inspector.");
        }
    }

    // This method is called when the collider of this object touches another collider.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the colliding object is the player.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set the isColliding flag to true.
            isColliding = true;

            // Apply the first damage when the player touches the enemy.
            TakeDamageFromPlayer(damage);

            // Start the timer after applying the first damage.
            timer = 0; // Start the timer
        }
    }

    // This method is called when the collider of this object stops touching another collider.
    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the colliding object is the player.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set the isColliding flag to false.
            isColliding = false;
            timer = 0f; //resets timer
        }
    }
}