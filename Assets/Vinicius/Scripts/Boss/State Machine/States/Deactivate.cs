using UnityEngine;

public class Deactivate : BaseState
{
    [SerializeField] private float leaveSpeed;

    private Vector2 leaveDirection;

    public override void StateEnter()
    {
        leaveDirection = controller.isFacingRight ? new Vector2(-1f, 1f) : new Vector2(1f, 1f);
    }

    public override void StateUpdate()
    {
        rb.linearVelocity = leaveSpeed * leaveDirection;
    }
}