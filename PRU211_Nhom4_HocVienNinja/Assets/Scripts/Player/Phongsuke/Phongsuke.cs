using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phongsuke : Player
{

    private void Awake()
    {
        PlayerNumber = 2;
    }
    new void Start()
    {
        
        CharacterName = "Phongsuke";
        TotalHealthPoint = 100;
        CurrentHealthPoint = 100;
        TotalChakra = 100;
        CurrentChakra = 100;
        AttackRange = 3f;
        CharacterDamage = 10;
        CharacterSpeed = 10;
        CooldownFirstSkill = 1;
        CooldownSecondSkill = 1;
        CooldownThirdSkill = 1;
        base.Start();
    }

    new void Update()
    {
        base.Update();

    }
}
