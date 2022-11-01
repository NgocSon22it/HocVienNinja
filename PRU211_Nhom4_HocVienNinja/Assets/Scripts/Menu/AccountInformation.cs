using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccountInformation : MonoBehaviour
{
    public TextMeshProUGUI PlayerNameUI;
    public TextMeshProUGUI PlayerCoinNumber;
    // Start is called before the first frame update

    void Update()
    {
        PlayerNameUI.text = AccountManager.AccountFullName;
        PlayerCoinNumber.text = AccountManager.AccountCoin.ToString();
    }
}
