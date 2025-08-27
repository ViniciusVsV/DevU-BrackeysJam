using UnityEngine;

public class Idle : BaseState
{
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatAmplitude;
    private float newYPosition;

    [SerializeField] private Transform xPoint;
    private Vector2 initialPosition;
    private float timeScaleTimer;

    public override void StateEnter()
    {
        initialPosition = xPoint.position;

        timeScaleTimer = 0f;
    }

    public override void StateUpdate()
    {
        timeScaleTimer += customTimeScale.GetDeltaTime();

        newYPosition = initialPosition.y + Mathf.Sin(timeScaleTimer * floatSpeed) * floatAmplitude;

        tr.position = new Vector2(transform.position.x, newYPosition);
    }
}