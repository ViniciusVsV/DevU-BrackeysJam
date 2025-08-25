using System.Collections;
using UnityEngine;

public class PlayerDamagedEffect : MonoBehaviour
{
    public static PlayerDamagedEffect Instance;

    [Header("-----Parameters-----")]
    [SerializeField] private float hitStopDuration;

    private Coroutine coroutine;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(GameObject gameObject, float invulnerabilityDuration)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Routine(gameObject, invulnerabilityDuration));
    }

    private IEnumerator Routine(GameObject gameObject, float invulnerabilityDuration)
    {
        SpriteFlash.Instance.ApplyEffect(gameObject);
        HitStop.Instance.ApplyEffect(hitStopDuration);
        CameraShake.Instance.ApplyEffect(false);

        yield return new WaitForSeconds(SpriteFlash.Instance.GetDuration());

        SpriteBlink.Instance.ApplyEffect(gameObject, invulnerabilityDuration);
    }
}