using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class BossController : MonoBehaviour
{
    [Header("-----States-----")]
    [SerializeField] private Idle idleState;
    [SerializeField] private DashWindUp dashWindUpState;
    [SerializeField] private Dash dashState;
    [SerializeField] private CameraLeave cameraLeaveState;
    [SerializeField] private RocketAttack rocketAttackState;
    [SerializeField] private Deactivate deactivateState;
    private StateMachine stateMachine;

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

        stateMachine.Set(idleState);

        isFacingRight = false;
        canTakeDamage = true;
    }

    private void Update()
    {
        stateMachine.currentState.StateUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.Set(dashWindUpState);

        else if (Input.GetKeyDown(KeyCode.F))
            stateMachine.Set(cameraLeaveState);
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.StateFixedUpdate();
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
}