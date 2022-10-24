using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amaterasu : MonoBehaviour
{
    public Enemy Enemy;
    public float TimeToDamage;
    private float TimeCause;

    private void Start()
    {
        StartCoroutine(FireStop());
    }
    // Update is called once per frame
    void Update()
    {
        if(Enemy != null)
        {
            this.transform.position = Enemy.transform.GetChild(0).position;
            if (TimeCause <= 0)
            {
                Enemy.GetComponent<Enemy>().TakeDamage(20);
                TimeCause = TimeToDamage;
            }
            else
            {
                TimeCause -= Time.deltaTime;
            }
        }
        else
        {
            Destroy(gameObject);
        }  
    }


    IEnumerator FireStop()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
