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
    public AudioSource Source;
    public AudioClip Nghiancom;
    public bool AnCom;
    public GameObject Portal;

    public AudioSource EndGameSource;
    public AudioClip EndGameMusic;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupCamera());
    }


    public void SetupPositionAndCamera(Vector3 Place, int CameraSize)
    {
        transform.position = Place;
        Maincamera.orthographicSize = CameraSize;
    }
    public void ToggleBossPaths(bool value)
    {
        foreach (GameObject sixpath in SixPaths)
        {
            sixpath.SetActive(value);
        }
    }
    public void ToggleCanvasForIntro(bool value)
    {
        foreach (GameObject a in AllCanvas)
        {
            a.gameObject.SetActive(value);
        }
    }
    IEnumerator SetupCamera()
    {

        yield return new WaitForSeconds(2f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        player.blurCamera = gameObject.transform.GetChild(0).gameObject;
        if (player.gameObject.name.Equals("Phongsuke(Clone)"))
        {
            player.GetComponent<Phongsuke>().Sharingan = gameObject.transform.GetChild(1).gameObject;
        }
        SetupPositionAndCamera(new Vector3(0, 25, -10), 7);
        yield return new WaitForSeconds(1f);
        Boss.SetActive(true);
        yield return new WaitForSeconds(2f);
        Source.Play();
        ToggleBossPaths(true);
        yield return new WaitForSeconds(1f);
        enemy.isStart = true;
        yield return new WaitForSeconds(3f);
        SetupPositionAndCamera(new Vector3(0, 11, -10), 20);
        player.GetComponent<Character>().IsStart = true;
        StartCoroutine(enemy.Move());
        ToggleCanvasForIntro(true);

    }
    public void ActivePortal()
    {
        EndGameSource.clip = EndGameMusic;
        EndGameSource.Play();
        Portal.SetActive(true);
    }
    public void PlayNghiAnCom()
    {
        StartCoroutine(LastVoice());
    }
    public IEnumerator LastVoice()
    {
        yield return new WaitForSeconds(1f);
        Source.clip = Nghiancom;
        Source.Play();
    }
}
