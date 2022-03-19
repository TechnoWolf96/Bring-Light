using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack_BaseFSM : StateMachineBehaviour
{
    protected CloseAttackFSM agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<CloseAttackFSM>();
    }


}
