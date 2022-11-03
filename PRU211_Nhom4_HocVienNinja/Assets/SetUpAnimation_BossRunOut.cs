using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpAnimation_BossRunOut : StateMachineBehaviour
{
    Luyldara luyldara;
    BossCamera mainCamera;
    Character character;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        luyldara = GameObject.FindGameObjectWithTag("Boss").GetComponent<Luyldara>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BossCamera>();
        luyldara.GetComponent<Collider2D>().enabled = false;
        luyldara.IsOut = true;
        luyldara.GetComponent<SpriteRenderer>().color = Color.white;
        luyldara.StopAllCoroutines();
        luyldara.transform.position = new Vector3(1, 25, 0);
        mainCamera.SetupPositionAndCamera(new Vector3(1, 25, -10), 7);
        mainCamera.ToggleBossPaths(false);
        mainCamera.ToggleCanvasForIntro(false);
        mainCamera.PlayNghiAnCom();
        character.enabled = false;
        character.IsHurt = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Score += luyldara.Score;
        GameManager.Coin += luyldara.Coin;
        Destroy(luyldara.gameObject);
        mainCamera.SetupPositionAndCamera(new Vector3(0, 11, -10), 20);
        mainCamera.ToggleBossPaths(true);
        mainCamera.ToggleCanvasForIntro(true);
        mainCamera.ActivePortal();
        character.enabled = true;
        character.IsHurt = false;
    }

}
