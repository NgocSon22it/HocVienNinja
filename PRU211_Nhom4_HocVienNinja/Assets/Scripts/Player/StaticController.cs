using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticController : MonoBehaviour
{
    public string Horizontal;
    public string Vertical;

    public KeyCode NormalAttackKey;
    public KeyCode JumpKey;
    public KeyCode DashKey;
    public KeyCode FirstSkill;
    public KeyCode SecondSkill;
    public KeyCode ThirdSkill;

    public Player player;


    private void Start()
    {
        player = GetComponent<Player>();
        if(player.PlayerNumber == 1)
        {
            Horizontal = "Horizontal";
            Vertical = "Vertical";
            NormalAttackKey = KeyCode.J;
            JumpKey = KeyCode.K;
            DashKey = KeyCode.L;
            FirstSkill = KeyCode.U;
            SecondSkill = KeyCode.I;
            ThirdSkill = KeyCode.O;
        }
        else if(player.PlayerNumber == 2)
        {
            Horizontal = "Horizontal2";
            Vertical = "Vertical2";
            NormalAttackKey = KeyCode.Keypad1;
            JumpKey = KeyCode.Keypad2;
            DashKey = KeyCode.Keypad3;
            FirstSkill = KeyCode.Keypad4;
            SecondSkill = KeyCode.Keypad5;
            ThirdSkill = KeyCode.Keypad6;
        }
    }
}
