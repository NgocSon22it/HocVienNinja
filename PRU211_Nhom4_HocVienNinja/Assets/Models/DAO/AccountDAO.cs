using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AccountDAO : MonoBehaviour
{
    string ConnectionStr = new HocVienNinjaConnect().GetConnectHocVienNinja();

    public AccountEntity CheckLogin(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Account where UserName = @username and PassWord = @password and [Delete] = 0";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", GetMD5(password));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountEntity account = new AccountEntity
                    {
                        AccountID = Convert.ToInt32(dr["Acc_ID"]),
                        Username = dr["UserName"].ToString(),
                        Password = dr["PassWord"].ToString(),
                        Name = dr["Name"].ToString(),
                        Coin = Convert.ToInt32(dr["Coin"]),

                    };
                    connection.Close();

                    return account;
                }

            }
            finally
            {
                connection.Close();
            }
        }
        return null;
    }
    public void CreateAccount(AccountEntity account)
    {
        string defaultAvatar = "/Account_Avatar/Default Avatar HocVienNinja.jpg";
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "insert into Account values(@name,@username,@password,0,0,@avatar,0,'Customer',0)";
            cmd.Parameters.AddWithValue("@username", account.Username);
            cmd.Parameters.AddWithValue("@password", GetMD5(account.Password));
            cmd.Parameters.AddWithValue("@name", account.Name);
            cmd.Parameters.AddWithValue("@avatar", defaultAvatar);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
    public AccountEntity GetAccountByUsername(string username)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Account where UserName = @username";
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountEntity a = new AccountEntity
                    {
                        AccountID = Convert.ToInt32(dr["Acc_ID"]),
                        Username = dr["UserName"].ToString(),
                        Password = dr["PassWord"].ToString(),
                        Name = dr["Name"].ToString(),
                        Coin = Convert.ToInt32(dr["Coin"])
                    };
                    connection.Close();

                    return a;

                }

            }
            finally
            {
                connection.Close();

            }
        }
        return null;
    }
    public static string GetMD5(string str)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] fromData = Encoding.UTF8.GetBytes(str);
        byte[] targetData = md5.ComputeHash(fromData);
        string byte2String = null;

        for (int i = 0; i < targetData.Length; i++)
        {
            byte2String += targetData[i].ToString("x2");

        }
        return byte2String;
    }
}
