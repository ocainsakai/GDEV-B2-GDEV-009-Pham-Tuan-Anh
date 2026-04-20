using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    private float lastShootTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Shoot(Vector2 direction)
    {
        if (Time.time - lastShootTime < fireRate)
        {
            return;
        }
        lastShootTime = Time.time;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.SetDirection(direction);
        }
    }
}
