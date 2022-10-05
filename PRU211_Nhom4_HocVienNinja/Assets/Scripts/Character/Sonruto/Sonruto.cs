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
    private void Awake()
    {
        CostFirstSkill = 5;
        CostSecondSkill = 20;
        CostThirdSkill = 30;
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
        if (Input.GetKeyDown(staticController.FirstSkill) && ReloadFirstSkill <= 0f && CurrentChakra >= CostFirstSkill)
        {
            Animator.SetTrigger("StrongAttack");
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
        if (Input.GetKeyDown(staticController.SecondSkill) && ReloadSecondSkill <= 0f && CurrentChakra >= CostSecondSkill)
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
        if (Input.GetKeyDown(staticController.ThirdSkill) && ReloadThirdSkill <= 0f && CurrentChakra >= CostThirdSkill)
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
