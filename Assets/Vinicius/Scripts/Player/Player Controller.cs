using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("-----Health-----")]
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float invulnerabilityDuration;
    private float invulnerabilityTimer;
    [SerializeField] private float knockbackStrength;
    private HeartsManager heartsManager;

    [Header("-----Movement-----")]
    [SerializeField] private float baseMoveSpeed;
    private float currentMoveSpeed;
    [SerializeField] private float moveDamping;
    private Vector2 moveDirection;
    private Vector2 moveToApply;
    private Coroutine moveSpeedRoutine;
    private bool isFacingRight;

    [Header("-----Shoot-----")]
    [SerializeField] private PlayerWeapon playerWeapon;
    [HideInInspector] public Vector2 lookDirection;
    private bool isShooting;

    [Header("-----Crash-----")]
    [SerializeField] private Sprite crashedSprite;
    [SerializeField] private float crashDuration;
    [SerializeField] private float crashY;

    public bool isDeactivated;
    public bool isOnController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        currentMoveSpeed = baseMoveSpeed;

        isFacingRight = true;
    }

    private void Start()
    {
        heartsManager = FindFirstObjectByType<HeartsManager>();
    }

    private void Update()
    {
        if (isDeactivated)
            return;

        if (invulnerabilityTimer > Mathf.Epsilon)
            invulnerabilityTimer -= Time.unscaledDeltaTime;

        if (isShooting)
            playerWeapon.Shoot();

        CheckFlip();
    }

    private void FixedUpdate()
    {
        Vector2 moveStrength = moveDirection.normalized * currentMoveSpeed;
        moveStrength += moveToApply;

        moveToApply /= moveDamping;

        if (Mathf.Abs(moveToApply.x) <= 0.01f && Mathf.Abs(moveToApply.y) <= 0.01f)
            moveToApply = Vector2.zero;

        if (isDeactivated)
            return;

        rb.linearVelocity = moveStrength;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
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

    private void CheckFlip()
    {
        if (isOnController)
        {
            if (lookDirection == Vector2.zero)
            {
                if (isFacingRight && moveDirection.x < 0f || !isFacingRight && moveDirection.x > 0f)
                    Flip();
            }

            else
            {
                if (isFacingRight && lookDirection.x < 0f || !isFacingRight && lookDirection.x > 0f)
                    Flip();
            }
        }

        else
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (isFacingRight && mousePosition.x < transform.position.x || !isFacingRight && mousePosition.x > transform.position.x)
                Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
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
        heartsManager.DeactivateHeart(maxHealth - currentHealth);

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

    public float Crash()
    {
        isDeactivated = true;

        StartCoroutine(CrashRoutine());

        return crashDuration;
    }
    private IEnumerator CrashRoutine()
    {
        float elapsedTime = 0f;
        float startY = transform.position.y;

        while (elapsedTime < crashDuration)
        {
            float yPosition = Mathf.Lerp(startY, crashY, elapsedTime / crashDuration);

            transform.position = new Vector2(transform.position.x, yPosition);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector2(transform.position.x, crashY);

        animator.Play("Crash");
    }

    public PlayerWeapon GetWeapon() { return playerWeapon; }

    public void BuffMoveSpeed(float newMoveSpeed, float duration)
    {
        if (moveSpeedRoutine != null)
            StopCoroutine(moveSpeedRoutine);

        moveSpeedRoutine = StartCoroutine(MoveSpeedRoutine(newMoveSpeed, duration));
    }
    private IEnumerator MoveSpeedRoutine(float newMoveSpeed, float duration)
    {
        currentMoveSpeed = newMoveSpeed;

        yield return new WaitForSeconds(duration);

        currentMoveSpeed = baseMoveSpeed;
    }
}