using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Common Abilities
    public string characterName { get; set; }
    public int totalHealthPoint { get; set; }
    public int currentHealthPoint { get; set; }
    public int totalChakra { get; set; }
    public int currentChakra { get; set; }
    public int characterSpeed { get; set; }
    public int characterDamage { get; set; }
    public int characterAttackRange { get; set; }
    public bool isHurt { get; set; }


    // Setting Component
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Collider2D col;
    public TrailRenderer trailRenderer;

    // Setting Normal Attack
    public Transform attackPoint;
    public float attackRange;
    public LayerMask layerToAttack;

    // Dashing
    public bool canDash = true;
    public bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    // Setting Player ( Attack Combo, Move, Interact with Enviroment)
    private bool isGrounded;
    public static float Yinput, Xinput;
    private int canJump = 0;
    private bool facingRight = true;
    public int combo;
    public bool canCombo;

    public bool locomotion;
    public bool isWalking;
    public bool canTurn;


    // Start is called before the first frame update
    void Start()
    {
        SetupPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (locomotion)
        {
            Xinput = Input.GetAxis("Horizontal");
            Yinput = Input.GetAxis("Vertical");
        }
    }

    // Set up Character Abilities
    void SetupPlayer()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        characterName = "";
        totalHealthPoint = 100;
        currentHealthPoint = totalHealthPoint;
        totalChakra = 100;
        currentChakra = totalChakra;
        characterAttackRange = 5;
        characterDamage = 10;
        characterSpeed = 10;
    }

    // Normal Attack
    public void normalAttack()
    {

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

    // player movement
    public void Walk()
    {

    }

    // player Dashing
    public void Dashing()
    {

    }

    // player Jump
    public void Jump()
    {

    }

    //player Die
    public void Die()
    {

    }

    // turn Player left or right
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    // get Hit by Enemy then decrease health point
    public void TakeDamage(int Damage)
    {
        if (isHurt)
        {
            return;
        }
        else
        {
            currentHealthPoint -= Damage;
            StartCoroutine(DamageAnimation());
          //Debug.Log(currentHealthPoint);
        }

    }
    // When Play was hit by Enemy, play red animation
    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        isHurt = true;
        for (int i = 0; i < 10; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                sr.color = Color.red;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                sr.color = Color.white;
            }

            yield return new WaitForSeconds(.1f);
        }
        isHurt = false;
    }

    // find closest Enemy
    public Enemy findClostestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            string checkTag = currentEnemy.gameObject.tag;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }

        return closestEnemy;
    }
    // Set up just Jump when on Ground
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            canJump = 2;
        }

    }
}
