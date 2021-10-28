using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionBGController : MonoBehaviour
{
    [SerializeField] private int x_threshhold;
    [SerializeField] private int x_startpoint;
    [SerializeField] private float x_speed = -0.01f;
    Vector2 new_pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= x_threshhold)
        {
            new_pos = new Vector2(x_startpoint, transform.position.y);
            transform.position = new_pos;
        }
        else
        {
            new_pos = new Vector2(transform.position.x + x_speed, transform.position.y);
            transform.position = new_pos;
        }
    }
}
