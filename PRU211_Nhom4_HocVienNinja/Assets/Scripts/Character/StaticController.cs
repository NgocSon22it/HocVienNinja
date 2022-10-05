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

    private void Start()
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
}
