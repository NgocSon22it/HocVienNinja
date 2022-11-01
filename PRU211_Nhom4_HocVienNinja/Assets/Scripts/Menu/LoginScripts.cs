using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginScripts : MonoBehaviour
{
    public TMP_InputField Username;
    public TMP_InputField Password;
    public TextMeshProUGUI LoginMessage;
    public TextMeshProUGUI UsernameMessage;
    public TextMeshProUGUI PasswordMessage;
    private AccountDAO accountDAO;

    public GameObject FormMenuUI;
    public GameObject AllSettingMenuUI;

    public AccountManager accountManager;
  
    // Start is called before the first frame update
    void Start()
    {
        accountDAO = gameObject.GetComponentInParent<AccountDAO>();
        UsernameMessage.text = "";
        PasswordMessage.text = "";
        LoginMessage.text = "";
    }

    public void Login()
    {
        var CheckLogin = accountDAO.CheckLogin(Username.text, Password.text);

        if(CheckLogin != null)
        {
            AccountManager.AccountID = CheckLogin.AccountID;
            AccountManager.AccountFullName = CheckLogin.Name;
            AccountManager.AccountCoin = CheckLogin.Coin;
            LoginMessage.text = "";
            FormMenuUI.SetActive(false);
            AllSettingMenuUI.SetActive(true);
            Password.text = "";
            Username.text = "";
            LoginMessage.text = "";
            PasswordMessage.text = "";
        }
        else
        {
            LoginMessage.text = "Tài khoản và Mật khẩu không đúng";
        }

        if (Username.text.Length == 0)
        {
            UsernameMessage.text = "Bạn cần điền tài khoản!";
        }
        else
        {
            UsernameMessage.text = "";
        }
        if (Password.text.Length == 0)
        {
            PasswordMessage.text = "Bạn cần điền mật khẩu!";
        }
        else
        {
            PasswordMessage.text = "";
        }
    }


}
