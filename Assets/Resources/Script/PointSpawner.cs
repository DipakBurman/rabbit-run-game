using UnityEngine;

// Spawns one collectible at a time inside the walled play area.
public class PointSpawner : MonoBehaviour
{
    public GameObject PointPrefab;
    public GameObject NewSpawnedpoint;

    [SerializeField] private Vector2 spawnMin = new Vector2(-22f, -16f);
    [SerializeField] private Vector2 spawnMax = new Vector2(22f, 16f);
    [SerializeField] private float minDistanceFromPlayer = 6f;
    [SerializeField] private int maxSpawnAttempts = 12;

    private Transform player;
    private bool isGameOver;

    private void Start()
    {
        if (PointPrefab == null)
        {
            PointPrefab = Resources.Load<GameObject>("Prefabs/Point");
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) player = playerObject.transform;

        SpawnObject();
    }

    private void Update()
    {
        if (!isGameOver && NewSpawnedpoint == null)
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        if (isGameOver || PointPrefab == null) return;

        NewSpawnedpoint = Instantiate(PointPrefab, GetSpawnPosition(), Quaternion.identity);

        if (SpawnerandMove.instance != null)
        {
            SpawnerandMove.instance.SpawnEnemy(NewSpawnedpoint);
        }
    }

    public void StopGameplay()
    {
        isGameOver = true;
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 position = new Vector2(
            Random.Range(spawnMin.x, spawnMax.x),
            Random.Range(spawnMin.y, spawnMax.y));

        if (player == null) return position;

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            if (Vector2.Distance(position, player.position) >= minDistanceFromPlayer)
            {
                return position;
            }

            position = new Vector2(
                Random.Range(spawnMin.x, spawnMax.x),
                Random.Range(spawnMin.y, spawnMax.y));
        }

        return position;
    }
}
