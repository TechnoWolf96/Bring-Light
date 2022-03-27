using UnityEngine;

public class AttackSpell_StateRangedSpellcasterFSM : RangedSpellcaster_BaseFSM
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.currentSpell.BreakCast();
    }
}
