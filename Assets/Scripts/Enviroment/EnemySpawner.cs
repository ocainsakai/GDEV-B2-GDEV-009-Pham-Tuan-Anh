using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float minDistanceFromPlayer = 5f;

    [Header("Interval Settings")]
    [SerializeField] private float initialInterval = 5f;
    [SerializeField] private float minimumInterval = 0.5f;
    [SerializeField] private AnimationCurve spawnCurve = new AnimationCurve(
        new Keyframe(0f, 0f),
        new Keyframe(1f, 1f)
    );
    [SerializeField] private float curveDuration = 120f;

    private float elapsedTime;
    private float spawnTimer;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = GetCurrentInterval();
        }
    }

    private float GetCurrentInterval()
    {
        float t = Mathf.Clamp01(elapsedTime / curveDuration);
        float curveValue = spawnCurve.Evaluate(t);
        float interval = Mathf.Lerp(initialInterval, minimumInterval, curveValue);
        return Mathf.Max(interval, minimumInterval);
    }

    private void SpawnEnemy()
    {
        if (MapBoundary.Instance == null) return;

        Bounds b = MapBoundary.Instance.Bounds;
        Vector2 playerPos = player != null ? (Vector2)player.position : Vector2.zero;

        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        float maxDist = Mathf.Max(b.extents.x, b.extents.y);
        float dist = Random.Range(minDistanceFromPlayer, maxDist);

        Vector2 spawnPos = playerPos + dir * dist;
        spawnPos = MapBoundary.Instance.Clamp(spawnPos);

        if (Vector2.Distance(spawnPos, playerPos) < minDistanceFromPlayer)
        {
            spawnPos = playerPos - dir * dist;
            spawnPos = MapBoundary.Instance.Clamp(spawnPos);
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
