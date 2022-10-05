using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadline : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Character>().TakeDamageforPlayer(10);
        }
        if (collision.gameObject.CompareTag("Summon"))
        {
            collision.GetComponent<Character>().TakeDamageforSummon(100);
        }
    }
}
