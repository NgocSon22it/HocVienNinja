using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katon : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 30;
        StartCoroutine(DestoyKaton());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            collision.GetComponent<Enemy>().TakeDamage(100);
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator DestoyKaton()
    {
        yield return new WaitForSecondsRealtime(5f);
        Destroy(gameObject);
    }
}
