using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameMaster gm;
    [SerializeField] private GameObject bullet_prefab;
    [SerializeField] private GameObject fire_bullet_prefab;
    [SerializeField] private Sprite ship1;
    [SerializeField] private Sprite ship2;

    [Header("Properties")]
    [SerializeField] private Vector2 speed_multiplier_ship1 = new Vector2(70, 70);
    [SerializeField] private float bullet_cooldown_ship1 = .5f;
    [SerializeField] private Vector2 speed_multiplier_ship2 = new Vector2(30, 30);
    [SerializeField] private float bullet_cooldown_ship2 = .2f;
    [SerializeField] private float hit_volume = 0.5f;

    
    private Rigidbody2D rigidbody_component;
    private SpriteRenderer spriterenderer_component;
    private AudioSource player_hitmarker_source;
    private Vector2 movement;
    private int health = 3;
    private bool cooldown_active = false;
    private float curr_bullet_cooldown;
    private Vector2 curr_speed_multiplier;
    private bool using_fire = false;

    private float inputX = 0;
    private float inputY = 0;

    // Start is called before the first frame update
    void Start()
    {
        //getting all the components
        rigidbody_component = GetComponent<Rigidbody2D>();
        spriterenderer_component = GetComponent<SpriteRenderer>();
        player_hitmarker_source = GetComponent<AudioSource>();

        //setting all needed options for the different ships (won't change in a run) 
        if(GameData.Is_Ship2)
        {
            spriterenderer_component.sprite = ship2;
            curr_bullet_cooldown = bullet_cooldown_ship2;
            curr_speed_multiplier = speed_multiplier_ship2;
        }
        else
        {
            spriterenderer_component.sprite = ship1;
            curr_bullet_cooldown = bullet_cooldown_ship1;
            curr_speed_multiplier = speed_multiplier_ship1;
        }
    }

    //the choice for what to put in which function was based on these forum posts:
    //summary: continous input in FixedUpdate(), single time ones in Update()
    //https://answers.unity.com/questions/1699747/how-to-write-input-in-update-and-physics-in-fixedu.html
    //https://forum.unity.com/threads/input-with-fixedupdate-avoiding-input-loss-with-high-frame-rates.619483/
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        if(Input.GetButton("Jump") && (!cooldown_active))
        {
            FireBullet(curr_bullet_cooldown);
        }
        if(Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void FixedUpdate()
    {
        movement = new Vector2(curr_speed_multiplier.x * inputX, curr_speed_multiplier.y * inputY);
        rigidbody_component.velocity = movement;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            ReduceHealth();
        }
        else if(col.gameObject.CompareTag("EnemyBullet"))
        {
            ReduceHealth();
        }
    }

    private void ReduceHealth()
    {
        player_hitmarker_source.PlayOneShot(player_hitmarker_source.clip, hit_volume);
        --health;
        gm.PlayerWasHit(health);
    }

    private void FireBullet(float time)
    {
        if(using_fire)
        {
            Instantiate(fire_bullet_prefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y), Quaternion.identity);
        }
        else
        {
            Instantiate(bullet_prefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y), Quaternion.identity);
        }
        cooldown_active = true;
        StartCoroutine(BulletCooldown(time));
    }

    private IEnumerator BulletCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        cooldown_active = false;
    }

    public void ActivateFire()
    {
        using_fire = true;        
    }

    public void DeactivateFire()
    {
        using_fire = false;
    }
}
