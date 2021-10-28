using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject score_text_go;
    private Text score_text_comp;
    void Start()
    {
        score_text_comp = score_text_go.GetComponent<Text>();
        score_text_comp.text = $"Last Score:\n\n{GameData.getScore()}";
    }
    public void StartGame()
    {
        SceneManager.LoadScene("PlayingScene");
    }
}
