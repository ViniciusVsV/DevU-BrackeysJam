using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MoveSpeedBuff : MonoBehaviour
{
    [SerializeField] private float newMoveSpeed;
    [SerializeField] private float buffDuration;

    private Collider2D col;

    private PlayerController playerController;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            col.enabled = false;
            AudioController.Instance.PlayPowerUpSound();

            playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
                playerController.BuffMoveSpeed(newMoveSpeed, buffDuration);

            Destroy(gameObject);
        }
    }
}