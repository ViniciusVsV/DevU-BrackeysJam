using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("-----Health-----")]
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float invulnerabilityDuration;
    private float invulnerabilityTimer;

    [Header("-----Movement-----")]
    [SerializeField] private float moveSpeed;
    private Vector2 moveDirection;
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
        rb.linearVelocity = moveDirection.normalized * moveSpeed;
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
        if (other.CompareTag("Obstacle"))
            TakeDamage(false, null);

        else if (other.CompareTag("Boss"))
            TakeDamage(true, other.gameObject);
    }

    //Ao tomar dano por contato do boss, fazer ele tomar um knockback para longe do boss
    //A otomar dano, dar ao jogar um pequeno tempo de invulnerabilidade
    private void TakeDamage(bool hasKnockback, GameObject other)
    {
        if (invulnerabilityTimer > Mathf.Epsilon)
            return;

        invulnerabilityTimer = invulnerabilityDuration;

        currentHealth--;

        if (currentHealth <= 0)
            Die();

        else if (hasKnockback)
        {
            //aplica um knockback
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}