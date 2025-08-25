using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerProjectileBehaviour : MonoBehaviour
{
    [Header("-----Behaviour-----")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeDuration;
    private Rigidbody2D rb;

    [Header("-----Rumble Parameters-----")]
    [SerializeField] private float lowFrequency = 0.5f;
    [SerializeField] private float highFrequency = 0.5f;
    [SerializeField] private float duration = 0.25f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifeDuration);
    }

    private void Start()
    {
        ControllerRumble.Instance.ApplyEffect(lowFrequency, highFrequency, duration);
    }

    void Update()
    {
        rb.linearVelocity = transform.up * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Boss"))
            Destroy(gameObject);
    }
}