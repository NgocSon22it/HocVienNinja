using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public static int AccountID;
    public static string AccountFullName;
    public static int AccountCoin;


    public GameObject FormMenu;
    public GameObject AllMenu;
    
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
    }



}
