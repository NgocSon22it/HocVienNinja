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

    List<Charac> list;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        list = new List<Charac>();
        Charac naruto = new Charac(1, "Naruto", "Naruto_Image");
        Charac sasuke = new Charac(2, "Sasuke", "Sasuke_Image");
        Charac xuka = new Charac(3, "Xuka", "Phong");
        Charac Thien = new Charac(4, "Ong Troi", "Thien");
        // Charac xuka = new Charac(3, "Xuka", "Phong");
        list.Add(naruto);
        list.Add(sasuke);
        list.Add(xuka);
        list.Add(Thien);

        //PlayerImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(naruto.PlayerImage);
        SetSprite();
    }

    public void SetSprite()
    {
        PlayerName.text = list[index].PlayerName.ToString();
        PlayerImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(list[index].PlayerImage);
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
}
