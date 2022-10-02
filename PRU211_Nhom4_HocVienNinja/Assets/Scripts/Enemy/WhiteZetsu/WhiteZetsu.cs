using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteZetsu : Enemy
{
    public float Distance;
    public float Timer; //Timer for cooldown between attacks

    private Player Player;
    private float IntTimer;
    private bool AttackMode;

    new void Start()
    {
        EnemyName = "Zetsu";
        EnemyAttackRange = 3;
        TotalHealthPoint = 500;
        CurrentHealthPoint = 500;
        EnemySpeed = 5;
        Timer = 2f;
        base.Start();
    }

    new void Update()
    {
        Player = FindClostestPlayer(15);
        if(Player != null && !AttackMode && IntTimer <= 0)
        {
            Walk();
        }

        if(IntTimer >= 0)
        {
            IntTimer -= Time.deltaTime;
            Animator.SetBool("Attack", false);
        }
        NormalAttack();
        base.Update();
    }

    public override void Walk()
    {
        Animator.SetBool("Walk", true);

        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            Vector2 targetPosition = new(Player.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.deltaTime);
        }
        if(transform.position.x > Player.transform.position.x && FacingRight)
        {
            Flip();
        }
        else if (transform.position.x < Player.transform.position.x && !FacingRight)
        {
            Flip();
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
                Animator.SetBool("Attack", true);
                Animator.SetBool("Walk", false);
                AttackMode = true;
                IntTimer = Timer;
            }
        }
        else
        {
            Animator.SetBool("Walk", false);

        }

    }
}
