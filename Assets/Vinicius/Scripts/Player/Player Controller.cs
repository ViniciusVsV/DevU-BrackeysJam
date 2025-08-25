using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("-----Health-----")]
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float invulnerabilityDuration;
    private float invulnerabilityTimer;

    [Header("-----Knockback-----")]
    [SerializeField] private float knockbackStrength;
    private Vector2 KnockbackDirection;

    [Header("-----Movement-----")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDamping;
    private Vector2 moveDirection;
    private Vector2 moveToApply;

    private bool isFacingRight;
    private Rigidbody2D rb;

    [Header("-----Shoot-----")]
    [SerializeField] private float shootCooldown;
    private float shootCooldownTimer;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform crosshairPosition;
    private Vector2 shootDirection;
    private Quaternion shootRotation;
    [HideInInspector] public Vector2 lookDirection;
    private bool isShooting;

    public bool isOnController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        isFacingRight = true;
    }

    private void Update()
    {
        if (shootCooldownTimer > Mathf.Epsilon)
            shootCooldownTimer -= Time.deltaTime;

        if (invulnerabilityTimer > Mathf.Epsilon)
            invulnerabilityTimer -= Time.deltaTime;

        if (isShooting && shootCooldownTimer <= Mathf.Epsilon)
        {
            shootDirection = (crosshairPosition.position - transform.position).normalized;
            shootRotation = Quaternion.LookRotation(Vector3.forward, shootDirection);

            shootCooldownTimer = shootCooldown;

            Instantiate(projectilePrefab, projectileSpawnPoint.position, shootRotation);
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveStrength = moveDirection.normalized * moveSpeed;
        moveStrength += moveToApply;

        moveToApply /= moveDamping;

        if (Mathf.Abs(moveToApply.x) <= 0.01f && Mathf.Abs(moveToApply.y) <= 0.01f)
            moveToApply = Vector2.zero;

        rb.linearVelocity = moveStrength;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

        Flip();
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            isShooting = true;
        else
            isShooting = false;
    }

    public void CheckController(InputAction.CallbackContext context)
    {
        if (context.control.device is Gamepad)
            isOnController = true;
        else
            isOnController = false;
    }

    private void Flip()
    {
        if (isFacingRight && moveDirection.x < 0f || !isFacingRight && moveDirection.x > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Boss"))
            TakeDamage(1, (transform.position - other.transform.position).normalized);
    }

    //Ao tomar dano por contato do boss, fazer ele tomar um knockback para longe do boss
    //A otomar dano, dar ao jogar um pequeno tempo de invulnerabilidade
    public void TakeDamage(int damage, Vector2 direction)
    {
        if (invulnerabilityTimer > Mathf.Epsilon)
        {
            TakeKnockback(direction);
            return;
        }

        invulnerabilityTimer = invulnerabilityDuration;

        PlayerDamagedEffect.Instance.ApplyEffect(gameObject, invulnerabilityDuration);

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
        else
            TakeKnockback(direction);
    }

    private void TakeKnockback(Vector2 knockbackDirection)
    {
        moveToApply += knockbackDirection * knockbackStrength;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}