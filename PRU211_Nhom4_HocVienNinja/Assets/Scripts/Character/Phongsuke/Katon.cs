using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katon : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 30;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamagebyFar(100);
            Destroy(gameObject);
        }
    }
}
