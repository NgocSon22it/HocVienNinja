using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amaterasu : Skill
{
    public GameObject Enemy;
    public float TimeToDamage = 0.2f;
    private void Start()
    {
        Damage = CommonValue.Skill[5].Damage;
    }
    // Update is called once per frame
    void Update()
    {
        if(Enemy != null && Enemy.GetComponent<Collider2D>().enabled)
        {
            TimeToDamage -= Time.deltaTime;
            transform.position = Enemy.transform.position;
            if (TimeToDamage <= 0)
            {
                Enemy.GetComponent<Enemy>().TakeDamage(Damage);
                TimeToDamage = 0.2f;
            }
        }
        else
        {
            Destroy(gameObject);
        }  
    }
}
