
using UnityEngine;

public class LookAtCursor_StatePlayerFSM : Player_BaseFSM
{

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null || agent.pause) return;
        agent.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }


}
