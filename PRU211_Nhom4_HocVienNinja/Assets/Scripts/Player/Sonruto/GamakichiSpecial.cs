using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamakichiSpecial : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamagebyFar(30);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
