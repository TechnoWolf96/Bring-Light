using UnityEngine;

public class RangedAttack_BaseFSM : StateMachineBehaviour
{
    protected RangedAttackFSM agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<RangedAttackFSM>();
    }

}
