using System.Collections;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    public static SpriteBlink Instance;

    [SerializeField] private int numberOfBlinks;
    [SerializeField] private Color blinkColor;
    [SerializeField] private Color baseColor;
    private SpriteRenderer spriteRenderer;

    private Coroutine coroutine;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(GameObject newObject, float duration)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            spriteRenderer.color = baseColor;
        }

        coroutine = StartCoroutine(Routine(newObject, duration));
    }

    private IEnumerator Routine(GameObject newObject, float duration)
    {
        spriteRenderer = newObject.GetComponent<SpriteRenderer>();

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