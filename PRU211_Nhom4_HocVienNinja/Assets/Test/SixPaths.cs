using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixPaths : MonoBehaviour
{
    public float Angle;
    public bool IsTouchGround;
    public bool CanCauseDamage;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = false;
    }
    public void SetUpStartLine(Vector2 startLine)
    {
        lineRenderer.SetPosition(0, startLine);
    }
    public void SetUpEndLine(Vector2 endLine)
    {
        lineRenderer.SetPosition(1, endLine);
    }
    public void TurnOnLine()
    {
        lineRenderer.enabled = true;
    }
    public void TurnOffLine()
    {
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Home"))
        {
            CanCauseDamage = true;
        }
        if (collision.gameObject.CompareTag("Player") && CanCauseDamage)
        {
            collision.GetComponent<Player>().TakeDamageforPlayer(10);
        }
        if (collision.gameObject.CompareTag("Summon") && CanCauseDamage)
        {
            collision.GetComponent<Player>().TakeDamageforSummon(100);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsTouchGround = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            CanCauseDamage = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsTouchGround = false;
        }
    }
}
