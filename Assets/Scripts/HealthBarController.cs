using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Sprite health3;
    [SerializeField] private Sprite health2;
    [SerializeField] private Sprite health1;
    private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onHealthChanged(int health)
    {
        switch (health)
        {
            case 3:
                sr.sprite = health3;
                break;
            case 2:
                sr.sprite = health2;
                break;
            case 1:
                sr.sprite = health1;
                break;
            default:
                sr.sprite = health1;
                break;
        }
    }
}
