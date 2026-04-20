using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 direction;
    public float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime = 3f;
        // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime);
        if (MapBoundary.Instance != null && !MapBoundary.Instance.IsInBounds(transform.position))
            Destroy(gameObject);
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
