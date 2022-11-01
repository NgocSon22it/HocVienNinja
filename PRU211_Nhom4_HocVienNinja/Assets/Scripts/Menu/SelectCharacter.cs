using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class SelectCharacter : MonoBehaviour
{
    public Image PlayerImage;

    public TextMeshProUGUI PlayerName;

    public static int CharacterID;

    int index;
    List<CharacterEntity> list;
    // Start is called before the first frame update
    void Start()
    {
        list = new List<CharacterEntity>();
        index = 0;
        CharacterEntity Sonruto = new CharacterEntity(1, "Sonruto", "CharacterImage/Sonruto");
        CharacterEntity Phongsuke = new CharacterEntity(2, "Phongsuke", "CharacterImage/Phongsuke");
        list.Add(Sonruto);
        list.Add(Phongsuke);
        SetSprite();
    }

    public void SetSprite()
    {
        PlayerName.text = list[index].CharacterName.ToString();
        PlayerImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(list[index].CharacterImage);
        Debug.Log(list[index].CharacterImage);
    }
    public void NextOption()
    {
        index++;
        if (index >= list.Count)
        {
            index = 0;
        }
        SetSprite();
    }
    public void PreviousOption()
    {
        index--;
        if (index < 0)
        {
            index = list.Count - 1;
        }
        SetSprite();

    }
    public void PlayGame()
    {
        CharacterID = list[index].CharacterID;
        GameManager.Score = 0;
        GameManager.Coin = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SelectMap.MapName, LoadSceneMode.Single);
    }
}
