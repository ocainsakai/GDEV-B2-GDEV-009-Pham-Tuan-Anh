using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Moving,
        Chase,
    }
    private EnemyState currentState = EnemyState.Idle;

    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyDetection enemyDetection;
    [SerializeField] private Health health;
    [SerializeField] private int collisionDamage = 10;

    private IEnumerator ramdomMoveCoroutine;
    void Awake()
    {
        if (enemyMovement == null)
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }
        if (enemyDetection == null)
        {
            enemyDetection = GetComponent<EnemyDetection>();
        }
        enemyDetection.onPlayerDetected += OnDetectedPlayer;
        if (health == null)
        {
            health = GetComponent<Health>();
        }
        health.onDeath += () => Destroy(gameObject);
    }
    void Start()
    {
        
        ramdomMoveCoroutine = RandomMoveRoutine();
        ChangeState(EnemyState.Moving);
    }
    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;

        // Exit logic for current state
        switch (currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Moving:
                StopCoroutine(ramdomMoveCoroutine);
                break;
            case EnemyState.Chase:
                break;
        }

        currentState = newState;

        // Enter logic for new state
        switch (currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Moving:
                StartCoroutine(ramdomMoveCoroutine);
                break;
            case EnemyState.Chase:
                break;
        }
    }
    private void MoveRandomly()
    {
        Debug.Log("Move Randomly");
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        enemyMovement.direction = randomDirection;
    }
    private IEnumerator RandomMoveRoutine()
    {
        Debug.Log("Start Random Move Routine");
        while (currentState == EnemyState.Moving)
        {
            MoveRandomly();
            yield return new WaitForSeconds(2f);
        }
    }
    private void OnDetectedPlayer(GameObject player)
    {
        Debug.Log("Player Detected: " + player.name);
        ChangeState(EnemyState.Chase);
        MoveTowardsPlayer(player);
    }
    private void MoveTowardsPlayer(GameObject player)
    {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        enemyMovement.moveSpeed = player.GetComponent<PlayerMovement>().moveSpeed * 1.2f; // Move faster than player
        enemyMovement.direction = directionToPlayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
                playerHealth.TakeDamage(collisionDamage);

            health.TakeDamage(collisionDamage);
        }
    }
}
