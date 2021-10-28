using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] private float speed = 0.05f;

    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
    }

    public void setSpeed(float new_speed)
    {
        if(speed > 0f)
        {
            speed = new_speed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Despawner")
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "EnemyBullet")
        {
            Destroy(gameObject);
        }
    }
}
