using UnityEngine;

public class DashWindUp : BaseState
{
    [SerializeField] private float duration;
    private float timer;
    [SerializeField] private float approachSpeed = 10f;

    private Transform playerTransform;
    private Vector2 targetPosition;
    public bool isFollowingPlayer;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public override void StateEnter()
    {
        isFollowingPlayer = false;

        timer = duration;
    }

    public override void StateUpdate()
    {
        if (!isFollowingPlayer)
        {
            targetPosition = new Vector2(tr.position.x, playerTransform.position.y);

            tr.position = Vector2.MoveTowards(tr.position, targetPosition, approachSpeed * customTimeScale.GetDeltaTime());

            if (Mathf.Abs(tr.position.y - playerTransform.position.y) < 0.1f)
                isFollowingPlayer = true;
        }

        if (isFollowingPlayer)
        {
            tr.position = new Vector2(tr.position.x, playerTransform.position.y);

            timer -= customTimeScale.GetDeltaTime();

            if (timer < Mathf.Epsilon)
                controller.SetDashState();
        }
    }
}