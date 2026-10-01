using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 40f;
    public float damage = 10f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                // Debug.Log("Enemy hit! Remaining health: " + enemy.health);
            }

            Destroy(gameObject); //destroybullet
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(damage);
                // Debug.Log("Player hit! Remaining health: " + player.health);
            }

            Destroy(gameObject); //destroybullet
        }
    }
}