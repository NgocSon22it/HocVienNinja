using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetUp : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    public Character player;
    float Yaxis;
    // Start is called before the first frame update
    void Start()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(SetUp());

    }

    private void FixedUpdate()
    {
        if(transform.position.y > Yaxis)
        {
            transform.position = new Vector2(transform.position.x, Yaxis);
        }
    }
    IEnumerator SetUp()
    {
        yield return new WaitForSeconds(2f);
        Yaxis = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        cinemachine.m_Follow = player.transform;
        cinemachine.m_LookAt = player.transform;
        player.GetComponent<Character>().blurCamera = gameObject.transform.GetChild(1).gameObject;
        player.GetComponent<Character>().IsStart = true;
        if (player.gameObject.name.Equals("Phongsuke(Clone)"))
        {
            player.GetComponent<Phongsuke>().Sharingan = gameObject.transform.GetChild(2).gameObject;
        }
    }
}
