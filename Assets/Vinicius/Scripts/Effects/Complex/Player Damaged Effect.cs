using System.Collections;
using UnityEngine;

public class PlayerDamagedEffect : MonoBehaviour
{
    public static PlayerDamagedEffect Instance;

    [Header("-----Parameters-----")]
    [SerializeField] private float hitStopDuration;

    [SerializeField] private float lowFrequency;
    [SerializeField] private float highFrequency;
    [SerializeField] private float rumbleDuration;

    private Coroutine coroutine;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(GameObject gameObject, float invulnerabilityDuration, Vector2 direction)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Routine(gameObject, invulnerabilityDuration, direction));
    }

    private IEnumerator Routine(GameObject gameObject, float invulnerabilityDuration, Vector2 direction)
    {
        SpriteFlash.Instance.ApplyEffect(gameObject);
        HitStop.Instance.ApplyEffect(hitStopDuration);
        CameraShake.Instance.ApplyEffect(false, direction);
        ControllerRumble.Instance.ApplyEffect(lowFrequency, highFrequency, rumbleDuration);

        yield return new WaitForSeconds(SpriteFlash.Instance.GetDuration());

        SpriteBlink.Instance.ApplyEffect(gameObject, invulnerabilityDuration);
    }
}