using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCanvasUI : MonoBehaviour
{
    public Luyldara luyldara;
    [SerializeField] private Image CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth.fillAmount = luyldara.CurrentHealthPoint / (float)luyldara.TotalHealthPoint;
    }
}
