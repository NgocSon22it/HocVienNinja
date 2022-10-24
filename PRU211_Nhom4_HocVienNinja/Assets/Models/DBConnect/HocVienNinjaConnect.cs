using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HocVienNinjaConnect
{
    string Server = "localhost";
    string id = "sa";
    string password = "123456";
    string database = "Ninja";
    public string GetConnectHocVienNinja()
    {
        return $"Server = {Server}; uid = {id}; pwd = {password}; Database = {database}; Trusted_Connection = False;";
    }
}
