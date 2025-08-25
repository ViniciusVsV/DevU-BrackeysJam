using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerProjectileBehaviour : MonoBehaviour
{
    [Header("-----Behaviour-----")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeDuration;
    private Rigidbody2D rb;

    [Header("-----Paramaters-----")]
    [SerializeField] private int damage;
    [SerializeField] private Sprite sprite;

    [Header("-----Rumble Parameters-----")]
    [SerializeField] private float lowFrequency;
    [SerializeField] private float highFrequency;
    [SerializeField] private float duration;

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
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
                damageable.TakeDamage(damage, Vector2.zero);

            Destroy(gameObject);
        }
    }
}