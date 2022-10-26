using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamakichiBullet : MonoBehaviour
{
    public AudioSource HitSound;
    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            HitSound.Play();
            collision.GetComponent<Enemy>().TakeDamage(10);
            Destroy(gameObject);
        }
    }


    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
