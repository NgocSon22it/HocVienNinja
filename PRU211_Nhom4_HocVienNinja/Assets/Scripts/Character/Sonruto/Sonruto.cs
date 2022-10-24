using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Sonruto : Character
{
    public Transform PlaceRasengan;
    public Transform PlaceRasengan1;
    public Transform PlaceRasengan2;
    public GameObject Rasengan;

    public AudioClip ClipRasengan;
    public AudioClip ClipSummomGamakichi;   


    public Transform PlaceGamakichi;
    public GameObject Gamakichi;

    public GameObject RasenShuriken;
    private void Awake()
    {
        SkillDAO skillDAO = GetComponent<SkillDAO>();
        CostFirstSkill = skillDAO.GetSkillbyID(1).Chakra;
        CooldownFirstSkill = skillDAO.GetSkillbyID(1).Cooldown;
        CostSecondSkill = skillDAO.GetSkillbyID(2).Chakra;
        CooldownSecondSkill = skillDAO.GetSkillbyID(2).Cooldown;
        CostThirdSkill = skillDAO.GetSkillbyID(3).Chakra;
        CooldownThirdSkill = skillDAO.GetSkillbyID(3).Cooldown;
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();     
    }

    // Update is called once per frame
    new void Update()
    {
        
        base.Update();
        FirstSkill();
        SecondSkill();
        ThirdSkill();

    }
    public void PlaysoundRasengan()
    {
        Source.clip = ClipRasengan;
        Source.Play();
    }
    public void PlaysoundSummonGamakichi()
    {
        Source.clip = ClipSummomGamakichi;
        Source.Play();
    }

    public override void FirstSkill()
    {
        if (Input.GetKeyDown(staticController.FirstSkill) && ReloadFirstSkill <= 0f && CurrentChakra >= CostFirstSkill && !IsSkilling)
        {
            Animator.SetTrigger("RasenShuriken");
            CurrentChakra -= CostFirstSkill;
            ReloadFirstSkill = CooldownFirstSkill;
        }
        else if(ReloadFirstSkill > 0)
        {
            ReloadFirstSkill -= Time.deltaTime;
        }
    }
    public override void SecondSkill()
    {
        if (Input.GetKeyDown(staticController.SecondSkill) && ReloadSecondSkill <= 0f && CurrentChakra >= CostSecondSkill && !IsSkilling)
        {
            Animator.SetTrigger("SummonGamakichi");
            CurrentChakra -= CostSecondSkill;
            ReloadSecondSkill = CooldownSecondSkill;

        }
        else if (ReloadSecondSkill > 0)
        {
            ReloadSecondSkill -= Time.deltaTime;
        }
    }
    public override void ThirdSkill()
    {
        if (Input.GetKeyDown(staticController.ThirdSkill) && ReloadThirdSkill <= 0f && CurrentChakra >= CostThirdSkill && !IsSkilling)
        {
            Animator.SetTrigger("Rasengan");
            CurrentChakra -= CostThirdSkill;
            ReloadThirdSkill = CooldownThirdSkill;
        }
        else if (ReloadThirdSkill > 0)
        {
            ReloadThirdSkill -= Time.deltaTime;
        }
    }
    public void CallRasengan()
    {
        StartCoroutine(MoveRasengan());

    }
    public void CallRasenShuriken()
    {
        StartCoroutine(MoveRasenShuriken());
    }
    public void CallGamakichi()
    {
        Instantiate(Gamakichi, PlaceGamakichi.position, Quaternion.identity);
    }

    IEnumerator MoveRasengan()
    {
        GameObject BulletIns = Instantiate(Rasengan, PlaceRasengan.position, PlaceRasengan.rotation);
        yield return new WaitForSecondsRealtime(3.9f);
        BulletIns.transform.position = PlaceRasengan1.position;
        yield return new WaitForSecondsRealtime(0.3f);
        BulletIns.transform.position = PlaceRasengan2.position;
    }
    IEnumerator MoveRasenShuriken()
    {
        GameObject BulletIns = Instantiate(RasenShuriken, PlaceRasengan.position, PlaceRasengan.rotation);
        yield return new WaitForSecondsRealtime(.85f);
        BulletIns.transform.position = PlaceRasengan1.position;
        yield return new WaitForSecondsRealtime(0.35f);
        BulletIns.transform.position = PlaceRasengan2.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
