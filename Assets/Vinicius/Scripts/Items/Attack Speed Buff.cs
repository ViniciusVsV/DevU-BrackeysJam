using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackSpeedBuff : MonoBehaviour
{
    private PlayerWeapon playerWeapon;

    [SerializeField] private float cooldownMultiplier = 0.8f;
    [SerializeField] private float buffDuration;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            col.enabled = false;

            playerWeapon = other.GetComponent<PlayerController>().GetWeapon();

            if (playerWeapon != null)
                playerWeapon.ChangeCooldown(cooldownMultiplier, buffDuration);

            Destroy(gameObject);
        }
    }
}