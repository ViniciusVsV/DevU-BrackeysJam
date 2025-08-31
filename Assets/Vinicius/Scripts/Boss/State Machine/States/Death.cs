using System.Collections;
using UnityEngine;

public class Death : BaseState
{
    [SerializeField] private float fallSpeed;
    [SerializeField] private float endDelay;

    [SerializeField] private GameObject jetpackPrefab;

    private AltitudeManager altitudeManager;

    private void Awake()
    {
        altitudeManager = FindFirstObjectByType<AltitudeManager>();
    }

    public override void StateEnter()
    {
        altitudeManager.hasStopped = true;

        BossKilledEffect.Instance.ApplyEffect();

        Instantiate(jetpackPrefab, transform.position, Quaternion.identity);
    }

    public override void StateFixedUpdate()
    {
        rb.linearVelocity = fallSpeed * Vector2.down;
    }
}