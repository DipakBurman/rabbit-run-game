using UnityEngine;

// Finds or spawns the enemy and steers it toward the player.
public class SpawnerandMove : MonoBehaviour
{
    public static SpawnerandMove instance;

    public GameObject player;
    public GameObject EnemyPrefab;
    public float EnemySpeed;
    public GameObject SpawnedEnemy;

    [SerializeField] private float spawnOffsetFromPoint = 3f;
    [SerializeField] private float minDistanceFromPlayer = 4f;

    private Rigidbody2D enemyBody;
    private bool isGameOver;
    private Vector3 targetPosition;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (EnemyPrefab == null)
        {
            EnemyPrefab = Resources.Load<GameObject>("Prefabs/enemy");
        }

        if (SpawnedEnemy == null)
        {
            SpawnedEnemy = GameObject.FindGameObjectWithTag("Enemy");
        }

        CacheEnemyBody();
    }

    private void Start()
    {
        KeepEnemyClearOfPlayer();
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    public void SpawnEnemy(GameObject point)
    {
        if (isGameOver || point == null) return;

        if (EnemyPrefab == null)
        {
            EnemyPrefab = Resources.Load<GameObject>("Prefabs/enemy");
        }

        if (EnemyPrefab == null) return;

        DestroyCurrentEnemy();

        SpawnedEnemy = Instantiate(EnemyPrefab, GetSpawnPositionNearPoint(point), Quaternion.identity);
        CacheEnemyBody();
    }

    private void DestroyCurrentEnemy()
    {
        if (SpawnedEnemy == null)
        {
            SpawnedEnemy = GameObject.FindGameObjectWithTag("Enemy");
        }

        if (SpawnedEnemy == null) return;

        Destroy(SpawnedEnemy);
        SpawnedEnemy = null;
        enemyBody = null;
    }

    private Vector3 GetSpawnPositionNearPoint(GameObject point)
    {
        Vector3 pointPosition = point.transform.position;
        Vector2 offsetDirection = Vector2.right;

        if (player != null)
        {
            Vector2 fromPlayer = (Vector2)pointPosition - (Vector2)player.transform.position;
            if (fromPlayer.sqrMagnitude > 0.0001f)
            {
                offsetDirection = fromPlayer.normalized;
            }
        }

        Vector3 spawnPosition = pointPosition + (Vector3)(offsetDirection * spawnOffsetFromPoint);

        if (player != null && Vector2.Distance(spawnPosition, player.transform.position) < minDistanceFromPlayer)
        {
            spawnPosition = player.transform.position + (Vector3)(offsetDirection * minDistanceFromPlayer);
            spawnPosition.z = 0f;
        }

        return spawnPosition;
    }

    public void StopGameplay()
    {
        isGameOver = true;

        if (enemyBody != null)
        {
            enemyBody.linearVelocity = Vector2.zero;
        }
    }

    private void MoveEnemy()
    {
        if (isGameOver || player == null || SpawnedEnemy == null) return;

        Vector2 currentPosition = enemyBody != null ? enemyBody.position : (Vector2)SpawnedEnemy.transform.position;
        Vector2 direction = ((Vector2)player.transform.position - currentPosition);
        if (direction.sqrMagnitude < 0.0001f) return;

        Vector2 nextPosition = currentPosition + direction.normalized * EnemySpeed * Time.fixedDeltaTime;

        if (enemyBody != null)
        {
            enemyBody.MovePosition(nextPosition);
        }
        else
        {
            SpawnedEnemy.transform.position = nextPosition;
        }
    }

    private void CacheEnemyBody()
    {
        if (SpawnedEnemy == null)
        {
            enemyBody = null;
            return;
        }

        enemyBody = SpawnedEnemy.GetComponent<Rigidbody2D>();
        if (enemyBody != null) enemyBody.freezeRotation = true;
    }

    private void KeepEnemyClearOfPlayer()
    {
        if (player == null || SpawnedEnemy == null) return;

        if (Vector2.Distance(SpawnedEnemy.transform.position, player.transform.position) < 4f)
        {
            Vector3 safePosition = GetSafeSpawnPosition();
            SpawnedEnemy.transform.position = safePosition;
            if (enemyBody != null) enemyBody.position = safePosition;
        }
    }

    private Vector3 GetSafeSpawnPosition()
    {
        return new Vector3(18f, 12f, 0f);
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }
}
