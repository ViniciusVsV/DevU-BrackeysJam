using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JetpackBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Transform playerPosition;
    private Rigidbody2D rb;

    private bool hasCollided;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerPosition = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector2 playerDirection = playerPosition.position - transform.position;

        rb.linearVelocity = playerDirection.normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided)
            return;

        if (other.CompareTag("Player"))
        {
            hasCollided = true;

            PlayerController playerController = other.GetComponent<PlayerController>();

            playerController.FlyAway();

            Destroy(gameObject);
        }
    }
}