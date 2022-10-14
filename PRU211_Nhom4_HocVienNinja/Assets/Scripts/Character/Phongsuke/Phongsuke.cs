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
    public bool IsActive;
    public Chidori ChidoriObject;

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
        if (Input.GetKeyDown(staticController.SecondSkill) && IsActive)
        {
            ChidoriObject.CancauseDamage = true;
            StartCoroutine(ChidoriDash());

        }
        if (Input.GetKeyDown(staticController.SecondSkill) && !IsActive)
        {
            Source.clip = ChidoriSound;
            Source.Play();
            Chidori.SetActive(true);
            IsActive = true;
            StartCoroutine(ChidoriTurnOff());
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
        yield return new WaitForSeconds(ChidoriDashTime);
        ChidoriObject.CancauseDamage = false;
        Rigid.gravityScale = origialGravity;
        IsChidoriDash = false;
        yield return new WaitForSeconds(2f);
        Chidori.SetActive(false);
        IsActive = false;
    }

    public void KatonSkill()
    {
        Instantiate(Katon, KatonPoint.position, KatonPoint.rotation);
    }
       
    IEnumerator ChidoriTurnOff()
    {
        yield return new WaitForSeconds(5f);
        IsActive = false;
        Chidori.SetActive(false);
    }

    public List<Enemy> FindAllEnemyInRange(int Range)
    {
        List<Enemy> list = new List<Enemy>();
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            if (Vector2.Distance(currentEnemy.transform.position, transform.position) <= Range)
            {
                list.Add(currentEnemy);
            }
        }

        return list;
    }
    private IEnumerator OpenSharingan()
    {
        List<Enemy> list = FindAllEnemyInRange(20);
        Debug.Log(list.Count);
        Sharingan.SetActive(true);
        yield return new WaitForSeconds(2f);
        foreach(Enemy enemy in list)
        {
            Instantiate(Amaterasu, enemy.transform.position, enemy.transform.rotation);
            Amaterasu.GetComponent<Amaterasu>().Enemy = enemy;
        }
        Sharingan.SetActive(false);
    }
}
