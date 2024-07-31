using System.Collections;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public float timeBetweenShoot = 0.3f;
    public float speed = 50f;

    protected Coroutine _currentCoroutine;
    protected bool isShooting = false;
    protected float lastShootTime = 0f;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (Time.time >= lastShootTime + timeBetweenShoot)
            {
                Shoot();
                lastShootTime = Time.time;
            }
            yield return null;
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;

        ShakeCamera.Instance.Shake();
    }

    public void StartShoot()
    {
        if (!isShooting)
        {
            isShooting = true;
            _currentCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    public void StopShoot()
    {
        if (isShooting)
        {
            isShooting = false;
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
        }
    }
}
