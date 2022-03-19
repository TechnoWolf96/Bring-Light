using UnityEngine;

public class Spellcaster_BaseFSM : StateMachineBehaviour
{
    protected SpellcasterFSM agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<SpellcasterFSM>();
    }


}
