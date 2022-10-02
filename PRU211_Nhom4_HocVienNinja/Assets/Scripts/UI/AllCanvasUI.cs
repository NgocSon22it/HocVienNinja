using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AllCanvasUI : MonoBehaviour
{

    public Player Player = null;
    public Player[] GetAllPlayer;

    [Header("Health & Chakra")]
    [SerializeField] private Image CurrentHealth;
    [SerializeField] private Image CurrentChakra;
    [SerializeField] private TextMeshProUGUI NumberHealth;
    [SerializeField] private TextMeshProUGUI NumberChakra;


    [Header("Skill")]
    [SerializeField] private Image CurrentCooldownFirstSkill;
    [SerializeField] private TextMeshProUGUI NumberCooldownFirstSkill;

    [SerializeField] private Image CurrentCooldownSecondSkill;
    [SerializeField] private TextMeshProUGUI NumberCooldownSecondSkill;

    [SerializeField] private Image CurrentCooldownThirdSkill;
    [SerializeField] private TextMeshProUGUI NumberCooldownThirdSkill;

    [SerializeField] private TextMeshProUGUI CostFirstSkill;
    [SerializeField] private TextMeshProUGUI CostSecondSkill;
    [SerializeField] private TextMeshProUGUI CostThirdSkill;


    // Start is called before the first frame update
    void Start()
    {
       
        
        CostFirstSkill.text = "0";
        CostSecondSkill.text = "0";
        CostThirdSkill.text = "0";
        CurrentHealth.fillAmount = 1f;
        CurrentChakra.fillAmount = 1f;
        CurrentCooldownFirstSkill.fillAmount = 0f;
        CurrentCooldownSecondSkill.fillAmount = 0f;
        CurrentCooldownThirdSkill.fillAmount = 0f;
        NumberCooldownFirstSkill.text = "";
        NumberCooldownSecondSkill.text = "";
        NumberCooldownThirdSkill.text = "";
        NumberHealth.text = Player.CurrentHealthPoint + " / " + Player.TotalHealthPoint;
        NumberChakra.text = Player.CurrentChakra + " / " + Player.TotalChakra;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name.Equals("PlayerOneUI"))
        {
            GetAllPlayer = GameObject.FindObjectsOfType<Player>();
            foreach (Player player in GetAllPlayer)
            {
                if (player.PlayerNumber == 1)
                {
                    Player = player;
                }
            }
        }
        else
        {
            GetAllPlayer = GameObject.FindObjectsOfType<Player>();
            foreach (Player player in GetAllPlayer)
            {
                if (player.PlayerNumber == 2)
                {
                    Player = player;
                }
            }
        }
        CurrentHealth.fillAmount = Player.CurrentHealthPoint / (float) Player.TotalHealthPoint;
        CurrentChakra.fillAmount = Player.CurrentChakra / (float) Player.TotalChakra;
        NumberHealth.text = Player.CurrentHealthPoint + " / " + Player.TotalHealthPoint;
        NumberChakra.text = Player.CurrentChakra + " / " + Player.TotalChakra;

        CurrentCooldownFirstSkill.fillAmount = Player.ReloadFirstSkill / Player.CooldownFirstSkill;
        CurrentCooldownSecondSkill.fillAmount = Player.ReloadSecondSkill / Player.CooldownSecondSkill;
        CurrentCooldownThirdSkill.fillAmount = Player.ReloadThirdSkill / Player.CooldownThirdSkill;

        if(Player.ReloadFirstSkill <= 0f)
        {
            NumberCooldownFirstSkill.text = "";
        }
        else
        {
            NumberCooldownFirstSkill.text = Player.ReloadFirstSkill.ToString("F2");
        }

        if (Player.ReloadSecondSkill <= 0f)
        {
            NumberCooldownSecondSkill.text = "";
        }
        else
        {
            NumberCooldownSecondSkill.text = Player.ReloadSecondSkill.ToString("F2");
        }
        if (Player.ReloadThirdSkill <= 0f)
        {
            NumberCooldownThirdSkill.text = "";
        }
        else
        {
            NumberCooldownThirdSkill.text = Player.ReloadThirdSkill.ToString("F2");
        }
        if(Player.CurrentChakra < Player.CostFirstSkill && Player.ReloadFirstSkill <= 0f)
        {
            CurrentCooldownFirstSkill.fillAmount = 1f;
        }
        if (Player.CurrentChakra < Player.CostSecondSkill && Player.ReloadSecondSkill <= 0f)
        {
            CurrentCooldownSecondSkill.fillAmount = 1f;
        }
        if (Player.CurrentChakra < Player.CostThirdSkill && Player.ReloadThirdSkill <= 0f)
        {
            CurrentCooldownThirdSkill.fillAmount = 1f;
        }

    }
}
