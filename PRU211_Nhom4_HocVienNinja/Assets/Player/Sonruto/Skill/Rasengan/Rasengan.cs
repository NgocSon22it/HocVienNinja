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
        yield return new WaitForSeconds(3.8f);
        this.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
        yield return new WaitForSeconds(0.5f);
        this.transform.position = new Vector2(transform.position.x + 5f, transform.position.y);

        yield return new WaitForSeconds(.2f);
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
