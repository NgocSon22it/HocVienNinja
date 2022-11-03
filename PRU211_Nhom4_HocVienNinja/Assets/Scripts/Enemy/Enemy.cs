using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string EnemyName { get; set; }
    public int TotalHealthPoint { get; set; }
    public int CurrentHealthPoint { get; set; }
    public int EnemySpeed { get; set; }
    public int EnemyDamage { get; set; }
    public int EnemyAttackRange { get; set; }


    public HealthBar HealthBar;
    public string EnemyImage;

    // Setting Component
    public Rigidbody2D Rigid;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public Collider2D Col;
    public AudioSource Source;
    public bool FacingRight = true;

    // Setting Normal Attack
    public Transform AttackPoint;
    public LayerMask LayerToAttack;
    public float Range;
    public bool isOnFloor;
    public bool isNearFloor;
    public bool isGround;


    // Information
    public int Score;
    public int Coin;
    
    // Start is called before the first frame update
    public void Start()
    {
        SetupComponent();

    }

    // Update is called once per frame
    public void Update()
    {
        if (this.gameObject.CompareTag("Enemy"))
        {
            SetHealthBar();
        }
    }
    // Set up Character Abilities
    void SetupComponent()
    {
        Animator = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Col = GetComponent<Collider2D>();
        Source = GetComponent<AudioSource>();          
    }

    // Abilites
    public virtual void Abilities()
    {

    }

    // give damage for normalAttack
    public void DamageNormalAttack()
    {
        Collider2D[] HitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, EnemyAttackRange, LayerToAttack);

        if (HitPlayer != null)
        {
            foreach (Collider2D Player in HitPlayer)
            {
             Player.GetComponent<Character>().TakeDamage(EnemyDamage);
            }
        }

    }


    public void PlaySoundAttack()
    {
        Source.Play();
    }
    // normal Attack
    public virtual void NormalAttack()
    {

    }

    // First Skill
    public virtual void FirstSkill()
    {

    }

    // Second Skill
    public virtual void SecondSkill()
    {

    }

    // Third Skill
    public virtual void ThirdSkill()
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
        GameManager.Score += Score;
        GameManager.Coin += Coin;
        Destroy(gameObject);
    }
    // turn enemy left or right
    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }
    public void handleRotation(Transform PlayerPositon)
    {
        if (transform.GetChild(0).position.x > PlayerPositon.GetChild(0).position.x && FacingRight)
        {
            Flip();
        }
        else if (transform.GetChild(0).position.x < PlayerPositon.GetChild(0).position.x && !FacingRight)
        {
            Flip();
        }
    }
    public void SetHealthBar()
    {
        HealthBar.SetHealth(CurrentHealthPoint, TotalHealthPoint);
    }
    // get Hit by Player then decrease health point
    public void TakeDamage(int Damage)
    {

        CurrentHealthPoint -= Damage;
        if (this.gameObject.CompareTag("Enemy"))
        {                       
            if (CurrentHealthPoint <= 0)
            {
                Die();
            }
        }
        else
        {
            if (CurrentHealthPoint <= 0)
            {
                Animator.SetTrigger("RunOut");
            }
        }       
        StartCoroutine(DamageAnimation());
    }


    // play red when get hit by Player
    IEnumerator DamageAnimation()
    {
        
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color = Color.red;
        yield return new WaitForSeconds(.2f);
        sp.color = Color.white;
    }

    // find closest Player to hit
    public Character FindClostestPlayer(int Range)
    {
        float distanceToClosestPlayer = Mathf.Infinity;
        Character closestPlayer = null;
        Character[] allPlayer = GameObject.FindObjectsOfType<Character>();

        foreach (Character currentPlayer in allPlayer)
        {
            float distanceToEnemy = (currentPlayer.transform.GetChild(0).position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestPlayer && Vector2.Distance(currentPlayer.transform.position, transform.position) <= Range)
            {
                distanceToClosestPlayer = distanceToEnemy;
                closestPlayer = currentPlayer;
            }
        }

        return closestPlayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Floor")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Floor")
        {
            isGround = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isNearFloor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isNearFloor = false;
        }
    }
}
