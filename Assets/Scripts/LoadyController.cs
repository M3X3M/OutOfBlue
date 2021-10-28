using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadyController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]private float mov_x = -0.01f; 
    [SerializeField]private float mov_y = -0.05f; 
    [SerializeField]private float thresh_top = 4f;
    [SerializeField]private float thresh_bot = -4f;
    [SerializeField]private float start_pos_x = 11f;
    private float next_x, next_y;
    private float speed_multiplier;
    private int degrees = 0;
    private bool start = false;

    void FixedUpdate()
    {
        if(start)
        {
            if(degrees <= -360)
            {
                degrees = 0;
            }
            else
            {
                degrees -= 2;
            }
            transform.eulerAngles = Vector3.forward * degrees;
            
            if(transform.position.y < thresh_bot || transform.position.y > thresh_top)
            {
                mov_y = -(mov_y);
            }
            next_x = transform.position.x + mov_x * speed_multiplier;
            next_y = transform.position.y + mov_y * speed_multiplier;
            transform.position = new Vector2(next_x, next_y);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameData.AddScore(1);
        }
        else if(col.gameObject.CompareTag("Despawner"))
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
            GameData.AddScore(1);
        }
        else if(col.gameObject.CompareTag("PlayerFireBullet"))
        {
            Destroy(gameObject);
            GameData.AddScore(1);
        }
    }

    public void setSpeedMultiplier(float new_speed_multiplier)
    {
        speed_multiplier = new_speed_multiplier;
    }

    public float getThreshholdTop()
    {
        return thresh_top;
    }

    public float getThreshholdBot()
    {
        return thresh_bot;
    }

    public void startMoving()
    {
        start = true;
    }

    public void setStartPos(float pos_y)
    {
        if(pos_y < thresh_bot || pos_y > thresh_top)
        {
            return;
        }
        transform.position = new Vector2(start_pos_x, pos_y); 
    }
}
