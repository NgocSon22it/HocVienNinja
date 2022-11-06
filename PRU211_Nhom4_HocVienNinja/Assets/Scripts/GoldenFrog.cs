using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenFrog : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Score += 17500;
            GameManager.Coin += 17500;
            Destroy(gameObject);
        }
    }

}
