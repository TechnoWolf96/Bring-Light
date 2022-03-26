using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget_StateCloseAttackFSM : CloseAttack_BaseFSM
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.LookAt(agent.follow.position);
    }
}
