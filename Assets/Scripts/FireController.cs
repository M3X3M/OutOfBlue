using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FireController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite ready_sprite;
    [SerializeField] private Sprite active_sprite;
    [SerializeField] private Sprite loading_sprite;
    [SerializeField] private GameObject player_go;

    [Header("Properties")]
    [SerializeField] private int fire_cooldown_time = 25;
    [SerializeField] private int fire_active_time = 5;

    private SpriteRenderer sr_component;
    private bool is_ready = false;
    private PlayerController player_controller;
    // Start is called before the first frame update
    void Start()
    {
        sr_component = GetComponent<SpriteRenderer>();
        player_controller = player_go.GetComponent<PlayerController>();
        StartCoroutine(FireCooldownTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if(is_ready && Input.GetButtonDown("Fire1"))
        {
            is_ready = false;
            player_controller.ActivateFire();
            sr_component.sprite = active_sprite;
            StartCoroutine(FireActiveTimer());
        }
    }

    private IEnumerator FireActiveTimer()
    {
        yield return new WaitForSeconds(fire_active_time);
        player_controller.DeactivateFire();
        sr_component.sprite = loading_sprite;
        StartCoroutine(FireCooldownTimer());
    }

    private IEnumerator FireCooldownTimer()
    {
        yield return new WaitForSeconds(fire_cooldown_time);
        sr_component.sprite = ready_sprite;
        is_ready = true;
    }
}
