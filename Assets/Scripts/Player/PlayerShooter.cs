using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Shooter shooter;
    [SerializeField] private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Shoot()
    {
        shooter.Shoot(playerMovement.direction);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
}
