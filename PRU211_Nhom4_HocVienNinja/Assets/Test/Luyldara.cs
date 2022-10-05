using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luyldara : Enemy
{
    [Header("SixPaths Move Circle")]
    public Transform RotationCenter;
    float PosX, PosY;
    public float RotationRadius;
    public float AngularSpeed;

    [Header("First Skill")]
    public Transform[] SpawnPoint;
    public GameObject DeadlineBullet;
    private Quaternion Rotation;

    [Header("Second Skill")]
    public SixPaths[] Paths;
    public GameObject[] SixPathsHome;
    public GameObject[] SixPathsUp;
    public GameObject[] SixPathsDown;
    public GameObject[] ThreeFloor;
    public Transform PlaceToFly;
    public bool ActiveSixPathsMoveCircle;
    public bool ActiveSixPathMoveToHome;
    public bool ActiveSixPathFlyUp;
    public bool ActiveFlyToSky;

    Character player;
    private Rigidbody2D rb;

    private void Awake()
    {
        for (int i = 0; i < 6; i ++)
        {
            Paths[i].GetComponent<Rigidbody2D>().isKinematic = true;
        }
        rb = GetComponent<Rigidbody2D>();
        TotalHealthPoint = 10000;
        CurrentHealthPoint = 10000;
    }
    // Update is called once per frame
    new void Update()
    {
        SixPathsFlyToHome();
        SixPathsFlyUp();
        SixPathsMoveCircle();
        FlyToSky();
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(ExecuteSecondSkill());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            FirstSkill(0);
        }

    }
    public void FirstSkill(int Number)
    {
        player = FindClostestPlayer(50);
        Vector2 DirectionToPlayer = (Vector2)player.transform.GetChild(0).position - (Vector2)SpawnPoint[Number].transform.position;
        DirectionToPlayer.Normalize();
        GameObject BulletIns = Instantiate(DeadlineBullet, SpawnPoint[Number].position, SpawnPoint[Number].rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(DirectionToPlayer * 500);
    }

    public void SecondSkill(int Number)
    {

        player = FindClostestPlayer(50);
        Vector2 DirectionToPlayer = (Vector2)player.transform.GetChild(0).position - (Vector2)Paths[Number].transform.position;
        DirectionToPlayer.Normalize();
        Paths[Number].GetComponent<SixPaths>().TurnOnLine();
        Paths[Number].GetComponent<SixPaths>().SetUpStartLine((Vector2)Paths[Number].transform.position);
        Vector2 direction = (Vector2)player.transform.GetChild(0).position - (Vector2)Paths[Number].transform.position;
        RaycastHit2D[] hit = Physics2D.RaycastAll((Vector2)Paths[Number].transform.position, direction.normalized, direction.magnitude + 30);
        foreach (RaycastHit2D rayinfo in hit)
        {
            if (rayinfo.transform.gameObject.CompareTag("Ground"))
            {
                Paths[Number].GetComponent<SixPaths>().SetUpEndLine(rayinfo.point);

            }
        }
        StartCoroutine(Delay(1f, Number, DirectionToPlayer));
        
    }
    public void SixPathsFlyToHome()
    {
        if (ActiveSixPathMoveToHome)
        {
            for (int i = 0; i < 6; i++)
            {
                Paths[i].transform.position = Vector2.MoveTowards(Paths[i].transform.position, SixPathsHome[i].transform.position, 15 * Time.deltaTime);
            }
        }
    }

    public void SixPathsFlyUp()
    {
        if (ActiveSixPathFlyUp)
        {
            for (int i = 0; i < 6; i++)
            {
                Paths[i].transform.position = Vector2.MoveTowards(Paths[i].transform.position, SixPathsUp[i].transform.position, 50 * Time.deltaTime);
            }
        }
    }
    public void SixPathsMoveCircle()
    {
        if (ActiveSixPathsMoveCircle)
        {
            foreach (SixPaths a in Paths)
            {
                PosX = RotationCenter.position.x + Mathf.Cos(a.Angle) * RotationRadius;
                PosY = RotationCenter.position.y + Mathf.Sin(a.Angle) * RotationRadius;

                a.transform.position = new Vector2(PosX, PosY);
                a.Angle += Time.deltaTime * AngularSpeed;

            }
        }
    }
    public void FlyToSky()
    {
        if (ActiveFlyToSky)
        {
            rb.gravityScale = 0;
            transform.position = Vector2.MoveTowards(transform.position, PlaceToFly.position, 5 * Time.deltaTime);
        }
    }
    IEnumerator Delay(float second, int a, Vector2 direction)
    {
        yield return new WaitForSeconds(second);
        Paths[a].GetComponent<SixPaths>().TurnOffLine();
        Paths[a].GetComponent<Rigidbody2D>().isKinematic = false;
        Paths[a].GetComponent<Rigidbody2D>().AddForce(direction * 2500);
    }


    IEnumerator ExecuteSecondSkill()
    {
        ActiveSixPathsMoveCircle = true;
        yield return new WaitForSeconds(3f);
        ActiveSixPathMoveToHome = false;
        ActiveSixPathsMoveCircle = false;
        int a = Random.Range(0, 2);
        int b = Random.Range(2, 4);
        int c = Random.Range(4, 6);
        ActiveFlyToSky = true;
        ActiveSixPathMoveToHome = true;
        for (int i = 0; i < 3; i++)
        {
            ThreeFloor[i].GetComponent<Animator>().SetTrigger("Dissappear");
            ThreeFloor[i].GetComponent<Collider2D>().enabled = false;
        }
        yield return new WaitForSeconds(2f);
        ActiveSixPathMoveToHome = false;
        SecondSkill(a);
        yield return new WaitForSeconds(2f);
        SecondSkill(b);
        yield return new WaitForSeconds(2f);
        SecondSkill(c);
        yield return new WaitForSeconds(2f);
        ActiveSixPathMoveToHome = true;
        yield return new WaitForSeconds(4f);
        ActiveSixPathFlyUp = true;
        for (int i = 0; i < 6; i++)
        {
            Paths[i].GetComponent<SixPaths>().TurnOnLine();
            Paths[i].GetComponent<SixPaths>().SetUpStartLine((Vector2)Paths[i].transform.position);
            Paths[i].GetComponent<SixPaths>().SetUpEndLine((Vector2)SixPathsDown[i].transform.position);
        }
        yield return new WaitForSeconds(1f);
        ActiveSixPathFlyUp = false;
        ActiveSixPathMoveToHome = false;
        for (int i = 0; i < 6; i++)
        {
            Paths[i].GetComponent<SixPaths>().TurnOffLine();
            Paths[i].GetComponent<Rigidbody2D>().isKinematic = false;
            Paths[i].GetComponent<Rigidbody2D>().AddForce(Vector2.down * 3500);
        }
        yield return new WaitForSeconds(3f);
        ActiveFlyToSky = false;
        rb.gravityScale = 1;
        for (int i = 0; i < 3; i++)
        {
            ThreeFloor[i].GetComponent<Animator>().SetTrigger("Appear");
            ThreeFloor[i].GetComponent<Collider2D>().enabled = true;
        }
        

    }


}
