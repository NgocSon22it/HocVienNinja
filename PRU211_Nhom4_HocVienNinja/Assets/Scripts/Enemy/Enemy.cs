using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName { get; set; }
    public int totalHealthPoint { get; set; }
    public int currentHealthPoint { get; set; }
    public int enemySpeed { get; set; }
    public int enemyDamage { get; set; }
    public int enemyAttackRange { get; set; }

    // Setting Component
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Collider2D col;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        SetupEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Set up Character Abilities
    void SetupEnemy()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

    }

    // Abilites
    public virtual void Abilities()
    {

    }

    // First Skill
    public virtual void firstSkill()
    {

    }

    // Second Skill
    public virtual void secondSkill()
    {

    }

    // Third Skill
    public virtual void thirdSkill()
    {

    }

    // enemy movement
    public virtual void Walk()
    {

    }
    // enemy Jump
    public virtual void Jump()
    {

    }

    // enemy Die
    public void Die()
    {

    }

    // turn enemy left or right
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    // get Hit by Player then decrease health point
    public void TakeDamage(int Damage)
    {

        currentHealthPoint -= Damage;
        StartCoroutine(DamageAnimation());
    }

    // play red when get hit by Player
    IEnumerator DamageAnimation()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sp.color = Color.white;
    }

    // find closest Player to hit
    public Player findClostestPlayer()
    {
        float distanceToClosestPlayer = Mathf.Infinity;
        Player closestPlayer = null;
        Player[] allPlayer = GameObject.FindObjectsOfType<Player>();

        foreach (Player currentPlayer in allPlayer)
        {
            float distanceToEnemy = (currentPlayer.transform.position - this.transform.position).sqrMagnitude;
            string checkTag = currentPlayer.gameObject.tag;
            if (distanceToEnemy < distanceToClosestPlayer)
            {
                distanceToClosestPlayer = distanceToEnemy;
                closestPlayer = currentPlayer;
            }
        }

        return closestPlayer;
    }
}
