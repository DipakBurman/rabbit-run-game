using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 30f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log("Enemy Health: " + health);

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