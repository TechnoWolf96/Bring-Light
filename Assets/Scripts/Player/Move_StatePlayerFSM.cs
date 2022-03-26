
using UnityEngine;

public class Move_StatePlayerFSM : Player_BaseFSM
{

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        agent.rb.velocity = moveDirection.normalized * agent.speed;
        agent.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.rb.velocity = Vector2.zero;
    }


}
