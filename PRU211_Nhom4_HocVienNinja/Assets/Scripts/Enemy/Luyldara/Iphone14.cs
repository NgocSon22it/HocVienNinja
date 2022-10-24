using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iphone14 : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Summon"))
        {
            collision.GetComponent<Character>().TakeDamage(10);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
