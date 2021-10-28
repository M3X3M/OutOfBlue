using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreController : MonoBehaviour
{
    private Text text_component;
    private int last_score = 0;
    // Start is called before the first frame update
    void Start()
    {
        text_component = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(last_score != GameData.getScore())
        {
            last_score = GameData.getScore();
            text_component.text = $"Removed Bugged Data:\n{last_score} Bytes";
        }
    }
}
