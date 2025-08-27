using UnityEngine;

public class MovimentoInimigo : MonoBehaviour
{
    [SerializeField] private CustomTimeScale customTimeScale;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 direcaoMovimento;
    public SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (direcaoMovimento.x < Mathf.Epsilon)
        {
            sr.flipX = true;
        }



        Destroy(gameObject, 5);
    }


    void FixedUpdate()
    {
        rb.linearVelocity = customTimeScale.GetFixedDeltaTime() / Time.fixedDeltaTime * moveSpeed * direcaoMovimento;
    }
}
