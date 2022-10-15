using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity
{
    public int CharacterID;
    public string CharacterName;
    public int TotalHealthPoint;
    public int TotalChakra;
    public int CharacterSpeed;
    public int CharacterDamage;
    public string CharacterImage;
    public string Description;
    public int AbilitiesID;

    public CharacterEntity()
    {

    }
    public CharacterEntity(int CharacterID, string CharacterName, string CharacterImage)
    {
        this.CharacterID = CharacterID;
        this.CharacterName = CharacterName;
        this.CharacterImage = CharacterImage;
    }
}
