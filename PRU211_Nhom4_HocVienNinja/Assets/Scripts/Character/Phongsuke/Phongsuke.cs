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
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
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
        Source.clip = KatonSound;
        Source.Play();
    }

    public void PlaySoundChidori()
    {
        Source.clip = ChidoriSound;
        Source.Play();
    }
    public void PlaySoundAmaterasu()
    {
        Source.clip = AmaterasuSound;
        Source.Play();
    }

    public override void FirstSkill()
    {
        if (Input.GetKeyDown(staticController.FirstSkill))
        {
            Animator.SetTrigger("Katon");
        }
    }

    public override void SecondSkill()
    {
        if (Input.GetKeyDown(staticController.SecondSkill))
        {
            Source.clip = ChidoriSound;
            Source.Play();
            Animator.SetTrigger("FirstChidori");
            Chidori.transform.position = FirstChidori.transform.position;
            Chidori.SetActive(true);
            StartCoroutine(ChidoriLogic());
        } 
    }

    public override void ThirdSkill()
    {
        if (Input.GetKeyDown(staticController.ThirdSkill))
        {
            StartCoroutine(OpenSharingan());
        }
    }
    private IEnumerator ChidoriDash()
    {
        IsChidoriDash = true;
        float origialGravity = Rigid.gravityScale;
        Rigid.gravityScale = 0f;
        Rigid.velocity = new Vector2(transform.right.x * 2 * ChidoriDashPower, 0f);        
        yield return new WaitForSecondsRealtime(ChidoriDashTime);
        ChidoriObject.CancauseDamage = false;
        Rigid.gravityScale = origialGravity;
        IsChidoriDash = false;
        yield return new WaitForSecondsRealtime(2f);
        Chidori.SetActive(false);
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
        Source.Stop();
        Animator.SetBool("LastChidori", false);
        Chidori.SetActive(false);      
    }

    private IEnumerator OpenSharingan()
    {
        
        Sharingan.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            if (Vector2.Distance(currentEnemy.transform.position, transform.position) <= 20)
            {
                Instantiate(Amaterasu, currentEnemy.transform.position, currentEnemy.transform.rotation);
                Amaterasu.GetComponent<Amaterasu>().Enemy = currentEnemy;
            }
        }

        Sharingan.SetActive(false);
    }
}
