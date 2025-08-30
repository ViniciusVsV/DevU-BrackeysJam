using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class BossController : MonoBehaviour, IDamageable
{
    [Header("-----States-----")]
    [SerializeField] private Idle idleState;
    [SerializeField] private DashWindUp dashWindUpState;
    [SerializeField] private Dash dashState;
    [SerializeField] private CameraLeave cameraLeaveState;
    [SerializeField] private RocketAttack rocketAttackState;
    [SerializeField] private Deactivate deactivateState;
    [SerializeField] private GuidedRocketAttack guidedRocketAttackState;
    private StateMachine stateMachine;

    [Header("-----Health-----")]
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [Header("-----AI-----")]
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float maxTimeVariation;
    [SerializeField] private float probabilityDecrease = 0.15f;
    private float[] attackProbabilities = { 0.34f, 0.33f, 0.33f };
    private float attackTimer;

    private Rigidbody2D rb;
    private Animator animator;

    public bool isFacingRight;
    public bool isDashing;
    public bool canTakeDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();

        idleState.Setup(rb, transform, animator, this);
        dashWindUpState.Setup(rb, transform, animator, this);
        dashState.Setup(rb, transform, animator, this);
        cameraLeaveState.Setup(rb, transform, animator, this);
        rocketAttackState.Setup(rb, transform, animator, this);
        deactivateState.Setup(rb, transform, animator, this);
        guidedRocketAttackState.Setup(rb, transform, animator, this);

        stateMachine.Set(idleState);

        currentHealth = maxHealth;

        isFacingRight = false;
        canTakeDamage = true;

        attackTimer = timeBetweenAttacks;
    }

    private void Update()
    {
        stateMachine.currentState.StateUpdate();

        if (stateMachine.currentState == idleState)
            attackTimer -= Time.deltaTime;

        if (attackTimer < Mathf.Epsilon)
        {
            attackTimer = Random.Range(timeBetweenAttacks - maxTimeVariation, timeBetweenAttacks + maxTimeVariation);

            ChooseAttack();
        }
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.StateFixedUpdate();
    }

    private void ChooseAttack()
    {
        float randomAttackValue = Random.Range(0f, 1f);
        float sum = 0f;
        int chosenIndex = 0;

        for (int i = 0; i < attackProbabilities.Length; i++)
        {
            sum += attackProbabilities[i];

            if (randomAttackValue <= sum)
            {
                chosenIndex = i;
                break;
            }
        }

        switch (chosenIndex)
        {
            case 0:
                stateMachine.Set(dashWindUpState);
                break;
            case 1:
                stateMachine.Set(cameraLeaveState);
                break;
            case 2:
                stateMachine.Set(guidedRocketAttackState);
                break;
        }

        if (attackProbabilities[chosenIndex] > 0.1f)
        {
            attackProbabilities[chosenIndex] -= probabilityDecrease;

            float probabilityIncrease = probabilityDecrease / (attackProbabilities.Length - 1);

            for (int i = 0; i < attackProbabilities.Length; i++)
            {
                if (i != chosenIndex)
                    attackProbabilities[i] += probabilityIncrease;
            }
        }

        float total = 0f;
        for (int i = 0; i < attackProbabilities.Length; i++)
            total += attackProbabilities[i];

        for (int i = 0; i < attackProbabilities.Length; i++)
            attackProbabilities[i] /= total;
    }

    public void SetIdleState()
    {
        stateMachine.Set(idleState);
    }

    public void SetDashState()
    {
        stateMachine.Set(dashState);
    }

    public void SetRocketAttackState()
    {
        stateMachine.Set(rocketAttackState);
    }

    public void SetDeactivateState()
    {
        canTakeDamage = false;

        stateMachine.Set(deactivateState);
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        if (!canTakeDamage)
            return;

        currentHealth -= damage;

        BossDamagedEffect.Instance.ApplyEffect(gameObject);

        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}