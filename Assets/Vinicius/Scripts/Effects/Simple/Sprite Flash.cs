using System.Collections;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    public static SpriteFlash Instance;

    private Coroutine coroutine;

    [SerializeField] private float duration;
    [SerializeField] private Material flashMaterial;
    private Material currentMaterial;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(GameObject gameObject)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Routine(gameObject));
    }

    private IEnumerator Routine(GameObject gameObject)
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        currentMaterial = spriteRenderer.material;

        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = currentMaterial;
    }

    public float GetDuration() { return duration; }
}