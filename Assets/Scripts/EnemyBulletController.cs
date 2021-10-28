using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] float hor_speed = -0.05f;
    [SerializeField] float vert_speed = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + hor_speed, transform.position.y + vert_speed);
    }

    public void setSpeeds(float hor_speed, float vert_speed)
    {
        if(hor_speed > 0f)
            return;

        this.hor_speed = hor_speed;
        this.vert_speed = vert_speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.CompareTag("Despawner"))
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.CompareTag("PlayerFireBullet"))
        {
            Destroy(gameObject);
        }
    }
}
