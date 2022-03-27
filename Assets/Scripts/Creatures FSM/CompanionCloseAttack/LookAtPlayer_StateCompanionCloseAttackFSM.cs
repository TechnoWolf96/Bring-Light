using UnityEngine;

public class LookAtPlayer_StateCompanionCloseAttackFSM : CompanionCloseAttack_BaseFSM
{

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.LookAt(agent.player.position);
    }
}
