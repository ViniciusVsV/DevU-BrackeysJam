using UnityEngine;

public class Dash : BaseState
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDelay;

    [SerializeField] private Transform rightDestination;
    [SerializeField] private Transform leftDestination;
    [SerializeField] private float maxDashVariation;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private Vector2 dashDirection;
    private float dashDistance;

    public override void StateEnter()
    {
        startPosition = tr.position;

        Transform usedDestination = controller.isFacingRight ? rightDestination : leftDestination;
        dashDirection = controller.isFacingRight ? Vector2.right : Vector2.left;

        dashDistance = Mathf.Abs(usedDestination.position.x - tr.position.x) + Random.Range(-maxDashVariation, maxDashVariation);
    }

    public override void StateUpdate()
    {
        targetPosition = startPosition + dashDirection * dashDistance;

        tr.position = Vector2.MoveTowards(tr.position, targetPosition, dashSpeed * customTimeScale.GetDeltaTime());

        if (Vector2.Distance(tr.position, targetPosition) < 0.01f)
        {
            controller.isFacingRight = !controller.isFacingRight;
            controller.SetIdleState();
        }
    }
}