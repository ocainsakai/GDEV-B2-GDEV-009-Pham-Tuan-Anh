using System;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private float detectionRadius = 5f;
    public Action<GameObject> onPlayerDetected;
    void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        Debug.Log("Colliders Detected: " + colliders.Length);
        foreach (var collider in colliders)        {
            if (collider.CompareTag("Player"))
            {
                onPlayerDetected?.Invoke(collider.gameObject);
                Debug.Log("Player Detected: " + collider.gameObject.name);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
