using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasenShuriken : Skill
{
    private Rigidbody2D rb;
    public AudioSource Source;
    public AudioClip RasenShurikenSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SkillDAO skillDAO = GetComponent<SkillDAO>();
        Damage = skillDAO.GetSkillbyID(1).Damage;
        StartCoroutine(SetSpeed());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            collision.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }
    IEnumerator SetSpeed()
    {
        Source.clip = RasenShurikenSound;
        Source.Play();
        yield return new WaitForSecondsRealtime(1f);
        rb.velocity = transform.right * 30;
        yield return new WaitForSecondsRealtime(5f);
        Destroy(gameObject);
    }
}
