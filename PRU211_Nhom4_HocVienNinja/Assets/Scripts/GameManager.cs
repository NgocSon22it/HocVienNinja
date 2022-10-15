using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int Score;

    public TextMeshProUGUI ScoreUI;
    public Transform SpawnPoint;
    public GameObject BarUI;
    Character player;
    private void Start()
    {
        Score = 100;
        ScoreUI.text = Score.ToString();
        StartCoroutine(ActiveBarUI());
        if(SelectCharacter.CharacterID == 1)
        {
            GameObject instance = Instantiate(Resources.Load("Character/Sonruto", typeof(GameObject)), SpawnPoint.position, SpawnPoint.rotation) as GameObject;
        }
        else
        {
            GameObject instance = Instantiate(Resources.Load("Character/Phongsuke", typeof(GameObject)), SpawnPoint.position, SpawnPoint.rotation) as GameObject;
        }
        

    }
    private void Update()
    {
        
        ScoreUI.text = Score.ToString();
    }

    IEnumerator ActiveBarUI()
    {
        yield return new WaitForSeconds(3f);
        BarUI.SetActive(true);
    }
}
