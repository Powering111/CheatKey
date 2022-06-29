using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for enemy
public class AttackAfterAnimation : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("ye");
        animator.GetComponent<Enemy>().Attack();
    }
}