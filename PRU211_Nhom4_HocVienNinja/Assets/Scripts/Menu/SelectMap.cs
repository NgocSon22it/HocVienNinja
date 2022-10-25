using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMap : MonoBehaviour
{
    public static string MapName;
    
    public void MapSelected(string name)
    {
        MapName = name;
    }
}
