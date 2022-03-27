using UnityEngine;

public class LookAtTarget_StateCloseAttackFSM : CloseAttack_BaseFSM
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.follow != null && agent != null) agent.LookAt(agent.follow.transform.position);
    }
}
