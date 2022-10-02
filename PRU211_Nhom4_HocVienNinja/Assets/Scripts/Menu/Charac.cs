using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charac
{
    public int PlayerID;
    public string PlayerName;
    public string PlayerImage;

    public Charac(int PlayerID, string PlayerName, string PlayerImage) 
    => (this.PlayerID, this.PlayerName, this.PlayerImage) = (PlayerID, PlayerName, PlayerImage);

}
