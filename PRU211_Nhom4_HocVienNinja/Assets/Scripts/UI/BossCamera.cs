using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{
    public Camera Maincamera;
    public Luyldara enemy;
    Character player;

    public GameObject Boss;
    public GameObject[] AllCanvas;
    public GameObject[] SixPaths;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupCamera());
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        player.blurCamera = gameObject.transform.GetChild(0).gameObject;
        player.GetComponent<Sonruto>().enabled = false;
    }

    IEnumerator SetupCamera() { 


        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(0, 25, -10);
        Maincamera.orthographicSize = 7;
        yield return new WaitForSeconds(1f);
        Boss.SetActive(true);
        yield return new WaitForSeconds(2f);
        foreach(GameObject sixpath in SixPaths)
        {
            sixpath.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        enemy.isStart = true;
        yield return new WaitForSeconds(3f);
        transform.position = new Vector3(0, 11, -10);
        Maincamera.orthographicSize = 20;
        player.GetComponent<Sonruto>().enabled = true;
        StartCoroutine(enemy.Move());
        foreach(GameObject a in AllCanvas)
        {
            a.gameObject.SetActive(true);
        }

    }
}
