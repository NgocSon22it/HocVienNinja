using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasenganDamage : MonoBehaviour
{
    public Transform AttackPoint;
    public float Range;
    public LayerMask LayerToAttack;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RasenganLogic());
    }

    IEnumerator RasenganLogic()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, Range, LayerToAttack);

        if (hitEnemy != null)
        {
            foreach (Collider2D enemy in hitEnemy)
            {
                enemy.GetComponent<Enemy>().TakeDamage(100);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, Range);
    }
}
