using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belly : Enemy
{
    public float Distance;
    public float Timer; //Timer for cooldown between attacks

    private Character Player;
    private float IntTimer;
    private bool AttackMode;
    public int RangeFoundPlayer;
    public int JumpPower;
    public float TimeMoveIdle = 3f;

    new void Start()
    {
        EnemyName = "Belly";
        EnemyAttackRange = 4;
        TotalHealthPoint = 500;
        CurrentHealthPoint = 500;
        EnemyDamage = 10;
        EnemySpeed = 6;
        Timer = 2f;
        Coin = 30;
        Score = 50;
        base.Start();
    }

    new void Update()
    {
        Player = FindClostestPlayer(RangeFoundPlayer);
        if (Player == null && isGround)
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
        if (Player != null && !AttackMode && IntTimer <= 0)
        {
            Walk();
        }

        if (IntTimer >= 0)
        {
            IntTimer -= Time.deltaTime;
            Animator.SetBool("Attack", false);
        }
        NormalAttack();
        base.Update();
        if (CurrentHealthPoint < TotalHealthPoint)
        {
            RangeFoundPlayer = 200;
        }


    }

    public override void Walk()
    {
        Animator.SetBool("Walk", true);
        Rigid.mass = 1;
        handleRotation(Player.transform);
        Vector2 targetPosition = new(Player.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.deltaTime);

        Jump();
    }

    public override void Jump()
    {
        if (!isOnFloor && Player.isOnFloor && isNearFloor)
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
            else if (EnemyAttackRange >= Distance && IntTimer <= 0)
            {
                handleRotation(Player.transform);
                Rigid.mass = 1000;
                Animator.SetBool("Attack", true);
                Animator.SetBool("Walk", false);
                AttackMode = true;
                IntTimer = Timer;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(AttackPoint.position, Range);
    }

}