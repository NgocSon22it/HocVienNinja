using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iphone14 : MonoBehaviour
{
    public GameObject Explode;
    public Sprite[] Sprite;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int a = Random.Range(0, 5);
        spriteRenderer.sprite = Sprite[a];
    }
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
