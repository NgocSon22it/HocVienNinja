using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpAnimation_BossRunOut : StateMachineBehaviour
{
    Luyldara luyldara;
    BossCamera mainCamera;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        luyldara = GameObject.FindGameObjectWithTag("Boss").GetComponent<Luyldara>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BossCamera>();
        luyldara.IsOut = true;
        luyldara.GetComponent<SpriteRenderer>().color = Color.white;
        luyldara.StopAllCoroutines();
        luyldara.transform.position = new Vector3(1, 25, 0);
        mainCamera.SetupPositionAndCamera(new Vector3(1, 25, -10), 7);
        mainCamera.ToggleBossPaths(false);
        mainCamera.ToggleCanvasForIntro(false);
        mainCamera.PlayNghiAnCom();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(luyldara.gameObject);
        mainCamera.SetupPositionAndCamera(new Vector3(0, 11, -10), 20);
        mainCamera.ToggleBossPaths(true);
        mainCamera.ToggleCanvasForIntro(true);
    }

}
