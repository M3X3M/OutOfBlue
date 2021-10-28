using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemyController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float pos_x = 7f;
    [SerializeField] private float thresh_bot = -3f;
    [SerializeField] private float thresh_top = 3f;
    [SerializeField] private float mov_x = 0.05f;
    [SerializeField] private float mov_y = 0.05f;
    [SerializeField] private float bullet_cooldown = 1.5f;
    [SerializeField] private float bullet_velocity = -0.05f;
    [Header("References")]
    [SerializeField] private GameObject responder_bullet_prefab;
    private float speed_multiplier = 1f;
    private bool has_shot = false;
    private float next_x, next_y;
    private GameObject curr_bullet_go;
    

    void FixedUpdate()
    {
        if(transform.position.x > pos_x)
        {
            next_x = transform.position.x - (speed_multiplier * mov_x);
            next_y = transform.position.y;
        }
        else
        {
            if(transform.position.y < thresh_bot || transform.position.y > thresh_top)
            {
                mov_y = -(mov_y);
            }
            next_x = transform.position.x;
            next_y = transform.position.y + mov_y * speed_multiplier;
        }
        transform.position = new Vector2(next_x, next_y);
    }

    void Update()
    {
        if((!has_shot) && transform.position.x <= pos_x)
        {
            has_shot = true;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        curr_bullet_go = Instantiate(responder_bullet_prefab, new Vector2(transform.position.x - 0.5f, transform.position.y),
        Quaternion.identity);
        curr_bullet_go.GetComponent<EnemyBulletController>().setSpeeds(bullet_velocity, 0f);
        yield return new WaitForSeconds(bullet_cooldown);
        has_shot = false;
   }

   public void SetDifficulty(float vertitcal_speed, float bullet_cooldown, float bullet_velocity)
   {
       if(this.mov_y > 0)
       {
            this.mov_y = vertitcal_speed;
       }
       else
       {
            this.mov_y = -vertitcal_speed;
       }

       this.bullet_cooldown = bullet_cooldown;
       this.bullet_velocity = bullet_velocity;
   }
}
