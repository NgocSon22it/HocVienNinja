using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class SettingMenu : MonoBehaviour
{

     public Slider MusicSlider;
     public Slider SoundSlider;

     List<Charac> list;
     int index;
    private static int Map;

    // 1 = 1 player, 2 = player
    public static int Mode;
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void MinusMusicSlider()
    {
        MusicSlider.value -= 5;
    }

    public void PlusMusicSlider()
    {
        MusicSlider.value += 5;
    }

    public void MinusSoundSlider()
    {
        SoundSlider.value -= 5;
    }

    public void PlusSoundSlider()
    {
        SoundSlider.value += 5;
    }
    public void SelectMap(int Map)
    {

    }
    public void PlayMode(int mode)
    {
        Mode = mode;
    }
}
