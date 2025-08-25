using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponBuff : MonoBehaviour
{
    private PlayerWeapon playerWeapon;

    [SerializeField] private GameObject newProjectile;
    [SerializeField] private float cooldownMultiplier = 0.8f;
    [SerializeField] private float damageMultiplier = 1.2f;
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
            {
                playerWeapon.ChangeProjectile(newProjectile, buffDuration);
                playerWeapon.ChangeCooldown(cooldownMultiplier, buffDuration);
                //Mudar o dano
            }

            Destroy(gameObject);
        }
    }
}