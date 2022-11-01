using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iphone14 : MonoBehaviour
{
    public GameObject Explode;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Summon"))
        {
            GameObject instance = Instantiate(Explode, transform.position, Quaternion.identity);
            Destroy(instance, 2f);
            collision.GetComponent<Character>().TakeDamage(10);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            GameObject instance = Instantiate(Explode, transform.position, Quaternion.identity);
            Destroy(instance, 2f);
            Destroy(gameObject);
        }
    }
}
