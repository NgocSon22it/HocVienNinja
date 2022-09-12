using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonruto : Player
{
    public Transform placeRasengan;
    public GameObject Rasengan;
    public AudioClip clip;
    public AudioSource source;

    // Start is called before the first frame update
    new void Start()
    {
        characterName = "Sonruto";
        totalHealthPoint = 100;
        currentHealthPoint = totalHealthPoint;
        totalChakra = 100;
        currentChakra = totalChakra;
        characterAttackRange = 5;
        characterDamage = 10;
        characterSpeed = 10;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        thirdSkill();
    }
    public void Playsound()
    {
        source.clip = clip;
        source.Play();
    }
    public override void thirdSkill()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetTrigger("Rasengan");
            Instantiate(Rasengan, placeRasengan.position, placeRasengan.rotation);           
        }
        
    }
}
