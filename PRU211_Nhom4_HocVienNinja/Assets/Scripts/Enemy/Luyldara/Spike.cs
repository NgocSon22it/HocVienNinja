using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    private EdgeCollider2D edgeCollider2D;
    public AudioSource BatViSource;
    private void Start()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        edgeCollider2D.enabled = false;
    }
    public void PlaySoundBatVi()
    {
        BatViSource.Play();
    }
    public void TurnOnCollider()
    {
        edgeCollider2D.enabled = true;
    }
    public void TurnOffCollider()
    {
        edgeCollider2D.enabled = false;
    }
    public void  DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Summon"))
        {
            collision.GetComponent<Character>().TakeDamage(10);
        }
    }
}
