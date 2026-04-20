using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Health health;
    void Awake()
    {
        if (health == null)
        {
            health = GetComponent<Health>();
        }
        health.onDeath += () =>
        {
            if (GameManager.Instance != null)
                GameManager.Instance.Lose();
            Destroy(gameObject);
        };
    }
   
}
