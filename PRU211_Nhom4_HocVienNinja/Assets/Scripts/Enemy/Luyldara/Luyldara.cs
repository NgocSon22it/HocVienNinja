using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luyldara : Enemy
{

    [Header("Ability")]
    public SixPaths[] SixPaths;
    public Transform rotationCenter;
    float posX, posY;
    public float rotationRadius = 5f;
    public float angularSpeed = 2f;
    public bool isStart;


    [Header("First Skill")]
    public Transform FirstSkillPoint;
    public GameObject DeadlineBullet;
    public AudioClip FirstSkillSound;

    [Header("Second Skill")]
    public Transform SecondSkillPoint;
    public GameObject IphoneBullet;
    public AudioClip SecondSkillSound;

    [Header("Third Skill")]
    public GameObject Spike;
    public Transform[] PlaceForSpike;
    public AudioClip ThirdSkillSound;

    [Header("Fouth Skill")]
    public LineRenderer[] lineRenderer;
    public Transform[] OddPlaceLazerFire;
    public Transform[] EvenPlaceLazerFire;
    public GameObject StartLazer;
    public bool StartLazerPlaceEven;
    public bool StartLazerPlaceOdd;
    public AudioClip FourthSkillSound;

    [Header("Movement")]
    public Transform[] Place;
    public GameObject[] SixPathsObject;
    public bool IsOut;


    private void Awake()
    {
        TotalHealthPoint = 1;
        CurrentHealthPoint = 1;
        TurnOffLazer();
    }

    new void Start()
    {
        Coin = 10000;
        Score = 10000;
        base.Start();
    }
    // Update is called once per frame
    new void Update()
    {
        SixPathsMoveCircle();
    }
    public void TurnOffLazer()
    {
        foreach (LineRenderer line in lineRenderer)
        {
            line.enabled = false;
        }
    }
    public void TurnOnCollider()
    {
        Col.enabled = true;
    }
    public void TurnOffCollider()
    {
        Col.enabled = false;
    }
    public void SixPathsMoveCircle()
    {
        if (isStart)
        {
            foreach (SixPaths path in SixPaths)
            {
                posX = rotationCenter.position.x + Mathf.Cos(path.angle) * rotationRadius;
                posY = rotationCenter.position.y + Mathf.Sin(path.angle) * rotationRadius;
                path.transform.position = new Vector2(posX, posY);

                path.angle = path.angle + Time.deltaTime * angularSpeed;
                if (path.angle >= 360f)
                {
                    path.angle = path.DefaultAngle;
                }
            }
        }
    }
    IEnumerator ExecuteThirdSkill()
    {
        Source.clip = ThirdSkillSound;
        Source.Play();
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < PlaceForSpike.Length; i++)
        {
            Instantiate(Spike, PlaceForSpike[i].position, PlaceForSpike[i].rotation);
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(Move());


    }
    IEnumerator ExecuteSecondSkill()
    {
        Source.clip = SecondSkillSound;
        Source.Play();
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 3; i++)
        {
            Character player = FindClostestPlayer(100);
            Vector2 DirectionToPlayer = player.transform.GetChild(0).position - transform.GetChild(0).position;
            DirectionToPlayer.Normalize();
            GameObject BulletIns = Instantiate(IphoneBullet, SecondSkillPoint.position, SecondSkillPoint.rotation);
            BulletIns.GetComponent<Rigidbody2D>().AddForce(DirectionToPlayer * 1500);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(Move());
    }
    IEnumerator ExecuteFirstSkill()
    {
        Source.clip = FirstSkillSound;
        Source.Play();
        yield return new WaitForSeconds(1f);
        for (int i = -1000; i <= 1000; i += 500)
        {
            GameObject BulletIns = Instantiate(DeadlineBullet, FirstSkillPoint.position, Quaternion.identity);
            BulletIns.GetComponent<Rigidbody2D>().AddForce(Vector2.right * i);
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(Move());
    }
    public IEnumerator Move()
    {
        if (!IsOut)
        {
            int a = Random.Range(0, 5);
            Animator.SetTrigger("Dissappear");

            foreach (GameObject path in SixPathsObject)
            {
                path.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1f);
            if (a > 2)
            {
                transform.position = Place[3].position;
            }
            else
            {
                transform.position = Place[a].position;
            }
            Animator.SetTrigger("Appear");
            foreach (GameObject path in SixPathsObject)
            {
                path.gameObject.SetActive(true);
            }


            if (a > 2)
            {
                int b = Random.Range(0, 2);
                if (b == 0)
                {
                    StartCoroutine(ExecuteFirstSkill());
                }
                else
                {
                    StartCoroutine(ExecuteFouthSkill());

                }
            }
            else
            {
                int b = Random.Range(0, 2);
                if (b == 0)
                {
                    StartCoroutine(ExecuteSecondSkill());
                }
                else
                {
                    StartCoroutine(ExecuteThirdSkill());

                }
            }
        }
    }
    IEnumerator LogicFouthSkill(Transform[] Place)
    {

        for (int i = 0; i < 4; i++)
        {
            lineRenderer[i].enabled = true;
            lineRenderer[i].SetPosition(0, transform.GetChild(0).position);
            lineRenderer[i].SetPosition(1, Place[i].position);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 4; i++)
        {
            lineRenderer[i].enabled = false;
        }

        for (int i = 4; i < 8; i++)
        {
            lineRenderer[i].enabled = true;
            lineRenderer[i].SetPosition(0, transform.GetChild(0).position);
            Vector2 direction = Place[i - 4].position - transform.GetChild(0).position;
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.GetChild(0).position, direction.normalized, direction.magnitude);
            foreach (RaycastHit2D rayinfo in hit)
            {
                Character character = rayinfo.transform.GetComponent<Character>();
                if (character != null)
                {
                    character.TakeDamage(10);
                }

            }
            lineRenderer[i].SetPosition(1, Place[i - 4].transform.position);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 4; i < 8; i++)
        {
            lineRenderer[i].enabled = false;
        }

        yield return new WaitForSeconds(.5f);

    }
    IEnumerator ExecuteFouthSkill()
    {
        Source.clip = FourthSkillSound;
        Source.Play();
        yield return new WaitForSeconds(.5f);
        GameObject MainLazer = Instantiate(StartLazer, transform.GetChild(0).position, Quaternion.identity);
        Destroy(MainLazer, 5f);
        StartCoroutine(LogicFouthSkill(OddPlaceLazerFire));
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(LogicFouthSkill(EvenPlaceLazerFire));
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(Move());
    }
}
