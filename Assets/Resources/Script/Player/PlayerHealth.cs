using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 30f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log("Player Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}