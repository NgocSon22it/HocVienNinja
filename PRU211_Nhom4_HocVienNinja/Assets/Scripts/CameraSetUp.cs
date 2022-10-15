using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetUp : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    public Character player;
    // Start is called before the first frame update
    void Start()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(SetUp());
    }
    IEnumerator SetUp()
    {
        yield return new WaitForSeconds(3f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        cinemachine.m_Follow = player.transform;
        cinemachine.m_LookAt = player.transform;
        player.GetComponent<Character>().blurCamera = gameObject.transform.GetChild(1).gameObject;
    }
}
