using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // Array to store multiple projectile prefabs
    public float spawnInterval = 2f;
    public float projectileSpeed = 5f;
    public float destroyAfterSeconds = 5f;
    public float offScreenDestroyDelay = 2f;
    public float spawnDistance = 0.2f; // Distance in front of the player to spawn the projectile.

    private float timeSinceLastSpawn = 0f;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnProjectile();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnProjectile()
    {
        // Calculate the spawn position in front of the player.
        Vector3 spawnPosition = transform.position + transform.up * spawnDistance; // Use transform.up

        // Select a random projectile from the array
        int randomIndex = Random.Range(0, projectilePrefabs.Length);
        GameObject projectilePrefab = projectilePrefabs[randomIndex]; // Select a random prefab

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation);

        // Get the forward direction
        Vector3 spawnDirection = transform.up; // Correct way

        // Apply velocity
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        if (projectileRigidbody != null)
        {
            projectileRigidbody.linearVelocity = spawnDirection * projectileSpeed; // Changed to velocity (corrected)
        }
        else
        {
            Debug.LogWarning("Projectile prefab does not have a Rigidbody2D component.");
        }

        StartCoroutine(DestroyOffScreen(projectile));

        if (destroyAfterSeconds > 0f)
        {
            Destroy(projectile, destroyAfterSeconds);
        }
    }

    IEnumerator DestroyOffScreen(GameObject projectile)
    {
        yield return new WaitForSeconds(offScreenDestroyDelay);

        // Check if the projectile has been destroyed before checking if it's off screen
        if (projectile != null && IsOffScreen(projectile))
        {
            Destroy(projectile);
        }
    }

    bool IsOffScreen(GameObject go)
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("No Main Camera found!");
            return false;
        }
        Vector3 screenPoint = cam.WorldToViewportPoint(go.transform.position);
        return screenPoint.x < -0.1f || screenPoint.x > 1.1f || screenPoint.y < -0.1f || screenPoint.y > 1.1f;
    }
}