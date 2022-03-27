using UnityEngine;

public class Stalk_StateCloseAttackFSM : CloseAttack_BaseFSM
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.navAgent.isStopped = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.follow == null || agent == null) return;
        agent.navAgent.SetDestination(agent.follow.transform.position);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.navAgent.isStopped = true;
    }

}
