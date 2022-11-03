using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HocVienNinjaConnect
{
    string Server = "hocvienninja.database.windows.net";
    string id = "ninja";
    string password = "Hocvien123";
    string database = "Ninja";
    public string GetConnectHocVienNinja()
    {
        return $"Server = {Server}; uid = {id}; pwd = {password}; Database = {database}; Trusted_Connection = False;";
    }
}
