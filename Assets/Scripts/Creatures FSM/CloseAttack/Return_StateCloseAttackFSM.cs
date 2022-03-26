using UnityEngine;

public class Return_StateCloseAttackFSM : CloseAttack_BaseFSM
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.navAgent.isStopped = false;
        agent.navAgent.SetDestination(agent.spawnPosition);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.navAgent.isStopped = true;
    }

}
