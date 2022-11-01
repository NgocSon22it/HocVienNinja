using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public float Distance;
    public float Timer; //Timer for cooldown between attacks

    private Character Player;
    private bool AttackMode;
    public int RangeFoundPlayer;
    public int JumpPower;
    public float TimeMoveIdle = 3f;
    private Quaternion Rotation;
    public GameObject FireBullet;
    new void Start()
    {
        base.Start();
        EnemyEntity enemyEntity = enemyDAO.GetEnemybyID(3);
        EnemyName = enemyEntity.EnemyName;
        TotalHealthPoint = enemyEntity.TotalHealthPoint;
        CurrentHealthPoint = TotalHealthPoint;
        EnemyDamage = enemyEntity.EnemyDamage;
        EnemySpeed = enemyEntity.EnemySpeed;
        Coin = enemyEntity.EnemyCoin;
        Timer = 2f;
        Score = 500;
        RangeFoundPlayer = 30;
        JumpPower = 20;
        EnemyAttackRange = 30;
    }

    new void Update()
    {
        Player = FindClostestPlayer(RangeFoundPlayer);
        if (Player == null)
        {
            Animator.SetBool("Walk", true);
            Rigid.velocity = transform.right * EnemySpeed;
            TimeMoveIdle -= Time.deltaTime;
            if (TimeMoveIdle <= 0)
            {
                TimeMoveIdle = 3f;
                Flip();
            }
        }
        if (Player != null && !AttackMode)
        {
            RangeFoundPlayer = 300;
            Walk();
        }
        NormalAttack();
        base.Update();
        if (CurrentHealthPoint < TotalHealthPoint)
        {
            RangeFoundPlayer = 300;
        }
    }

    public override void Walk()
    {
        Animator.SetBool("Walk", true);
        
        handleRotation(Player.transform);
        Vector2 targetPosition = new(Player.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.deltaTime);

        Jump();
    }

    public override void Jump()
    {
        if (isNearFloor && isGround)
        {
            Rigid.velocity = new Vector2(.2f, 1.3f) * JumpPower;
        }

    }
    public override void NormalAttack()
    {
        if (Player != null)
        {
            Distance = Vector2.Distance(transform.position, Player.transform.position);

            if (Distance > EnemyAttackRange)
            {
                Animator.SetBool("Attack", false);
                AttackMode = false;
            }
            else if (EnemyAttackRange >= Distance)
            {
                handleRotation(Player.transform);
                Animator.SetBool("Attack", true);
                Animator.SetBool("Walk", false);
                AttackMode = true;
            }
        }

    }
    public void Fire()
    {

        if (Player != null)
        {
            Vector2 direction = (Vector2)Player.transform.GetChild(0).position - (Vector2)transform.position;
            direction.Normalize();
            GameObject BulletIns = Instantiate(FireBullet, AttackPoint.position, AttackPoint.rotation);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Rotation.eulerAngles = new Vector3(0, 0, angle);
            BulletIns.transform.rotation = Rotation;
            BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * 1500);
            Destroy(BulletIns, 3f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
