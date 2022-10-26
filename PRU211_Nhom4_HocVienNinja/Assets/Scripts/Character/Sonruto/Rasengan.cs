using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Rasengan : Skill
{
    public GameObject Explosion;
    public float Range;
    public LayerMask LayerToAttack;
    // Start is called before the first frame update
    void Start()
    {
        SkillDAO skillDAO = GetComponent<SkillDAO>();
        Damage = skillDAO.GetSkillbyID(3).Damage;
        StartCoroutine(SetupRasengan());
    }

    IEnumerator SetupRasengan()
    {
        yield return new WaitForSecondsRealtime(4.5f);
        GameObject a = Instantiate(Explosion, transform.position, transform.rotation);             
        yield return new WaitForSecondsRealtime(0.3f);
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(transform.position, Range, LayerToAttack);

        if (hitEnemy != null)
        {
            foreach (Collider2D enemy in hitEnemy)
            {
                enemy.GetComponent<Enemy>().TakeDamage(Damage);
            }
        }
        Destroy(a, 5f);
        Destroy(gameObject);
    }
}
