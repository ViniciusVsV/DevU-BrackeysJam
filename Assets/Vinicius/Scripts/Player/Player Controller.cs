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
    [SerializeField] private float knockbackStrength;

    [Header("-----Movement-----")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDamping;
    private Vector2 moveDirection;
    private Vector2 moveToApply;

    private bool isFacingRight;
    private Rigidbody2D rb;

    [Header("-----Shoot-----")]
    [SerializeField] private PlayerWeapon playerWeapon;
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
        if (invulnerabilityTimer > Mathf.Epsilon)
            invulnerabilityTimer -= Time.unscaledDeltaTime;

        if (isShooting)
            playerWeapon.Shoot();
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

    public void TakeDamage(int damage, Vector2 direction)
    {
        if (invulnerabilityTimer > Mathf.Epsilon)
        {
            moveToApply += direction * knockbackStrength;
            return;
        }

        invulnerabilityTimer = invulnerabilityDuration;

        PlayerDamagedEffect.Instance.ApplyEffect(gameObject, invulnerabilityDuration, direction);

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
        else
            moveToApply += direction * knockbackStrength;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public PlayerWeapon GetWeapon(){ return playerWeapon; }
}