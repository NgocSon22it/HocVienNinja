using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chidori : Skill
{
    public bool CancauseDamage;

    private Collider2D collide;
    public AudioSource ChidoriHitSound;

    private void Start()
    {
        SkillDAO skillDAO = GetComponent<SkillDAO>();
        Damage = skillDAO.GetSkillbyID(1004).Damage;
        collide = GetComponent<Collider2D>();
        collide.enabled = false;
    }
    private void Update()
    {
        if (CancauseDamage)
        {
            collide.enabled = true;
        }
        else
        {
            collide.enabled = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) && CancauseDamage)
        {
            collision.GetComponent<Enemy>().TakeDamage(Damage);
            ChidoriHitSound.Play();
        }
    }
}
