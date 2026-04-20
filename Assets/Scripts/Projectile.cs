using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 direction;
    public float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int maxBounces = 3;
    private int bounceCount;

    void FixedUpdate()
    {
        Vector2 newPos = (Vector2)transform.position + direction * speed * Time.fixedDeltaTime;

        if (MapBoundary.Instance != null)
        {
            Bounds b = MapBoundary.Instance.Bounds;

            if (newPos.x <= b.min.x || newPos.x >= b.max.x)
            {
                direction.x = -direction.x;
                newPos.x = Mathf.Clamp(newPos.x, b.min.x, b.max.x);
                bounceCount++;
            }
            if (newPos.y <= b.min.y || newPos.y >= b.max.y)
            {
                direction.y = -direction.y;
                newPos.y = Mathf.Clamp(newPos.y, b.min.y, b.max.y);
                bounceCount++;
            }

            if (bounceCount > maxBounces)
            {
                Destroy(gameObject);
                return;
            }
        }

        transform.position = newPos;
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (health != null && collision.gameObject.CompareTag("Enemy"))
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(DestroyAfterTime(lifetime));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
