using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chidori : MonoBehaviour
{
    public bool CancauseDamage;

    private Collider2D collide;

    private void Start()
    {
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

        if (collision.gameObject.CompareTag("Enemy") && CancauseDamage)
        {
            collision.GetComponent<Enemy>().TakeDamagebyMelee(300);
        }
    }
}
