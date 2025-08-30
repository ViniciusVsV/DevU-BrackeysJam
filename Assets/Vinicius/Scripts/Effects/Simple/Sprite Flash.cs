using System.Collections;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    public static SpriteFlash Instance;

    private Coroutine coroutine;

    [SerializeField] private float duration;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private Material baseMaterial;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(GameObject newObject)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            spriteRenderer.material = baseMaterial;
        }

        coroutine = StartCoroutine(Routine(newObject));
    }

    private IEnumerator Routine(GameObject newObject)
    {
        spriteRenderer = newObject.GetComponent<SpriteRenderer>();
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = baseMaterial;
    }

    public float GetDuration() { return duration; }
}