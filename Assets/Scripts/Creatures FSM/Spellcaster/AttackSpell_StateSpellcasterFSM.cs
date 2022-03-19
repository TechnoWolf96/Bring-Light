using UnityEngine;

public class AttackSpell_StateSpellcasterFSM : Spellcaster_BaseFSM
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.currentSpell.BreakCast();
    }
}
