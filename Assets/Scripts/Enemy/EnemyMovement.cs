using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 direction = Vector2.zero;
    void FixedUpdate()
    {
        transform.position += (Vector3)(direction * moveSpeed * Time.fixedDeltaTime);
        if (MapBoundary.Instance != null)
            transform.position = (Vector3)MapBoundary.Instance.Clamp(transform.position);
    }
}
