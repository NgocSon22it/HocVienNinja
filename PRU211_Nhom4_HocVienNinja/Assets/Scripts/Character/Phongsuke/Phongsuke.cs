using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Phongsuke : Character
{  

    //Katon
    public GameObject Katon;
    public AudioClip KatonSound;
    public Transform KatonPoint;

    // Chidori
    private bool IsChidoriDash;
    public float ChidoriDashPower = 48f;
    public float ChidoriDashTime = 0.2f;
    public AudioClip ChidoriSound;
    public GameObject Chidori;
    public Chidori ChidoriObject;
    public Transform FirstChidori;
    public Transform LastChidori;

    //Amaterasu
    public AudioClip AmaterasuSound;
    public GameObject Sharingan;
    public GameObject Amaterasu;
    public float AmaterasuRange;

    private void Awake()
    {
        CostFirstSkill = CommonValue.Skill[3].Chakra;
        CooldownFirstSkill = CommonValue.Skill[3].Cooldown;

        CostSecondSkill = CommonValue.Skill[4].Chakra;
        CooldownSecondSkill = CommonValue.Skill[4].Cooldown;

        CostThirdSkill = CommonValue.Skill[5].Chakra;
        CooldownThirdSkill = CommonValue.Skill[5].Cooldown;
    }
    new void Start()
    {
        base.Start();
        Debug.Log(CostFirstSkill + " " + CostSecondSkill + " " + CostThirdSkill);
    }

    new void Update()
    {
        if (!IsStart)
        {
            return;
        }
        if (IsDashing)
        {
            return;
        }
        if (IsChidoriDash)
        {
            return;
        }
        base.Update();
        FirstSkill();
        SecondSkill();
        ThirdSkill();
    }

    public void PlaySoundKaton()
    {
        SkillSource.clip = KatonSound;
        SkillSource.Play();
    }

    public void PlaySoundChidori()
    {
        SkillSource.clip = ChidoriSound;
        SkillSource.Play();
    }
    public void PlaySoundAmaterasu()
    {
        SkillSource.clip = AmaterasuSound;
        SkillSource.Play();
    }

    public override void FirstSkill()
    {
        if (Input.GetKeyDown(staticController.FirstSkill) && ReloadFirstSkill <= 0f && CurrentChakra >= CostFirstSkill && !IsSkilling)
        {
            Animator.SetTrigger("Katon");
            CurrentChakra -= CostFirstSkill;
            ReloadFirstSkill = CooldownFirstSkill;
        }
        else if (ReloadFirstSkill > 0)
        {
            ReloadFirstSkill -= Time.deltaTime;
        }
    }

    public override void SecondSkill()
    {
        if (Input.GetKeyDown(staticController.SecondSkill) && ReloadSecondSkill <= 0f && CurrentChakra >= CostSecondSkill && !IsSkilling)
        {
            SkillSource.clip = ChidoriSound;
            SkillSource.Play();
            Animator.SetTrigger("FirstChidori");
            Chidori.transform.position = FirstChidori.transform.position;
            Chidori.SetActive(true);
            StartCoroutine(ChidoriLogic());
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
            SkillSource.clip = AmaterasuSound;
            SkillSource.Play();
            StartCoroutine(OpenSharingan());
            CurrentChakra -= CostThirdSkill;
            ReloadThirdSkill = CooldownThirdSkill;
        }
        else if (ReloadThirdSkill > 0)
        {
            ReloadThirdSkill -= Time.deltaTime;
        }
    }
    private IEnumerator ChidoriDash()
    {
        IsChidoriDash = true;
        IsHurt = true;
        Rigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        float origialGravity = Rigid.gravityScale;
        Rigid.gravityScale = 0f;
        Rigid.velocity = new Vector2(transform.right.x * 2 * ChidoriDashPower, 0f);
        yield return new WaitForSecondsRealtime(ChidoriDashTime);
        ChidoriObject.CancauseDamage = false;
        Rigid.gravityScale = origialGravity;
        IsChidoriDash = false;
        yield return new WaitForSecondsRealtime(1f);
        Rigid.constraints = RigidbodyConstraints2D.None;
        Rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSecondsRealtime(1f);
        Chidori.SetActive(false);
        IsHurt = false;
    }

    public void KatonSkill()
    {
        Instantiate(Katon, KatonPoint.position, KatonPoint.rotation);
    }

    IEnumerator ChidoriLogic()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Animator.SetBool("LastChidori", true);
        Chidori.transform.position = LastChidori.transform.position;
        ChidoriObject.CancauseDamage = true;
        StartCoroutine(ChidoriDash());
        yield return new WaitForSecondsRealtime(1f);
        SkillSource.Stop();
        Animator.SetBool("LastChidori", false);
        Chidori.SetActive(false);
    }
    private IEnumerator OpenSharingan()
    {

        StartSkill();
        Sharingan.SetActive(true);
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(transform.position, AmaterasuRange, LayerToAttack);
        yield return new WaitForSecondsRealtime(2f);
        if (hitEnemy != null)
        {
            foreach (Collider2D enemy in hitEnemy)
            {

                GameObject instance = Instantiate(Amaterasu, enemy.transform.position, enemy.transform.rotation);
                instance.GetComponent<Amaterasu>().Enemy = enemy.gameObject;
                Destroy(instance, 5f);
                Debug.Log(enemy.name);
            }

        }
        Sharingan.SetActive(false);
        EndSkill();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, AmaterasuRange);
    }
}
