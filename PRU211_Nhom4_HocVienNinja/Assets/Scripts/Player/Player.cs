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
    public void Start()
    {
        locomotion = true;
        SetupPlayer();
    }

    // Update is called once per frame
    public void Update()
    {
        if (locomotion)
        {
            Xinput = Input.GetAxis("Horizontal");
            Yinput = Input.GetAxis("Vertical");
        }
        isWalking = Mathf.Abs(Xinput) > 0;
        Walk();
        Jump();

        normalAttack();
    }

    // Set up Character Abilities
    void SetupPlayer()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Normal Attack
    public void normalAttack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !canCombo && isGrounded)
        {
            canCombo = true;
            //characterSpeed = 0;
            //canTurn = false;
            animator.SetTrigger("Attack" + combo);
            Debug.Log("ok");
        }
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
        rigid.velocity = new Vector2(Xinput * characterSpeed, rigid.velocity.y);
        if (Xinput < -0.01 && facingRight)
        {
            Flip();
        }
        else if (Xinput > 0.01 && !facingRight)
        {
            Flip();
        }
        animator.SetBool("Run", isWalking);

    }

    // player Dashing
    public void Dashing()
    {

    }

    // player Jump
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 10);
            animator.SetTrigger("Jump");
            isGrounded = false;
            canJump--;
            combo = 0;
            canCombo = false;
        }
        animator.SetBool("isGround", isGrounded);
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

    public void Startcombo()
    {
        canCombo = false;
        if (combo < 3)
        {
            combo++;
        }
    }

    public void Finishcombo()
    {
        canCombo = false;
        //characterSpeed = 10;
        combo = 0;
        //canTurn = true;
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
