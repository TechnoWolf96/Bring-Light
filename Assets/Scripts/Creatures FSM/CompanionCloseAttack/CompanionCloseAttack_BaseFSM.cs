using UnityEngine;

public class CompanionCloseAttack_BaseFSM : StateMachineBehaviour
{
    protected CompanionCloseAttackFSM agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) agent = animator.gameObject.GetComponent<CompanionCloseAttackFSM>();
    }


}
