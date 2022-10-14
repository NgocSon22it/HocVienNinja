using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadline : MonoBehaviour
{
    public GameObject Explosion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Summon"))
        {
            collision.GetComponent<Character>().TakeDamage(10);
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }
}
