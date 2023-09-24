using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
