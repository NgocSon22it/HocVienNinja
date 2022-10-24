using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccountManager : MonoBehaviour
{
    public static int AccountID;

    public GameObject FormMenu;
    public GameObject AllMenu;

    public TextMeshProUGUI PlayerNameUI;
    public TextMeshProUGUI PlayerCoinNumber;

    AccountDAO accountDAO;
    private void Start()
    {
        if (AccountID != 0)
        {
            FormMenu.SetActive(false);
            AllMenu.SetActive(true);
        }
        else
        {
            FormMenu.SetActive(true);
            AllMenu.SetActive(false);
        }
        accountDAO = GetComponent<AccountDAO>();
        InvokeRepeating(nameof(UserInformation), 0f,2f);

    }

    void UserInformation()
    {
        if (AllMenu.activeInHierarchy)
        {
            AccountEntity accountEntity = accountDAO.GetAccountbyID(AccountID);
            PlayerNameUI.text = accountEntity.Name;
            PlayerCoinNumber.text = accountEntity.Coin.ToString();
            Debug.Log(accountEntity.Name);
        }

    }

}
