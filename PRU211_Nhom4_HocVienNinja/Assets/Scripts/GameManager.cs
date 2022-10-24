using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int Score;
    public static int Coin;


    public TextMeshProUGUI ScoreUI;
    public TextMeshProUGUI CoinUI;

    public Transform SpawnPoint;
    public GameObject BarUI;

    public GameObject SettingMenu;
    public GameObject SettingIcon;
    public bool isPause;

    public GameObject DieMenu;

    AccountDAO accountDAO;

    GameObject player;
    public bool isDie;
    
    private void Start()
    {
        isPause = false;
        accountDAO = GetComponent<AccountDAO>();
        ScoreUI.text = Score.ToString();
        CoinUI.text = Coin.ToString();
        StartCoroutine(ActiveBarUI());
        if (SelectCharacter.CharacterID == 1)
        {
            player = Instantiate(Resources.Load("Character/Sonruto", typeof(GameObject)), SpawnPoint.position, SpawnPoint.rotation) as GameObject;
        }
        else
        {
            player = Instantiate(Resources.Load("Character/Phongsuke", typeof(GameObject)), SpawnPoint.position, SpawnPoint.rotation) as GameObject;
        }
        

    }
    private void Update()
    {
        CoinUI.text = Coin.ToString();
        ScoreUI.text = Score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause && !isDie)
        {
            SettingIcon.SetActive(false);
            SettingMenu.SetActive(true);
            OnSetting();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause && !isDie)
        {
            SettingIcon.SetActive(true);
            SettingMenu.SetActive(false);
            OutSetting();
        }
        if(player.GetComponent<Character>().CurrentHealthPoint <= 0)
        {
            isDie = true;
            DieMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OnSetting()
    {
        Time.timeScale = 0f;
        isPause = true;
    }

    public void OutSetting()
    {
        Time.timeScale = 1f;
        isPause = false;
    }
    public void SaveButton()
    {
        accountDAO.CreateScore(AccountManager.AccountID, Score);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
    }
    public void DoNotSaveButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
    }
    IEnumerator ActiveBarUI()
    {
        yield return new WaitForSeconds(3f);
        BarUI.SetActive(true);
    }
}
