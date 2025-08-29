using UnityEngine;

public class Idle : BaseState
{
    [SerializeField] private float floatSpeed;

    [SerializeField] private float targetY;
    private float currentTargetY;

    private int moveDirection;

    public override void StateEnter()
    {
        currentTargetY = targetY;

        if (currentTargetY > tr.position.y)

            moveDirection = 1;

        else
        {
            moveDirection = -1;
            currentTargetY *= -1;
        }
    }

    public override void StateUpdate()
    {
        if (Mathf.Abs(tr.position.y - currentTargetY) <= 1f)
        {
            currentTargetY *= -1;
            moveDirection *= -1;
        }
    }

    public override void StateFixedUpdate()
    {
        rb.linearVelocity = floatSpeed * new Vector2(0, moveDirection);
    }

    public override void StateExit()
    {
        rb.linearVelocity = Vector2.zero;
    }
}