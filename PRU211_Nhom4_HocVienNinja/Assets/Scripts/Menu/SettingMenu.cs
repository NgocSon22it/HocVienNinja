using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{

    public Toggle MusicCheckBox;
    public Toggle SoundCheckBox;

    public AudioMixer MusicAudioMixer;
    public AudioMixer SoundAudioMixer;

    public static bool MusicStatus = true;
    public static bool SoundStatus = true;

    private void Start()
    {
        MusicCheckBox.isOn = MusicStatus;
        SoundCheckBox.isOn = SoundStatus;
    }
    public void ToggleMusic()
    {
        if (MusicCheckBox.isOn)
        {
            MusicAudioMixer.SetFloat("Volume", 0f);
            MusicStatus = true;
        }
        else
        {
            MusicAudioMixer.SetFloat("Volume", -80f);
            MusicStatus = false;
        }
    }
    public void ToggleSound()
    {
        if (SoundCheckBox.isOn)
        {
            SoundAudioMixer.SetFloat("Volume", 0f);
            SoundStatus = true;
        }
        else
        {
            SoundAudioMixer.SetFloat("Volume", -80f);
            SoundStatus = false;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
