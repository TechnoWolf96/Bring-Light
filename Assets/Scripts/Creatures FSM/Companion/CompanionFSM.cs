using UnityEngine;

public class CompanionFSM : CloseAttackFSM
{
    [SerializeField] protected Transform _player;
    public Transform player { get => _player; }

    protected override void UpdateState()
    {
        base.UpdateState();
        anim.SetFloat("DistanceToPlayer", Vector2.Distance(transform.position, player.position));
    }

}
