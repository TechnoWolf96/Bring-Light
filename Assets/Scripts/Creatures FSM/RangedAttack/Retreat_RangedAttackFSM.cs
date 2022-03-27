using UnityEngine;

public class Retreat_RangedAttackFSM : RangedAttack_BaseFSM
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.navAgent.isStopped = false;
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null || agent.follow == null) return;
        Vector2 newPosition = agent.transform.position + (agent.transform.position - agent.follow.transform.position).normalized;
        agent.navAgent.SetDestination(newPosition);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.navAgent.isStopped = true;
    }


}
