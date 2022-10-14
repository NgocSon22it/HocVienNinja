using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public float Distance;
    public float Timer; //Timer for cooldown between attacks

    private Character Player;
    private float IntTimer;
    private bool AttackMode;
    public int RangeFoundPlayer;
    new void Start()
    {
        EnemyName = "CocRe";
        EnemyAttackRange = 4;
        TotalHealthPoint = 500;
        CurrentHealthPoint = 500;
        EnemyDamage = 10;
        EnemySpeed = 5;
        Timer = 2f;
        base.Start();
    }

    new void Update()
    {
        Player = FindClostestPlayer(RangeFoundPlayer);
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
        if(CurrentHealthPoint < TotalHealthPoint)
        {
            RangeFoundPlayer = 200;
        }
    }

    public override void Walk()
    {
        Animator.SetBool("Walk", true);
        handleRotation(Player.transform);
        Rigid.mass = 1;
        Vector2 targetPosition = new(Player.transform.position.x, transform.position.y);

        Vector2 newPosition = Vector2.MoveTowards(Rigid.position, targetPosition, EnemySpeed * Time.fixedDeltaTime);

        Rigid.MovePosition(newPosition);

        

        
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
        else
        {
            Animator.SetBool("Walk", false);

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(AttackPoint.position, Range);
    }

}
