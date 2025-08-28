using System.Collections.Generic;
using UnityEngine;

public class CameraLeave : BaseState
{
    [SerializeField] private float leaveSpeed;
    [SerializeField] private float leaveDelay;
    private float leaveDelayTimer;
    [SerializeField] private float leaveDuration;
    private float leaveDurationTimer;

    private Vector2 leaveDirection;

    public override void StateEnter()
    {
        leaveDirection = controller.isFacingRight ? Vector2.left : Vector2.right;

        leaveDelayTimer = leaveDelay;
        leaveDurationTimer = leaveDuration;
    }

    public override void StateUpdate()
    {
        if (leaveDelayTimer > Mathf.Epsilon)
        {
            leaveDelayTimer -= Time.deltaTime;
            return;
        }

        if (leaveDurationTimer > Mathf.Epsilon)
            leaveDurationTimer -= Time.deltaTime;
        else
        {
            controller.SetRocketAttackState();
            return;
        }

        rb.linearVelocity = leaveSpeed * leaveDirection;
    }

    public override void StateExit()
    {
        rb.linearVelocity = Vector2.zero;
    }
}