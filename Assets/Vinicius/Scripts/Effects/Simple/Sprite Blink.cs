using System.Collections;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    public static SpriteBlink Instance;

    [SerializeField] private int numberOfBlinks;
    [SerializeField] private Color blinkColor;
    private SpriteRenderer spriteRenderer;
    private Color baseColor;

    private Coroutine coroutine;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(GameObject gameObject, float duration)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Routine(gameObject, duration));
    }

    private IEnumerator Routine(GameObject gameObject, float duration)
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;

        float elapsedTime = 0f;
        float elapsedPercentage;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            elapsedPercentage = elapsedTime / duration;

            if (elapsedPercentage > 1)
                elapsedPercentage = 1;

            float pingPongPercentage = Mathf.PingPong(elapsedPercentage * 2 * numberOfBlinks, 1);
            spriteRenderer.color = Color.Lerp(baseColor, blinkColor, pingPongPercentage);

            yield return null;
        }

        spriteRenderer.color = baseColor;
    }
}