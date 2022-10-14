using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luyldara : Enemy
{
    [Header("First Skill")]
    public Transform FirstSkillPoint;
    public GameObject DeadlineBullet;

    [Header("Second Skill")]
    public Transform SecondSkillPoint;
    public GameObject IphoneBullet;

    [Header("Third Skill")]
    public GameObject Spike;
    public Transform[] PlaceForSpike;

    [Header("Fouth Skill")]
    public LineRenderer[] lineRenderer;
    public Transform[] OddPlaceLazerFire;
    public Transform[] EvenPlaceLazerFire;
    public GameObject StartLazer;
    public bool StartLazerPlaceEven;
    public bool StartLazerPlaceOdd;
    [Header("Movement")]
    public Transform[] Place;
    public GameObject[] SixPathsObject;
    private void Awake()
    {
        /*for (int i = 0; i < 6; i ++)
        {
            Paths[i].GetComponent<Rigidbody2D>().isKinematic = true;
        }*/
        Rigid = GetComponent<Rigidbody2D>();
        TotalHealthPoint = 2000;
        CurrentHealthPoint = 2000;
        TurnOffLazer();
    }
    // Update is called once per frame
    new void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Move());
        }
    }
    public void TurnOffLazer()
    {
        foreach (LineRenderer line in lineRenderer)
        {
            line.enabled = false;
        }
    }
    IEnumerator ExecuteThirdSkill()
    {
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
        yield return new WaitForSeconds(1f);
        Character player = FindClostestPlayer(100);
        handleRotation(player.transform);
        for (int i = 0; i < 3; i++)
        {
            Vector2 DirectionToPlayer = player.transform.GetChild(0).position - transform.GetChild(0).position;
            DirectionToPlayer.Normalize();
            GameObject BulletIns = Instantiate(IphoneBullet, SecondSkillPoint.position, SecondSkillPoint.rotation);
            BulletIns.GetComponent<Rigidbody2D>().AddForce(DirectionToPlayer * 1000);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(Move());
    }
    IEnumerator ExecuteFirstSkill()
    {
        yield return new WaitForSeconds(1f);
        for (int i = -1000; i <= 1000; i += 500)
        {
            GameObject BulletIns = Instantiate(DeadlineBullet, FirstSkillPoint.position, Quaternion.identity);
            BulletIns.GetComponent<Rigidbody2D>().AddForce(Vector2.right * i);
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        int a = Random.Range(0, 4);
        Animator.SetTrigger("Dissappear");
        foreach (GameObject path in SixPathsObject)
        {
            path.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        transform.position = Place[a].position;
        Animator.SetTrigger("Appear");
        foreach (GameObject path in SixPathsObject)
        {
            path.gameObject.SetActive(true);
        }


        if (a == 3)
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
                Character enemy = rayinfo.transform.GetComponent<Character>();
                if (enemy != null)
                {
                    enemy.TakeDamage(10);
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
        yield return new WaitForSeconds(.5f);
        GameObject MainLazer = Instantiate(StartLazer, transform.GetChild(0).position, Quaternion.identity);
        StartCoroutine(LogicFouthSkill(OddPlaceLazerFire));
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(LogicFouthSkill(EvenPlaceLazerFire));
        yield return new WaitForSeconds(2.5f);
        Destroy(MainLazer);
        StartCoroutine(Move());
    }
}
