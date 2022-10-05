using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int Score;

    public TextMeshProUGUI ScoreUI;

    public GameObject BarUI;

    CharacterDAO characterDAO;
    public Transform SpawnPoint;
    Character player;
    private void Start()
    {
        StartCoroutine(ActiveBarUI());
        characterDAO = GetComponent<CharacterDAO>();
        ScoreUI.text = Score.ToString();
        CharacterEntity character = characterDAO.GetCharacterbyID(SelectCharacter.CharacterID);
        Debug.Log("Character/" + character.CharacterName);
        
        GameObject instance = Instantiate(Resources.Load("Character/" + character.CharacterName, typeof(GameObject)), SpawnPoint.position, SpawnPoint.rotation) as GameObject;

    }
    private void Update()
    {
        ScoreUI.text = Score.ToString();
    }

    IEnumerator ActiveBarUI()
    {
        yield return new WaitForSeconds(5f);
        BarUI.SetActive(true);
    }
}
