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
        health.onDeath += () => Destroy(gameObject);
    }
   
}
