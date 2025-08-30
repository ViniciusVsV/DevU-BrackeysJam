using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class GuidedRocketBehaviour : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    [SerializeField] private Collider2D explosionCol;

    private Transform playerPosition;
    private Vector2 playerDirection;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private bool hasStopped;
    private bool hasCollided;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        currentHealth = maxHealth;
    }

    private void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (hasStopped)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        playerDirection = ((Vector2)playerPosition.position - rb.position).normalized;

        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        rb.linearVelocity = transform.right * moveSpeed;
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Explode();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided)
            return;

        if (other.CompareTag("Player") || other.CompareTag("Obstacle"))
        {
            hasCollided = true;
            Explode();
        }
    }

    private void Explode()
    {
        col.enabled = false;

        hasStopped = true;

        spriteRenderer.enabled = false;

        ExplosionEffect.Instance.ApplyEffect(transform.position);

        explosionCol.enabled = true;

        Destroy(gameObject, 0.1f);
    }
}