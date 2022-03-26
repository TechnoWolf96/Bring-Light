using UnityEngine;

public class Spellcaster_BaseFSM : StateMachineBehaviour
{
    protected SpellcasterFSM agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null)
            agent = animator.gameObject.GetComponent<SpellcasterFSM>();
    }


}
