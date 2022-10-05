using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamakichi : Character
{
    public Transform FirePoint;
    public GameObject Bullet;
    public GameObject SpecialBullet;

    private Quaternion Rotation;

    public AudioClip ClipNormal;
    public AudioClip ClipSpecial;

    Enemy ClostestEnemy;
    private void Awake()
    {
        CurrentHealthPoint = 300;
        TotalHealthPoint = 300;
        TotalChakra = 100;
        CurrentChakra = 0;
        SetHealthBar();
        SetChakrahBar();
        CharacterName = "Gamakichi";
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    new void Update()
    {
            ClostestEnemy = FindClostestEnemy(12);
            if (ClostestEnemy != null)
            {
                if (transform.position.x > ClostestEnemy.transform.position.x && FacingRight)
                {
                    Flip();
                }
                else if (transform.position.x < ClostestEnemy.transform.position.x && !FacingRight)
                {
                    Flip();

                }
                Animator.SetBool("FoundEnemy", true);

            }
            else
            {
            Animator.SetBool("FoundEnemy", false);

            }
            if(CurrentChakra >= 120)
        {
            Animator.SetTrigger("Special");
            CurrentChakra = 0;
        }
            if(CurrentHealthPoint <= 0)
        {
            Destroy(gameObject);
        }
        SetChakrahBar();
    }

    public void PlaySoundNormalAttack()
    {
        Source.clip = ClipNormal;
        Source.Play();
    }

    public void PlaySoundSpecialAttack()
    {
        Source.clip = ClipSpecial;
        Source.Play();
    }

    public void ShootBubble()
    {
        ClostestEnemy = FindClostestEnemy(12);

        if (ClostestEnemy != null)
        {
            Vector2 direction = (Vector2)ClostestEnemy.transform.GetChild(0).position - (Vector2)transform.position;
            direction.Normalize();
            GameObject BulletIns = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Rotation.eulerAngles = new Vector3(0, 0, angle);
            BulletIns.transform.rotation = Rotation;
            BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * 2000);
            CurrentChakra += 20;
        }
    }

    public void ShootSpecialBubble()
    {
        ClostestEnemy = FindClostestEnemy(12);
        if (ClostestEnemy != null)
        {
            Vector2 direction = (Vector2)ClostestEnemy.transform.GetChild(0).position - (Vector2)transform.position;
            direction.Normalize();
            GameObject BulletIns = Instantiate(SpecialBullet, FirePoint.position, FirePoint.rotation);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Rotation.eulerAngles = new Vector3(0, 0, angle);
            BulletIns.transform.rotation = Rotation;
            BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * 1300);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
