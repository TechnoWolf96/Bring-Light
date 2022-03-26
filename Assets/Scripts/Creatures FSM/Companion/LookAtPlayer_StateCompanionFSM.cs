using UnityEngine;

public class LookAtPlayer_StateCompanionFSM : Companion_BaseFSM
{

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.LookAt(agent.player.position);
    }
}
