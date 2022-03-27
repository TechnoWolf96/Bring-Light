using UnityEngine;

public class Player_BaseFSM : StateMachineBehaviour
{
    protected Player agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) agent = animator.gameObject.GetComponent<Player>();
    }


}
