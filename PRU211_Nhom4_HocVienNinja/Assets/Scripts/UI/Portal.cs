using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    [SerializeField]
    string MapName;
    [SerializeField]
    GameObject Manager;

    AccountDAO accountDAO;

    private void Start()
    {
        accountDAO = GetComponent<AccountDAO>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (MapName.Equals("Last"))
            {
                accountDAO.updateAccountItem(AccountManager.AccountID, 1, CommonValue.ItemOneQuantity);
                accountDAO.updateAccountItem(AccountManager.AccountID, 2, CommonValue.ItemTwoQuantity);
                Manager.GetComponent<GameManager>().GameOver = true;
            }
            else
            {
                accountDAO.updateAccountItem(AccountManager.AccountID, 1, CommonValue.ItemOneQuantity);
                accountDAO.updateAccountItem(AccountManager.AccountID, 2, CommonValue.ItemTwoQuantity);
                SceneManager.LoadScene(MapName, LoadSceneMode.Single);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (MapName.Equals("Last"))
            {
                accountDAO.updateAccountItem(AccountManager.AccountID, 1, CommonValue.ItemOneQuantity);
                accountDAO.updateAccountItem(AccountManager.AccountID, 2, CommonValue.ItemTwoQuantity);
                Manager.GetComponent<GameManager>().GameOver = true;
            }
            else
            {
                accountDAO.updateAccountItem(AccountManager.AccountID, 1, CommonValue.ItemOneQuantity);
                accountDAO.updateAccountItem(AccountManager.AccountID, 2, CommonValue.ItemTwoQuantity);
                SceneManager.LoadScene(MapName, LoadSceneMode.Single);
            }
        }
    }
}
