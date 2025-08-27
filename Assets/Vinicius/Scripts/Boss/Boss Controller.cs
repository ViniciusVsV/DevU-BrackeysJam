using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class BossController : MonoBehaviour
{
    //Comportamento do boss se remusme em:
    //Flutuar em um lado da tela
    //Escolher entre sair da tela e invocar foguetes e dar um dash na posição x do jogador

    //Se sair da tela: Esolhe aleatoriamente entre os atques de invocar foguetes e depois volta em um lado aleatório da tela, repetindo o estado de flutuar

    //Se dar o dash: troca de posição na tela e repete o estado de flutuar
    [SerializeField] private CustomTimeScale customTimeScale;

    [Header("-----States-----")]
    [SerializeField] private Idle idleState;
    [SerializeField] private DashWindUp dashWindUpState;
    [SerializeField] private Dash dashState;
    private StateMachine stateMachine;

    private Rigidbody2D rb;
    private Animator animator;

    public bool isFacingRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();

        idleState.Setup(rb, transform, animator, this, customTimeScale);
        dashWindUpState.Setup(rb, transform, animator, this, customTimeScale);
        dashState.Setup(rb, transform, animator, this, customTimeScale);

        stateMachine.Set(idleState);

        isFacingRight = true;
    }

    private void Update()
    {
        stateMachine.currentState.StateUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.Set(dashWindUpState);
    }

    public void SetIdleState()
    {
        stateMachine.Set(idleState);
    }

    public void SetDashState()
    {
        stateMachine.Set(dashState);
    }
}