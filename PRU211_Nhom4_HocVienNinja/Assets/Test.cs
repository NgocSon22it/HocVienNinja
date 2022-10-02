using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite =
        Resources.Load<Sprite>("Naruto_Image");
    }
}
