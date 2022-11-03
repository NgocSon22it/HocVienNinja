using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasenShuriken : Skill
{
    private Rigidbody2D rb;
    public AudioSource CallSource;
    public AudioSource HitSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Damage = CommonValue.Skill[0].Damage;
        StartCoroutine(SetSpeed());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            collision.GetComponent<Enemy>().TakeDamage(Damage);
            HitSource.Play();

        }
    }
    IEnumerator SetSpeed()
    {
        CallSource.Play();
        yield return new WaitForSecondsRealtime(1f);
        rb.velocity = transform.right * 30;
        yield return new WaitForSeconds(2.2f);
        Destroy(gameObject);
    }
}
