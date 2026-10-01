using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f;

    private float timer;

    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}