using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HocVienNinjaConnect
{
    string Server = "ninjagame.database.windows.net";
    string id = "ninjagame";
    string password = "NinjaS31504!";
    string database = "Ninja";
    public string GetConnectHocVienNinja()
    {
        return $"Server = {Server}; uid = {id}; pwd = {password}; Database = {database}; Trusted_Connection = False;";
    }
}
