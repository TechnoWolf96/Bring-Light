using UnityEngine;

public class Companion_BaseFSM : StateMachineBehaviour
{
    protected CompanionFSM agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) agent = animator.gameObject.GetComponent<CompanionFSM>();
    }


}
