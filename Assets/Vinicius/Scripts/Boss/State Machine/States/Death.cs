using System.Collections;
using UnityEngine;

public class Death : BaseState
{
    [SerializeField] private float fallSpeed;
    [SerializeField] private float endDelay;

    private AltitudeManager altitudeManager;

    private void Awake()
    {
        altitudeManager = FindFirstObjectByType<AltitudeManager>();
    }

    public override void StateEnter()
    {
        altitudeManager.hasStopped = true;

        StartCoroutine(Routine());
    }

    public override void StateFixedUpdate()
    {
        rb.linearVelocity = fallSpeed * Vector2.down;
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(endDelay);

        BossKilledEffect.Instance.ApplyEffect();
    }
    //Soltar a jetpack (opcional)

    //Cair até a morte
}