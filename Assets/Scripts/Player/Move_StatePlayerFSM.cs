using UnityEngine;

public class Move_StatePlayerFSM : Player_BaseFSM
{

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) return;
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        agent.rb.velocity = moveDirection.normalized * agent.speed;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) return;
        agent.rb.velocity = Vector2.zero;
    }


}
