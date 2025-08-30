using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerProjectileBehaviour : MonoBehaviour
{
    [Header("-----Behaviour-----")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeDuration;
    [SerializeField] private float destructionDelay;
    [SerializeField] private LayerMask collisionLayers;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("-----Effects-----")]
    [SerializeField] private ParticleSystem bulletHitParticles;
    [SerializeField] private TrailRenderer bulletTrail;
    private PlayerWeapon playerWeapon;

    [Header("-----Rumble Parameters-----")]
    [SerializeField] private float lowFrequency;
    [SerializeField] private float highFrequency;
    [SerializeField] private float duration;

    private Vector3 previousPosition;
    private bool hasCollided;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        previousPosition = transform.position;

        Destroy(gameObject, lifeDuration);
    }

    private void Start()
    {
        playerWeapon = FindFirstObjectByType<PlayerWeapon>();

        ControllerRumble.Instance.ApplyEffect(lowFrequency, highFrequency, duration);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.right * moveSpeed;
    }

    private void LateUpdate()
    {
        if (!hasCollided)
        {
            Vector2 direction = transform.position - previousPosition;
            float distance = direction.magnitude;

            if (distance > 0f)
            {
                RaycastHit2D hit = Physics2D.Raycast(previousPosition, direction.normalized, distance, collisionLayers);
                Debug.DrawLine(previousPosition, transform.position, Color.red, 0.1f);

                if (hit.collider != null)
                {
                    hasCollided = true;

                    spriteRenderer.enabled = false;
                    moveSpeed = 0f;

                    bulletHitParticles.transform.position = hit.point;
                    bulletHitParticles.Play();

                    bulletTrail.emitting = false;

                    if (hit.collider.gameObject.TryGetComponent<IDamageable>(out var damageable))
                        damageable.TakeDamage(playerWeapon.currentDamage, Vector2.zero);

                    Destroy(gameObject, destructionDelay);
                }
            }
        }

        previousPosition = transform.position;
    }
}