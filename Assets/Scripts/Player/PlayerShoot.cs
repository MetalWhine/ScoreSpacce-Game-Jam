using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Seconds between shots, reduce to make faster")]
    private float shootDelay = 3f;

    [SerializeField]
    [Tooltip("Force of bullet shot (Speed)")]
    private float bulletForce = 3f;

    [SerializeField]
    private GameObject bulletPrefab;

    private Transform firePoint;

    private bool shooting = false;

    public void setShootDelay(float delay)
    {
        shootDelay = delay;
    }

    public float getShootDelay()
    {
        return shootDelay;
    }

    public void setBulletForce(float force)
    {
        bulletForce = force;
    }

    public float getBulletForce()
    {
        return bulletForce;
    }

    IEnumerator InstantiateBullet()
    {
        shooting = true;
        GameObject Bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(shootDelay);

        shooting = false;
    }

    private void Shoot()
    {
        if(shooting == false)
        {
            StartCoroutine(InstantiateBullet());
        }
    }

    private void Start()
    {
        firePoint = GameObject.FindGameObjectWithTag("Fire Point").transform;
        if(bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not set!");
            GetComponent<PlayerShoot>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }
}
