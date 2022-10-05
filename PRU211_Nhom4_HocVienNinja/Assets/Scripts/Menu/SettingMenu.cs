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
    private static int Map;
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

}
