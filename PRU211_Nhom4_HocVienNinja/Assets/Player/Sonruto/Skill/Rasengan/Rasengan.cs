using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasengan : MonoBehaviour
{
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupRasengan());
    }

    IEnumerator SetupRasengan()
    {
        yield return new WaitForSeconds(4.5f);
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
