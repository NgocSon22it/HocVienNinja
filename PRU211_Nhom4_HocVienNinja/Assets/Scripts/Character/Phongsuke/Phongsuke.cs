using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Phongsuke : Character
{

    //Katon
    public GameObject Katon;
    public AudioClip KatonSound;

    // Chidori
    private bool CanDash1 = true;
    private bool IsDashing1;
    public float DashingPower1 = 48f;
    public float DashingTime1 = 0.2f;
    public float DashingCooldown1 = 1;
    public AudioClip ChidoriSound;
    public GameObject Chidori;
    public bool isActive;
    public Chidori chidoriobject;

    //Amaterasu
    public AudioClip AmaterasuSound;
    public CinemachineVirtualCamera cam;
    public float fovAim;
    public float fovNorm;
    public GameObject Sharingan;
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        if (IsDashing1)
        {
            return;
        }
        base.Update();
        FirstSkill();
        SecondSkill();
        ThirdSkill();
    }

    public override void FirstSkill()
    {
        // base.FirstSkill();
        if (Input.GetKeyDown(staticController.FirstSkill))
        {
            Source.clip = KatonSound;
            Source.Play();
            StartCoroutine(KatonL());
        }

    }

    public override void SecondSkill()
    {
        if (Input.GetKeyDown(staticController.SecondSkill) && isActive && CanDash1)
        {
            chidoriobject.CancauseDamage = true;
            StartCoroutine(Dash1());

        }
        if (Input.GetKeyDown(staticController.SecondSkill) && !isActive)
        {
            Source.clip = ChidoriSound;
            Source.Play();
            Chidori.SetActive(true);
            isActive = true;
        }
        
    }

    public override void ThirdSkill()
    {
        if (Input.GetKey(staticController.ThirdSkill))
        {
            
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, fovAim, Time.deltaTime * 10);
            if(cam.m_Lens.OrthographicSize <= 1.1)
            {
                Sharingan.SetActive(true);
            }
        }
        else
        {

            Sharingan.SetActive(false);
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, fovNorm, Time.deltaTime * 10);
        }
    }
    private IEnumerator Dash1()
    {
        CanDash1 = false;
        IsDashing1 = true;
        float origialGravity = Rigid.gravityScale;
        Rigid.gravityScale = 0f;
        Rigid.velocity = new Vector2(transform.right.x * 2 * DashingPower1, 0f);        
        yield return new WaitForSeconds(DashingTime1);
        chidoriobject.CancauseDamage = false;
        Rigid.gravityScale = origialGravity;
        IsDashing1 = false;
        yield return new WaitForSeconds(DashingCooldown1);
        CanDash1 = true;
        Chidori.SetActive(false);
        isActive = false;
    }
    private IEnumerator KatonL()
    {
        
        yield return new WaitForSeconds(1.5f);
        Instantiate(Katon, transform.position, transform.rotation);
    }

}
