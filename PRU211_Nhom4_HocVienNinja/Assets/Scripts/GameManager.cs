using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int Score;

    public static int CharacterID;

    public static string MapName;

    public TextMeshProUGUI ScoreUI;

    
    private void Start()
    {
        ScoreUI.text = Score.ToString();
        if(SettingMenu.Mode == 1)
        {
            Debug.Log("1  nguoiwf");
        }
        else
        {
            Debug.Log("2  nguoiwf");

        }
    }

    private void Update()
    {
        ScoreUI.text = Score.ToString();
    }
}
