using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 30f;

    public void TakeDamage(float damage)
    {
        if (health <= 0) return;

        health -= damage;

        Debug.Log("Player Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Let Movement end the round so the lose screen still shows.
        Movement movement = GetComponent<Movement>();
        if (movement != null)
        {
            movement.Lose();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
