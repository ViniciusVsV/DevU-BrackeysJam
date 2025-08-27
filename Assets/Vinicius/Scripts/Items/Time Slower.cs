using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class TimeSlower : MonoBehaviour
{
    [SerializeField] private CustomTimeScale customTimeScale;
    [SerializeField] private float slowedTimeScale;
    [SerializeField] private float duration = 3f;

    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            col.enabled = false;
            spriteRenderer.enabled = false;

            StartCoroutine(Routine());
        }
    }

    private IEnumerator Routine()
    {
        customTimeScale.ChangeTimeScale(slowedTimeScale);

        yield return new WaitForSeconds(duration);

        customTimeScale.ResetTimeScale();

        Destroy(gameObject);
    }
}