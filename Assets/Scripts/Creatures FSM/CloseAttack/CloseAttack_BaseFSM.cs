using UnityEngine;

public class CloseAttack_BaseFSM : StateMachineBehaviour
{
    protected CloseAttackFSM agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null)
            agent = animator.gameObject.GetComponent<CloseAttackFSM>();
    }


}
