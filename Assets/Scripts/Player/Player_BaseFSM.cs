using UnityEngine;

public class Player_BaseFSM : StateMachineBehaviour
{
    protected Player agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<Player>();
    }


}
