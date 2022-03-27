using UnityEngine;

public class RangedSpellcaster_BaseFSM : StateMachineBehaviour
{
    protected RangedSpellcasterFSM agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null)
            agent = animator.gameObject.GetComponent<RangedSpellcasterFSM>();
    }


}
