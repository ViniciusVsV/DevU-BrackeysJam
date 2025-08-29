using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public static HitStop Instance;

    private float currentTimeScale;
    private Coroutine coroutine;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(float duration)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Routine(duration));
    }

    private IEnumerator Routine(float duration)
    {
        currentTimeScale = Time.timeScale;

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = currentTimeScale;
    }
}