using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyHealthController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private int start_health = 1;
    [SerializeField] private float min_opacity = 0.5f;
    [SerializeField] private float hitmarker_volume = 0.5f;

    [Header("Burning Properties")]
    [SerializeField] private int amount_burn_damage = 3;
    [SerializeField] private int amount_burn_damage_per_burn = 1;
    [SerializeField] private float time_between_burns = 1f;

    [Header("References")]
    [SerializeField] private Sprite normal_sprite;
    [SerializeField] private Sprite burning_sprite;
    [SerializeField] private AudioClip burn_hiss;
    [SerializeField] private AudioClip hitmarker;
    
    private AudioSource hit_sound_source;
    private bool is_burning = false;
    private SpriteRenderer sr;
    private int health;

    void Start()
    {
        hit_sound_source = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        health = start_health;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("PlayerBullet"))
        {
            ReduceHealth(1);
        }
        else if(col.gameObject.CompareTag("PlayerFireBullet"))
        {
            ReduceHealth(1);
            Burn();
        }   
    }

    private void ReduceHealth(int dmg)
    {
        hit_sound_source.PlayOneShot(hitmarker, hitmarker_volume);
        health = health - dmg;
        if(health <= 0)
        {
            Destroy(this.gameObject);
            GameData.AddScore(start_health);
        }
        else
        {
            ChangeSpriteVis(health);
        }
    }

    private void ChangeSpriteVis(int curr_health)
    {
        float percentage = (float)curr_health / (float)start_health;
        float relative_percentage = min_opacity + (percentage * (1 - min_opacity));
        sr.color = new Color(1f, 1f, 1f, relative_percentage);
    }

    private void Burn()
    {
        hit_sound_source.PlayOneShot(burn_hiss, hitmarker_volume);
        if(!is_burning)
        {
            is_burning = true;
            sr.sprite = burning_sprite;
            StartCoroutine(GetBurned(amount_burn_damage));
        }
    }

    private IEnumerator GetBurned(int amount_burns)
    {
        for(int i = 0; i < amount_burns; i++)
        {
            yield return new WaitForSeconds(time_between_burns);
            ReduceHealth(amount_burn_damage_per_burn);
        }
        sr.sprite = normal_sprite;
        is_burning = false;
    }
}
