using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipChangeController : MonoBehaviour
{
    private Image image_comp;
    [SerializeField] private Sprite ship1;
    [SerializeField] private Sprite ship2;
    [SerializeField] private Text description_text;

    private string text_ship1 = $"Speed +\nFirerate -";
    private string text_ship2 = $"Speed -\nFirerate +";
    // Start is called before the first frame update
    void Start()
    {
        GameData.Is_Ship2 = false;
        image_comp = GetComponent<Image>();
    }

    public void changeShip()
    {
        if(GameData.Is_Ship2)
        {
            GameData.Is_Ship2 = false;
            image_comp.sprite = ship1;
            description_text.text = text_ship1;
        }
        else
        {
            GameData.Is_Ship2 = true;
            image_comp.sprite = ship2;
            description_text.text = text_ship2;
        }
    }
}
