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
    CharacterDAO characterDAO;
    // Start is called before the first frame update
    void Start()
    {
        characterDAO = GetComponent<CharacterDAO>();
        list = characterDAO.GetAllCharacter();
        index = 0;
        SetSprite();
    }

    public void SetSprite()
    {
        PlayerName.text = list[index].CharacterName.ToString();
        PlayerImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(list[index].CharacterImage.Remove(0,1));
        Debug.Log(list[index].CharacterImage.Remove(0,1));
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
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
