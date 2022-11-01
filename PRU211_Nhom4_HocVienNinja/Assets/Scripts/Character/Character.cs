using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Common Abilities SQL
    public int CharacterID;
    public string CharacterName;
    public int TotalHealthPoint;
    public int TotalChakra;
    public int CharacterSpeed;
    public int CharacterDamage;
    public string CharacterImage;
    public string Description;
    public int AbilitiesID;

    // Setting Logic Abilities
    public float CooldownFirstSkill;

    public int CostFirstSkill;

    public float CooldownSecondSkill;

    public int CostSecondSkill;

    public float CooldownThirdSkill;

    public int CostThirdSkill;

    // UI Health and Chakra

    public HealthBar HealthBar;

    public ChakraBar ChakraBar;

    // Setting Component
    public Rigidbody2D Rigid;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public Collider2D Col;
    public TrailRenderer TrailRenderer;
    public StaticController staticController;
    public AudioSource SkillSource;
    public CharacterDAO characterDAO;

    public AudioSource AttackSource;
    public AudioClip NormalAttackClip;
    public AudioClip ThirdAttackClip;


    public AudioSource UidaSource;

    public AudioSource EffectSource;
    public AudioClip[] EffectClip;
    public GameObject[] Effect;
    // Setting Normal Attack
    public Transform AttackPoint;
    public LayerMask LayerToAttack;

    // Dashing
    private bool CanDash = true;
    public bool IsDashing;
    private float DashingPower = 24f;
    private float DashingTime = 0.2f;
    private float DashingCooldown = 1;

    // Setting Player ( Attack Combo, Move, Interact with Enviroment)
    private bool IsGrounded;
    public static float Yinput, Xinput;
    private int CanJump = 0;
    public bool FacingRight;
    public int Combo;
    public bool CanCombo;
    public bool IsSkilling;
    public bool IsHurt;
    public float AttackRange;
    public int CurrentHealthPoint;
    public int CurrentChakra;
    public bool IsWalking;
    public bool CanTurn;
    public float ReloadFirstSkill;
    public float ReloadSecondSkill;
    public float ReloadThirdSkill;
    public int Jumppower;
    public GameObject blurCamera;
    public bool isOnFloor;
    public bool IsStart;
    public bool IsReachPortal;
    // Start is called before the first frame update
    public void Start()
    {
        SetupComponent();
        CharacterEntity character = characterDAO.GetCharacterbyID(2);
        InvokeRepeating(nameof(RegenChakra), 1f, 2f);
        CharacterName = character.CharacterName;
        TotalHealthPoint = character.TotalHealthPoint;
        CurrentHealthPoint = TotalHealthPoint;
        TotalChakra = character.TotalChakra;
        CurrentChakra = TotalChakra;
        AttackRange = 3f;
        CharacterDamage = character.CharacterDamage;
        CharacterSpeed = character.CharacterSpeed;
        FacingRight = true;
        CanTurn = true;
    }

    //Update is called once per frame
    public void Update()
    {
        if (!IsStart)
        {
            return;
        }
        if (IsDashing)
        {
            return;
        }
        if (!IsSkilling)
        {
            Xinput = Input.GetAxis(staticController.Horizontal);
            Yinput = Input.GetAxis(staticController.Vertical);
        }
        IsWalking = Mathf.Abs(Xinput) > 0;
        Walk();
        Jump();
        Dashing();
        NormalAttack();
        UseFirstItem();
        UseSecondItem();
    }
    public void SetHealthBar()
    {
        HealthBar.SetHealth(CurrentHealthPoint, TotalHealthPoint);
    }
    public void SetChakrahBar()
    {
        ChakraBar.SetChakra(CurrentChakra, TotalChakra);
    }

    void RegenChakra()
    {
        if (CurrentChakra >= TotalChakra)
        {
            CurrentChakra = TotalChakra;
        }
        else
        {
            CurrentChakra += 1;
        }

    }

    // Set up Character Abilities
    public void SetupComponent()
    {
        Animator = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Col = GetComponent<Collider2D>();
        TrailRenderer = GetComponent<TrailRenderer>();
        staticController = GetComponent<StaticController>();
        characterDAO = GetComponent<CharacterDAO>();
    }
    public void StartSkill()
    {
        Time.timeScale = 0f;
        blurCamera.SetActive(true);
        CharacterSpeed = 0;
        CanTurn = false;
        IsSkilling = true;
    }
    public void EndSkill()
    {
        Time.timeScale = 1f;
        blurCamera.SetActive(false);
        CharacterSpeed = 10;
        CanTurn = true;
        IsSkilling = false;

    }
    // execute Normal Attack
    public void NormalAttack()
    {
        if (Input.GetKeyDown(staticController.NormalAttackKey) && !CanCombo && IsGrounded && !IsSkilling)
        {

            CanCombo = true;
            CharacterSpeed = 0;
            CanTurn = false;
            Animator.SetTrigger("Attack" + Combo);
        }
    }
    public void PlaySoundAttack()
    {
        AttackSource.clip = NormalAttackClip;
        AttackSource.Play();
    }
    public void PlaySoundThirdAttack()
    {
        AttackSource.clip = ThirdAttackClip;
        AttackSource.Play();
    }
    // give damage for normalAttack
    public void DamageNormalAttack()
    {
        Collider2D[] HitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, LayerToAttack);

        if (HitEnemy != null)
        {
            foreach (Collider2D Enemy in HitEnemy)
            {
                Enemy.GetComponent<Enemy>().TakeDamage(CharacterDamage);
                if (Enemy.gameObject.CompareTag("Enemy"))
                {
                    Enemy.GetComponent<Animator>().SetTrigger("Hurt");
                }
            }
        }

    }

    // Abilites
    public virtual void Abilities()
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

    //// player movement
    public void Walk()
    {
        Rigid.velocity = new Vector2(Xinput * CharacterSpeed, Rigid.velocity.y);
        if (Xinput < -0.01 && FacingRight && CanTurn)
        {
            Flip();
        }
        else if (Xinput > 0.01 && !FacingRight && CanTurn)
        {
            Flip();
        }
        Animator.SetBool("Run", IsWalking);
    }

    // player Dashing
    public void Dashing()
    {
        if (Input.GetKeyDown(staticController.DashKey) && CanDash)
        {
            StartCoroutine(Dash());
        }
    }

    public void UseFirstItem()
    {
        if (Input.GetKeyDown(KeyCode.Q) && AccountManager.ItemOneQuantity > 0)
        {
            EffectSource.clip = EffectClip[0];
            EffectSource.Play();
            GameObject effect = Instantiate(Effect[0], transform.position, Quaternion.identity);
            Destroy(effect, 3f);
            CurrentHealthPoint += 20;
            if (CurrentHealthPoint > TotalHealthPoint)
            {
                CurrentHealthPoint = TotalHealthPoint;
            }
            AccountManager.ItemOneQuantity--;
        }
    }
    public void UseSecondItem()
    {
        if (Input.GetKeyDown(KeyCode.E) && AccountManager.ItemTwoQuantity > 0)
        {
            EffectSource.clip = EffectClip[1];
            EffectSource.Play();
            GameObject effect = Instantiate(Effect[1], transform.position, Quaternion.identity);
            Destroy(effect, 3f);
            CurrentChakra += 20;
            if (CurrentChakra > TotalChakra)
            {
                CurrentChakra = TotalChakra;
            }
            AccountManager.ItemTwoQuantity--;

        }
    }
    //// player Jump
    public void Jump()
    {
        if (Input.GetKeyDown(staticController.JumpKey) && CanJump > 0 && !IsSkilling)
        {
            Rigid.velocity = new Vector2(Rigid.velocity.x, Jumppower);
            Animator.SetTrigger("Jump");
            IsGrounded = false;
            CanJump--;
            Combo = 0;
            CanCombo = false;
            CanTurn = true;
            CharacterSpeed = 10;

        }
        Animator.SetBool("isGround", IsGrounded);
    }
    //player Die
    public bool Die()
    {
        if (CurrentHealthPoint <= 0)
        {
            return true;
        }
        return false;
    }

    //// turn Player left or right
    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }


    // get Hit by Enemy then decrease health point
    public void TakeDamage(int Damage)
    {
        if (this.gameObject.CompareTag("Player"))
        {
            if (IsHurt)
            {
                return;
            }
            else
            {
                CurrentHealthPoint -= Damage;
                UidaSource.Play();
                StartCoroutine(DamageAnimation());
            }
        }
        else
        {
            CurrentHealthPoint -= Damage;
            StartCoroutine(DamageAnimationforSummon());
        }


    }

    // When Play was hit by Enemy, play red animation
    public IEnumerator DamageAnimation()
    {
        IsHurt = true;
        for (int i = 0; i < 10; i++)
        {
            SpriteRenderer.color = Color.red;

            yield return new WaitForSeconds(.1f);

            SpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
        IsHurt = false;
    }
    public IEnumerator DamageAnimationforSummon()
    {
        SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.2f);
        SpriteRenderer.color = Color.white;

    }
    // find closest Enemy
    public Enemy FindClostestEnemy(int Range)
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy && Vector2.Distance(currentEnemy.transform.position, transform.position) <= Range)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        return closestEnemy;
    }

    public void Startcombo()
    {
        CanCombo = false;
        if (Combo < 3)
        {
            Combo++;
        }
    }

    public void Finishcombo()
    {
        CanCombo = false;
        CharacterSpeed = 10;
        Combo = 0;
        CanTurn = true;
    }

    private IEnumerator Dash()
    {
        CanDash = false;
        IsDashing = true;
        float origialGravity = Rigid.gravityScale;
        Rigid.gravityScale = 0f;
        Rigid.velocity = new Vector2(transform.right.x * 2 * DashingPower, 0f);
        yield return new WaitForSeconds(DashingTime);
        Rigid.gravityScale = origialGravity;
        IsDashing = false;
        yield return new WaitForSeconds(DashingCooldown);
        CanDash = true;
    }

    //// Set up just Jump when on Ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Floor"))
        {
            IsGrounded = true;
            CanJump = 2;
            isOnFloor = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            IsReachPortal = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
