using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private string powerupType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(powerupType == "Shoot")
            {
                float bulletForce = collision.GetComponentInChildren<PlayerShoot>().getBulletForce();
                if (bulletForce < 60f)
                {
                    collision.GetComponentInChildren<PlayerShoot>().setBulletForce(bulletForce += 1f);
                }
                float bulletDelay = collision.GetComponentInChildren<PlayerShoot>().getShootDelay();
                if(bulletDelay > 0.15)
                {
                    collision.GetComponentInChildren<PlayerShoot>().setShootDelay(bulletDelay -= 0.05f);
                }
            }
            else if(powerupType == "Speed")
            {
                float speed = collision.GetComponentInChildren<PlayerMove>().getPlayerSpeed();
                if(speed < 45f)
                {
                    collision.GetComponentInChildren<PlayerMove>().setPlayerSpeed(speed += 0.5f);
                }
            }
            Destroy(gameObject);
        }
    }
}
