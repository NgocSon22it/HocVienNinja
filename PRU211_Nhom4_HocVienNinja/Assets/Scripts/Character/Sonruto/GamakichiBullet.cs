using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamakichiBullet : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
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
